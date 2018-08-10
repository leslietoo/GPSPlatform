using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GPS.PubSub.Abstractions
{
    public interface IConsumer : IPubSub
    {
        void OnMessage(Action<(string Key, byte[] data)> callback);
        CancellationTokenSource Cts { get; }
        void Subscribe();
        void Unsubscribe();
    }
}
