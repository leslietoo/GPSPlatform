using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GPS.PubSub.Abstractions
{
    public interface IConsumer : IPubSub,IDisposable
    {
        event EventHandler<object> OnMessage;
        event EventHandler<object> OnError;
        event EventHandler<object> OnConsumeError;
        CancellationTokenSource Cts { get; }
        void Subscribe();
        void Unsubscribe();
    }
}
