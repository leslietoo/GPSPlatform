using GPS.Service.Abstractions;
using Grpc.Core;
using Grpc.Health.V1;
using Grpc.HealthCheck;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace GPS.GrpcServiceBase.Internal
{
    public class GrpcBackgroundService : BackgroundService
    {
        public override string ServiceName => "GrpcService";

        private readonly IList<Server> _servers;

        private readonly ILogger<GrpcBackgroundService> _logger;

        public GrpcBackgroundService(IList<Server> servers, ILoggerFactory loggerFactory)
        {
            _servers = servers;
            Server serverHealth = new Server();
            serverHealth.Services.Add(Health.BindService(new HealthServiceImpl()));
            _servers.Add(serverHealth);
            _logger = loggerFactory.CreateLogger<GrpcBackgroundService>();
        }

        protected override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Starting gRPC background service");
            foreach (var server in _servers)
            {
                StartServer(server);
            }
            _logger.LogDebug("gRPC background service started");
            return Task.CompletedTask;
        }

        protected override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Stopping gRPC background service");
            var shutdownTasks = _servers.Select(server => server.ShutdownAsync()).ToList();
            await Task.WhenAll(shutdownTasks).ConfigureAwait(false);
            _logger.LogDebug("gRPC background service stopped");
        }

        private void StartServer(Server server)
        {
            _logger.LogDebug("Starting gRPC server listening on: {hostingEndpoints}",string.Join("; ", server.Ports.Select(p => $"{p.Host}:{p.BoundPort}")));
            server.Start();
            _logger.LogDebug("Started gRPC server listening on: {hostingEndpoints}",string.Join("; ", server.Ports.Select(p => $"{p.Host}:{p.BoundPort}")));
        }
    }
}
