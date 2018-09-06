using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GPS.IdentityServer4Grain.Providers;

namespace GPS.IdentityServer4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new Microsoft.Extensions.Hosting.HostBuilder()
                        .ConfigureServices((hostContext, services) =>
                        {
                            services.AddDbContext<GPSIdentityServerDbContext>();
                        }).Build();
            builder.StartAsync().Wait();
        }
    }
}
