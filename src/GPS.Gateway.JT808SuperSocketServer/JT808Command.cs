using JT808.Protocol;
using SuperSocket.SocketBase.Command;
using System;
using Newtonsoft.Json;
using JT808.Protocol.Exceptions;
using JT808.Protocol.Extensions;

namespace GPS.Gateway.JT808SuperSocketServer
{
    /// <summary>
    /// JT808通用应答
    /// </summary>
    public class JT808Command : CommandBase<JT808Session<JT808RequestInfo>, JT808RequestInfo>
    {
        public JT808Command()
        {

        }

        public override string Name => "JT808";

        public override void ExecuteCommand(JT808Session<JT808RequestInfo> session, JT808RequestInfo requestInfo)
        {
            if (requestInfo.JT808Package == null) return;
            session.TerminalPhoneNo = requestInfo.JT808Package.Header.TerminalPhoneNo;
            string receive = requestInfo.OriginalBuffer.ToHexString();
            session.Logger.Debug("receive-" + receive);
            session.Logger.Debug("receive-" + requestInfo.JT808Package.Header.MsgId.ToString() + "-" + JsonConvert.SerializeObject(requestInfo.JT808Package));
            try
            {
                Func<JT808Package, JT808Session<JT808RequestInfo>, IJT808Package> handlerFunc;
                if (((JT808Server)session.AppServer).JT808MsgIdHandler.HandlerDict.TryGetValue(requestInfo.JT808Package.Header.MsgId,out handlerFunc))
                {
                    IJT808Package jT808PackageImpl = handlerFunc(requestInfo.JT808Package, session);
                    if (jT808PackageImpl != null)
                    {
                        session.Logger.Debug("send-" + jT808PackageImpl.JT808Package.Header.MsgId.ToString() + "-" + JT808Serializer.Serialize(jT808PackageImpl.JT808Package).ToHexString());
                        session.Logger.Debug("send-" + jT808PackageImpl.JT808Package.Header.MsgId.ToString() + "-" + JsonConvert.SerializeObject(jT808PackageImpl.JT808Package));
                        session.TrySend(jT808PackageImpl);
                    }
                }
            }
            catch (JT808Exception ex)
            {
                session.Logger.Error("JT808Exception receive-" + receive);
                session.Logger.Error(ex.Message, ex);
            }
            catch (Exception ex)
            {
                session.Logger.Error("Exception receive-" + receive);
                session.Logger.Error(ex);
            }
        }
    }
}
