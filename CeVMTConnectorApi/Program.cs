using CeVMTConnectorApi.Bll;
using CeVMTConnectorApi.Dal.Contexts;
using CeVMTConnectorApi.Dal.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<SqlDataContext>();
builder.Services.AddSingleton<AzureDataContext>();
builder.Services.AddTransient<SqlRepository>();
builder.Services.AddTransient<AzureRepository>();
builder.Services.AddTransient<DataTransferService>();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("C:/Logs/CeVMTConnectorApi/CeVMTConnectorApi.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Logging.AddSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();