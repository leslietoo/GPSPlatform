using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.PubSub.Abstractions
{
    public interface IConsumer : IPubSub,IDisposable
    {
        event EventHandler<object> OnMessage;
        event EventHandler<object> OnError;
        event EventHandler<object> OnConsumeError;
        void Subscribe();
        void Unsubscribe();
    }
}
