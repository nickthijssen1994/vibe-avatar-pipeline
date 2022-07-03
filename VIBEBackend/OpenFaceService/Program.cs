using Autofac;
using Autofac.Extensions.DependencyInjection;
using MessagingNetwork;
using OpenFaceService;
using OpenFaceService.Data.Context;
using OpenFaceService.Services;
using OpenFaceService.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);
using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
	.SetMinimumLevel(LogLevel.Trace)
	.AddConsole());

ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy",
		builder => builder.SetIsOriginAllowed(x => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(sp => RabbitBusFactory.CreateBus(builder.Configuration["EventBusConnection"],
	ushort.Parse(builder.Configuration["EventBusPort"]), builder.Configuration["EventBusVirtualHost"],
	builder.Configuration["EventBusUserName"], builder.Configuration["EventBusPassword"]));

builder.Services.AddHostedService<BusListenerService>();
builder.Services.AddAzureBlobStorageServices(builder.Configuration);
builder.Services.AddTransient<IOpenFaceServiceContext, OpenFaceServiceContext>();
builder.Services.AddTransient<IOpenFaceAnalysisService, OpenFaceAnalysisServiceImpl>();

var container = new ContainerBuilder();
container.Populate(builder.Services);
var autofacServiceProvider = new AutofacServiceProvider(container.Build());

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();