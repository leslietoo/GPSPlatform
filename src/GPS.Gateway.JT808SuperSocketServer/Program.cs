using SuperSocket.SocketBase;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS.Gateway.JT808SuperSocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            JT808Service jT808Service = new JT808Service();
            jT808Service.Start();
            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }
            Console.Read();
            jT808Service.Stop();
            Console.Read();
        }
    }
}
