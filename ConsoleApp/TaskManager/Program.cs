using BL.Interfaces;
using BL.Mapping;
using BL.Services;
using DAL.Database;
using DAL.Interfaces;
using DAL.Repositories;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskManager.Extensions;
using static Shared.Constants.ConfigurationConstants;

namespace TaskManager
{
    public class Program
    {
        public static void Main(string[] args)
            => CreateHostBuilder(args).Build().Run();

        // EF Core uses this method at design time to access the DbContext
        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder => webBuilder.UseStartup<Startup>());
    }

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString(ConnectionString);
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

            //services.AddHangfire(x => x.UseSqlServerStorage(connection));
            //services.AddHangfireServer();

            services.AddRepositories().AddServices().AddAutoMapper().AddLogging(x => x.AddSerilog());

            services.AddHttpClient();

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHangfireDashboard();
            //RecurringJob.AddOrUpdate<IEmailService>("sendmessage", x => x.SendEmailAsync(), Cron.Hourly);

            app.UseRouting();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}