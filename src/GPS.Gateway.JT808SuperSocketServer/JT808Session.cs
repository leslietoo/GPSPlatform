using JT808.Protocol;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS.Gateway.JT808SuperSocketServer
{
    public class JT808Session<TRequestInfo> : AppSession<JT808Session<TRequestInfo>, TRequestInfo>
         where TRequestInfo : class, IRequestInfo
    {

        public  bool TrySend(IJT808Package jT808Package)
        {
           return base.TrySend(jT808Package.JT808Package.Buffer.ToArray(), 0, jT808Package.JT808Package.Buffer.Length);
        }
        //protected override void OnSessionStarted()
        //{

        //}

        //protected override void HandleUnknownRequest(TRequestInfo requestInfo)
        //{
        //    base.HandleUnknownRequest(requestInfo);
        //}

        //protected override void HandleException(Exception e)
        //{

        //}

        //protected override void OnSessionClosed(CloseReason reason)
        //{

        //    base.OnSessionClosed(reason);
        //}
    }
}
