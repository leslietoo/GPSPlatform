using JT808.Protocol;
using JT808.Protocol.Enums;
using JT808.Protocol.MessageBodyReply;
using SuperSocket.SocketBase.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using JT808.Protocol.JT808PackageImpl.Reply;
using Protocol.Common.Extensions;
using Newtonsoft.Json;
using JT808.Protocol.Exceptions;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using SuperSocket.SocketBase;
using JT808.MsgIdExtensions;
using JT808.Protocol.JT808PackageImpl.Reply;

namespace GPS.Gateway.JT808SuperSocketServer
{
    /// <summary>
    /// JT808通用应答
    /// </summary>
    public class JT808Command : CommandBase<JT808Session<JT808RequestInfo>, JT808RequestInfo>
    {
        public override string Name => "JT808";

        public static readonly JT808GlobalConfigs JT808GlobalConfigs = new JT808GlobalConfigs();

        /// <summary>
        /// 平台流水号
        /// </summary>
        private ushort _SNumId;

        public ushort sNumId
        {
            get
            {
               if (_SNumId >= 65535) _SNumId = 0;
               return  _SNumId++;
            }
        }

        public JT808Command()
        {
            InitHandler();
        }

        public override void ExecuteCommand(JT808Session<JT808RequestInfo> session, JT808RequestInfo requestInfo)
        {
            if (requestInfo.JT808Package == null) return;
            string receive = MessagePack.MessagePackSerializer.Serialize(requestInfo.JT808Package).ToHexString();
            session.Logger.Debug("receive-" + receive);
            session.Logger.Debug("receive-" + requestInfo.JT808Package.Header.MsgId.ToString() + "-" + JsonConvert.SerializeObject(requestInfo.JT808Package));
            try
            {
                Func<JT808Package, IJT808Package> handlerFunc;
                if (HandlerDict.TryGetValue(requestInfo.JT808Package.Header.MsgId,out handlerFunc))
                {
                    IJT808Package jT808PackageImpl = handlerFunc(requestInfo.JT808Package);
                    if (jT808PackageImpl != null)
                    {
                        session.Logger.Debug("send-" + jT808PackageImpl.JT808Package.Header.MsgId.ToString() + "-" + MessagePack.MessagePackSerializer.Serialize(jT808PackageImpl.JT808Package).ToHexString());
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

        /// <summary>
        /// 消息处理业务
        /// </summary>
        private Dictionary<JT808MsgId, Func<JT808Package, IJT808Package>> HandlerDict;

        /// <summary>
        /// 初始化消息处理业务
        /// </summary>
        private void InitHandler()
        {
            HandlerDict = new Dictionary<JT808MsgId, Func<JT808Package, IJT808Package>>
            {
                {JT808MsgId.终端鉴权, Msg0x0102},
                {JT808MsgId.终端心跳, Msg0x0002},
                {JT808MsgId.终端注销, Msg0x0003},
                {JT808MsgId.终端注册, Msg0x0100},
                {JT808MsgId.位置信息汇报,Msg0x0200 },
                {JT808MsgId.定位数据批量上传,Msg0x0704 }
            };
        }

        private IJT808Package Msg0x0102(JT808Package jT808Package)
        {
                return new JT808_0x8001Package(jT808Package.Header, sNumId, new JT808_0x8001()
                {
                    MsgId = jT808Package.Header.MsgId,
                    JT808PlatformResult = JT808PlatformResult.Success,
                    MsgNum = jT808Package.Header.MsgNum
                });
        }

        private IJT808Package Msg0x0002(JT808Package jT808Package)
        {
            return new JT808_0x8001Package(jT808Package.Header, sNumId, new JT808_0x8001()
            {
                MsgId = jT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = jT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0003(JT808Package jT808Package)
        {
            return new JT808_0x8001Package(jT808Package.Header, sNumId, new JT808_0x8001()
            {
                MsgId = jT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = jT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0100(JT808Package jT808Package)
        {
            return new JT808_0x8100Package(jT808Package.Header, sNumId, new JT808_0x8100()
            {
                Code = "J" + jT808Package.Header.TerminalPhoneNo,
                JT808TerminalRegisterResult = JT808TerminalRegisterResult.成功,
                MsgNum = jT808Package.Header.MsgNum
            });
        }

        private static readonly JT808_0x0200_Producer jT808_0X0200_Producer = new JT808_0x0200_Producer();

        private IJT808Package Msg0x0200(JT808Package jT808Package)
        {
            //jT808_0X0200_Producer.MsgIdProducer.ProduceAsync(((ushort)JT808MsgId.位置信息汇报).ToString(), null, JsonConvert.SerializeObject(jT808Package));
            return new JT808_0x8001Package(jT808Package.Header, sNumId, new JT808_0x8001()
            {
                MsgId = jT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = jT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0704(JT808Package jT808Package)
        {
            return null;
        }
    }
}
