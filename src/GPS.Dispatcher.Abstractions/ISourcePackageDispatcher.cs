using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GPS.Dispatcher.Abstractions
{
    /// <summary>
    /// 源包分发器
    /// </summary>
    public interface ISourcePackageDispatcher
    {
        Task SendAsync(byte[] data);
    }
}
