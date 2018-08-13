using GPS.PubSub.Abstractions;
using JT808.MsgId0x0200Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using JT808.Protocol.Extensions;
using System.Threading;

namespace GPS.JT808PubSubToRabbitMQ.Producer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serverHostBuilder = new HostBuilder();
            JT808_0x0200_Producer jT808_0X0200_Producer = new JT808_0x0200_Producer("host=172.16.19.120");
            var bytes = "7E 02 00 00 6D 01 35 10 26 00 01 2A 98 00 00 00 00 00 08 00 01 01 57 99 5C 06 CA 26 AC 02 72 00 00 01 48 18 07 22 16 00 10 01 04 00 00 80 73 10 01 63 2A 02 00 00 30 01 17 56 02 0A 00 53 31 06 01 CC 00 24 93 16 97 41 01 CC 00 24 93 14 03 47 01 CC 00 26 39 13 BB 47 01 CC 00 24 93 16 98 4A 01 CC 00 26 39 12 54 4A 01 CC 00 24 93 12 67 4F 57 08 00 00 00 00 00 00 00 00 62 7E".ToHexBytes();
            int i = 100000;
            while (i>0)
            {
                jT808_0X0200_Producer.ProduceAsync("", new byte[] { (byte)i });
                Console.WriteLine(i.ToString());
                i--;
                Thread.Sleep(5000);
            }
            await serverHostBuilder.RunConsoleAsync();
        }
    }
}
