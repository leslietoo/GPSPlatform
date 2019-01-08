using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GPS.PubSub.Abstractions
{
    public interface IJT808Consumer : IJT808PubSub, IDisposable
    {
        void OnMessage(string msgId, Action<(string MsgId, byte[] data)> callback);
        CancellationTokenSource Cts { get; }
        void Subscribe();
        void Unsubscribe();
    }
}
