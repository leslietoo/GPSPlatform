using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.Gateway.JT808SuperSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += new ConsoleCancelEventHandler((sender, e) =>
                {
                    cts.Cancel();
                });
                JT808Service jT808Service = new JT808Service();
                Console.WriteLine("Start:" + jT808Service.ServiceName);
                jT808Service.Start();
                while (!cts.IsCancellationRequested)
                {

                }
                Console.WriteLine("Stop:" + jT808Service.ServiceName);
                jT808Service.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.ToString());
            }
        }
    }
}
