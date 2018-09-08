using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.Gateway.JT808SuperSocketServer
{
    public class JT808Service: IHostedService
    {
        private readonly JT808Server JT808Server;

        private readonly ILogger<JT808Service> log;

        private readonly ILogFactory LogFactory;

        private readonly ServerConfig serverConfig;

        public JT808Service(JT808Server jT808Server,
                            IOptions<ServerConfig> serverConfigAccessor,
                            ILogFactory logFactory,
                            ILoggerFactory loggerFactory)
        {
            JT808Server = jT808Server;
            this.serverConfig = serverConfigAccessor.Value;
            LogFactory = logFactory;
            log = loggerFactory.CreateLogger<JT808Service>();
        }

        public string ServiceName => "JT808Service";

        public Task StartAsync(CancellationToken cancellationToken)
        {
            log.LogInformation("Start:" + ServiceName);
            if (!JT808Server.Setup(serverConfig, null,null, LogFactory, null))
            {
                log.LogError("Failed to initialize SuperSocket ServiceEngine! Please check error log for more information!");
                return Task.CompletedTask;
            }
            if (!JT808Server.Start())
            {
                log.LogInformation("Start Error");
                return Task.CompletedTask;
            }
            log.LogInformation("Start Listener:" + JsonConvert.SerializeObject(serverConfig));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            log.LogInformation("Stop:" + ServiceName);
            log.LogInformation("Stop Listener:" + JsonConvert.SerializeObject(serverConfig));
            JT808Server.Stop();
            return Task.CompletedTask;
        }
    }
}
