using System;
using System.Threading.Tasks;

namespace GPS.PubSub.Abstractions
{
    public interface IJT808Producer:IJT808PubSub, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgId">消息Id</param>
        /// <param name="terminalNo">设备终端号</param>
        /// <param name="data">hex data</param>
        void ProduceAsync(string msgId, string terminalNo,byte[] data);
    }
}
