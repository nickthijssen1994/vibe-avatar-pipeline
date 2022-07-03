using MessagingNetwork;
using ReportService.Repositories;
using FileStorage;

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

builder.Services.AddAzureBlobStorageServices(builder.Configuration);
builder.Services.AddScoped<IReportRepository, ReportRepository>();

builder.Services.AddSingleton(sp => RabbitBusFactory.CreateBus(builder.Configuration["EventBusConnection"],
	ushort.Parse(builder.Configuration["EventBusPort"]), builder.Configuration["EventBusVirtualHost"],
	builder.Configuration["EventBusUserName"], builder.Configuration["EventBusPassword"]));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	//var context = services.GetRequiredService<ReportContext>();
	//context.Database.EnsureCreated();
}

if (app.Environment.IsProduction())
{
	app.UseHttpsRedirection();
}

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();