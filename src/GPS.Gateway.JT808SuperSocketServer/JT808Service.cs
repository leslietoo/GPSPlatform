using log4net;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS.Gateway.JT808SuperSocketServer
{
    public class JT808Service: IService
    {
        private SuperSocket.SocketBase.IBootstrap bootstrap;

        private readonly ILog log;

        public JT808Service()
        {
            log = LogManager.GetLogger(typeof(JT808Service));
        }

        public string ServiceName => "JT808Service";

        public void Start()
        {
            bootstrap = BootstrapFactory.CreateBootstrap();
            Console.WriteLine(bootstrap.StartupConfigFile);
            if (!bootstrap.Initialize())
            {
                log.Error("Failed to initialize SuperSocket ServiceEngine! Please check error log for more information!");
                return;
            }
            var result = bootstrap.Start();
            foreach (var server in bootstrap.AppServers)
            {
                if (server.State == ServerState.Running)
                {
                    log.InfoFormat("- {0} has been started", server.Name);
                }
                else
                {
                    log.ErrorFormat("- {0} failed to start", server.Name);
                }
            }
            switch (result)
            {
                case (StartResult.None):
                    log.Error("No server is configured, please check you configuration!");
                    return;
                case (StartResult.Success):
                    log.Info("The SuperSocket ServiceEngine has been started!");
                    break;
                case (StartResult.Failed):
                    log.Error("Failed to start the SuperSocket ServiceEngine! Please check error log for more information!");
                    return;
                case (StartResult.PartialSuccess):
                    log.Error("Some server instances were started successfully, but the others failed! Please check error log for more information!");
                    break;
            }
        }

        public void Stop()
        {
            bootstrap?.Stop();
        }
    }
}
