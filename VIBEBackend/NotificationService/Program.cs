using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NotificationService.Data.Contexts;
using NotificationService.Data.Repositories;
using NotificationService.Data.Repositories.Implementations;
using NotificationService.Services;
using NotificationService.Services.Factories;
using NotificationService.Services.Implementations;
using MessagingNetwork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy",
		builder => builder.SetIsOriginAllowed((host) => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<INotificationService, MailNotificationService>((provider) =>
	new NotificationServiceFactory(provider.GetRequiredService<IMailRepository>()).CreateNotificationService());
builder.Services.AddScoped<IMailRepository, EntityMailRepository>();
builder.Services.AddDbContext<NotificationServiceContext>(options =>
{
	options.UseSqlServer(builder.Configuration["ConnectionString"], sqlOptions =>
	{
		sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
		//Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
		sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
	});
});

builder.Services.AddSingleton(sp => RabbitBusFactory.CreateBus(builder.Configuration["EventBusConnection"],
	ushort.Parse(builder.Configuration["EventBusPort"]), builder.Configuration["EventBusVirtualHost"],
	builder.Configuration["EventBusUserName"], builder.Configuration["EventBusPassword"]));
builder.Services.AddHostedService<BusListenerService>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<NotificationServiceContext>();

	await context.Database.MigrateAsync();

	if (app.Environment.IsDevelopment())
	{
		await context.Seed();
		app.UseSwagger();
		app.UseSwaggerUI();
	}
	else if (app.Environment.IsProduction())
	{
		app.UseHttpsRedirection();
	}
}

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();