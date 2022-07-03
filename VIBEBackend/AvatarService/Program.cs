using System.Reflection;
using AvatarService.Data;
using AvatarService.Repository;
using AvatarService.Services;
using AvatarService.Services.Implementations;
using MessagingNetwork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy",
		builder => builder.SetIsOriginAllowed((host) => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
});

// Add services to the container.

builder.Services.AddDbContext<AvatarContext>(options =>
{
	options.UseSqlServer(builder.Configuration["ConnectionString"], sqlOptions =>
	{
		sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
		//Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
		sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
	});
});
builder.Services.AddScoped<IAvatarRepository, AvatarRepository>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(sp => RabbitBusFactory.CreateBus(builder.Configuration["EventBusConnection"],
	ushort.Parse(builder.Configuration["EventBusPort"]), builder.Configuration["EventBusVirtualHost"],
	builder.Configuration["EventBusUserName"], builder.Configuration["EventBusPassword"]));

builder.Services.AddScoped<IAvatarService, AvatarServiceImpl>();
builder.Services.AddHostedService<BusListenerService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<AvatarContext>();

	await context.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();