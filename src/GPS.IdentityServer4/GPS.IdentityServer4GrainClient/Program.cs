using GPS.IdentityServer4IGrain;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GPS.IdentityServer4GrainClient
{
    class Program
    {
        const int initializeAttemptsBeforeFailing = 5;
        static int attempt = 0;

        static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<IClusterClient> StartClientWithRetries(int initializeAttemptsBeforeFailing = 5)
        {
            attempt = 0;
            IClusterClient client;
            while (true)
            {
                try
                {
                    client = new ClientBuilder()
                        .UseStaticClustering(new System.Net.IPEndPoint(IPAddress.Parse("127.0.0.1"),11111))
                        .UseLocalhostClustering()
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "dev";
                            options.ServiceId = "IdentityGrainApp";
                        })
                        .ConfigureLogging(logging => logging.AddConsole())
                        .Build();

                    await client.Connect(RetryFilter);
                    Console.WriteLine("Client successfully connect to silo host");
                    break;
                }
                catch (SiloUnavailableException)
                {
                    attempt++;
                    Console.WriteLine($"Attempt {attempt} of {initializeAttemptsBeforeFailing} failed to initialize the Orleans client.");
                    if (attempt > initializeAttemptsBeforeFailing)
                    {
                        throw;
                    }
                    await Task.Delay(TimeSpan.FromSeconds(4));
                }
            }

            return client;
        }

        private static async Task<string> DoClientWork(IClusterClient client)
        {
            IIdentityGrain atm = client.GetGrain<IIdentityGrain>(Guid.NewGuid());

            var a =await atm.GenerateToken(new IdentityServer4IGrain.Dtos.JwtOptionsParameterDto()
            {
                Claims = new Dictionary<string, string>() { }
            });
            return a;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                using (var client = await StartClientWithRetries())
                {
                    await DoClientWork(client);
                    Console.ReadKey();
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                return 1;
            }
        }

        private static async Task<bool> RetryFilter(Exception exception)
        {
            if (exception.GetType() != typeof(SiloUnavailableException))
            {
                Console.WriteLine($"Cluster client failed to connect to cluster with unexpected error.  Exception: {exception}");
                return false;
            }
            attempt++;
            Console.WriteLine($"Cluster client attempt {attempt} of {initializeAttemptsBeforeFailing} failed to connect to cluster.  Exception: {exception}");
            if (attempt > initializeAttemptsBeforeFailing)
            {
                return false;
            }
            await Task.Delay(TimeSpan.FromSeconds(4));
            return true;
        }
    }
}
