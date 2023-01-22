using AutoMapper;
using CoolParking.BL.Interfaces;
using CoolParking.BL.Services;

namespace CoolParking.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddSingleton<IParkingService, ParkingService>();
            services.AddTransient<ITimerService, TimerService>();
            services.AddSingleton<ILogService>(_ => new LogService($"{Directory.GetCurrentDirectory()}/Transactions.log"));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            services.AddCors();

            services.AddMvcCore(options =>
            {
                options.RequireHttpsPermanent = true;
                options.RespectBrowserAcceptHeader = true;
            })
            .AddFormatterMappings();
        }

        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
