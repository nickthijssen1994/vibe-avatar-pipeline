using AnalysisService.Services;
using AnalysisService.Services.Implementations;
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

builder.Services.AddSingleton(sp => RabbitBusFactory.CreateBus(builder.Configuration["EventBusConnection"],
	ushort.Parse(builder.Configuration["EventBusPort"]), builder.Configuration["EventBusVirtualHost"],
	builder.Configuration["EventBusUserName"], builder.Configuration["EventBusPassword"]));

builder.Services.AddScoped<IAnalysisService, AnalysisServiceImpl>();
builder.Services.AddHostedService<BusListenerService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();