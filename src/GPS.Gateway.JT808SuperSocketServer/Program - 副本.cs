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
            JT808Service jT808Service = new JT808Service();
            jT808Service.Start();
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            Console.CancelKeyPress += new ConsoleCancelEventHandler((obj, eventArgs) => 
            {
                Console.WriteLine("Cancel...");
                cancelTokenSource.Cancel();
            });
            while (!cancelTokenSource.IsCancellationRequested)
            {
                
            }
            jT808Service.Stop();
            Console.WriteLine("Stop...");
        }
    }
}
