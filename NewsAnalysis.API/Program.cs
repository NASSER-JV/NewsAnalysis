using System.Reflection;
using MediatR;
using NewsAnalysis.API.App.Ports;
using NewsAnalysis.API.Infrastructure.Adapters;
using NewsAnalysis.API.Infrastructure.Adapters.DataService;
using NewsAnalysis.API.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<AwsS3Config>(builder.Configuration.GetSection("AWSS3Credentials"));

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IStorageService, StorageService>();
builder.Services.AddSingleton<IDataService, DataService>();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();