using System;
using System.Threading.Tasks;

namespace GPS.PubSub.Abstractions
{
    public interface IJT808Producer:IJT808PubSub, IDisposable
    {
        void ProduceAsync(string key, byte[] data);
    }
}
