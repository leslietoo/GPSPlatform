using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.Service.Abstractions
{
    public abstract class BackgroundService : IHostedService, IDisposable
    {
        public abstract string ServiceName { get; }

        protected abstract Task StartAsync(CancellationToken cancellationToken);

        protected abstract Task StopAsync(CancellationToken cancellationToken);

        private readonly CancellationTokenSource _stoppingCts =new CancellationTokenSource();

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            return StartAsync(cancellationToken);
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            return StopAsync(cancellationToken);
        }

        public virtual void Dispose()
        {
            _stoppingCts.Cancel();
        }
    }
}
