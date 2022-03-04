using BL.Interfaces;
using DAL.Database;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using WebAPI.Extensions;
using static Shared.Constants.ConfigurationConstants;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString(ÑonnectionString);

// Add services to the container.
#region Service container
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddRepositories().AddServices();
builder.Services.AddHangfire(x => x.UseSqlServerStorage(connection));
builder.Services.AddHangfireServer();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
#endregion

// Configure the HTTP request pipeline.
#region Configure
app.UseGlobalExceptionMiddleware();

app.UseHangfireDashboard();
RecurringJob.AddOrUpdate<IWeatherHistoryService>("addweatherhistory", x => x.AddWeatherHistoryAsync(),
    builder.Configuration["CronSetting"]);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(endpoints => 
{
    endpoints.MapControllers();
});

app.Run();
#endregion 
