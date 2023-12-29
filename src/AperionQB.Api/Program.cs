using AperionQB.Infrastructure.Data;
using Serilog;
using AperionQB.Application;
using AperionQB.Infrastructure;


var builder = WebApplication.CreateBuilder(args);


// Add Serilog Logging
builder.Host.UseSerilog((context, config) =>
{
    config.MinimumLevel.Error();
    config.WriteTo.Console();
    config.WriteTo.Debug();
    config.WriteTo.File("logs/QuibbyLogs.log", rollingInterval: RollingInterval.Day);
});

// Add services to the container
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));

// Setup CORS
string[] allowedOrigins = builder.Configuration["AllowedOrigins"].Split(",");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader();
    });
});


builder.Services.AddControllers().AddJsonOptions(config =>
{
    config.JsonSerializerOptions.MaxDepth = 0;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors();
app.MapControllers();

app.Run();

