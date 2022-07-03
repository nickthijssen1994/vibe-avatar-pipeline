using MessagingNetwork;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy",
		builder => builder.SetIsOriginAllowed((host) => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
});

builder.Configuration.AddJsonFile($"ocelot.{builder.Configuration["OcelotDefaultHost"]}.json", false, true);
builder.Services.AddOcelot();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(sp => RabbitBusFactory.CreateBus(builder.Configuration["EventBusConnection"],
	ushort.Parse(builder.Configuration["EventBusPort"]), builder.Configuration["EventBusVirtualHost"],
	builder.Configuration["EventBusUserName"], builder.Configuration["EventBusPassword"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) { }

app.UseSwagger();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerForOcelotUI(opt =>
	{
		opt.DownstreamSwaggerHeaders = new[]
		{
			new KeyValuePair<string, string>("Key", "Value")
		};
	})
	.UseOcelot()
	.Wait();

app.Run();