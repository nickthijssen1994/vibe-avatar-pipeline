using FileStorage;
using FileStorageService.Repositories;
using FileStorageService.Services;
using FileStorageService.Services.Implementations;
using MessagingNetwork;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy",
		builder => builder.SetIsOriginAllowed((host) => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
});

// Add services to the container.
builder.Services.AddAzureBlobStorageServices(builder.Configuration);
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 268435456; });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(sp => RabbitBusFactory.CreateBus(builder.Configuration["EventBusConnection"],
	ushort.Parse(builder.Configuration["EventBusPort"]), builder.Configuration["EventBusVirtualHost"],
	builder.Configuration["EventBusUserName"], builder.Configuration["EventBusPassword"]));

builder.Services.AddScoped<IFileStorageService, FileStorageServiceImpl>();
builder.Services.AddHostedService<BusListenerService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();