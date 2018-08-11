using GPS.Dispatcher.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GPS.JT808SourcePackageDispatcher
{
    public class JT808SourcePackageDispatcherImpl : ISourcePackageDispatcher
    {
        public async Task SendAsync(byte[] data)
        {
            await Task.CompletedTask;
        }
    }
}
