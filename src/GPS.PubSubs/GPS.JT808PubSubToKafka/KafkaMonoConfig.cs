using Confluent.Kafka;
using System;

namespace GPS.JT808PubSubToKafka
{
    public  class KafkaMonoConfig
    {
        public static void Load(string monoRuntimePath)
        {
            if (Environment.OSVersion.Platform.ToString() != "Win32NT")
            {
                Library.Load(monoRuntimePath);
            }
        }
    }
}
