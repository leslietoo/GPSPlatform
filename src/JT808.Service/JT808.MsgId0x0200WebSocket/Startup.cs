using JT808.MsgId0x0200WebSocket.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JT808.MsgId0x0200WebSocket
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("SignalRCors", policy =>
                 {
                     policy.AllowAnyOrigin()
                             .AllowAnyHeader()
                             .AllowAnyMethod()
                             .AllowCredentials();
                 });
            });
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            //signalr依赖注入：https://docs.microsoft.com/en-us/aspnet/signalr/overview/advanced/dependency-injection
            app.UseCors("SignalRCors");
            //注册hub
            app.UseSignalR(routes =>
            {
                routes.MapHub<AlarmHub>("/AlarmHub");
            });
        }
    }
}
