using JT808.Protocol;
using JT808.Protocol.Enums;
using JT808.Protocol.MessageBodyReply;
using SuperSocket.SocketBase.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JT808.Protocol.JT808PackageImpl.Reply;
using Protocol.Common.Extensions;
using Newtonsoft.Json;
using JT808.Protocol.Exceptions;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace GPS.Gateway.JT808SuperSocketServer
{
    /// <summary>
    /// JT808通用应答
    /// </summary>
    public class JT808Command : CommandBase<JT808Session<JT808RequestInfo>, JT808RequestInfo>
    {
        public override string Name => "JT808";

        public static readonly JT808GlobalConfigs JT808GlobalConfigs = new JT808GlobalConfigs();

        private int _SNumId;

        public int sNumId
        {
            get
            {
               if (_SNumId >= 65535) _SNumId = 0;
               return  _SNumId++;
            }
        }

        public JT808Command()
        {

        }

        private Producer<string, string> MQProducer;

        private Dictionary<string, object> MQConfig = new Dictionary<string, object>
        {
            {"bootstrap.servers", "127.0.0.1:9092" }
        };

        private Func<JT808RequestInfo, Action, IJT808Package> HandlerFunc;

        public override void ExecuteCommand(JT808Session<JT808RequestInfo> session, JT808RequestInfo requestInfo)
        {
            
            try
            {
                session.Logger.Debug("receive-" + requestInfo.JT808Package.Buffer.ToArray().ToHexString());
                requestInfo.JT808Package.ReadBuffer(JT808GlobalConfigs);
                session.Logger.Debug("receive-" + requestInfo.JT808Package.Header.MsgId.ToString() + "-" + JsonConvert.SerializeObject(requestInfo.JT808Package));
                IJT808Package jT808PackageImpl = null;
                switch (requestInfo.JT808Package.Header.MsgId)
                {
                    case JT808.Protocol.Enums.JT808MsgId.终端鉴权:
                        InitProducer(requestInfo.JT808Package.Header.TerminalPhoneNo);
                        jT808PackageImpl = new JT808_0x8001Package(requestInfo.JT808Package.Header, sNumId, new JT808_0x8001()
                        {
                            MsgId = requestInfo.JT808Package.Header.MsgId,
                            JT808PlatformResult = JT808PlatformResult.Success,
                            MsgNum = requestInfo.JT808Package.Header.MsgNum
                        }, JT808GlobalConfigs);
                        break;
                    case JT808.Protocol.Enums.JT808MsgId.终端心跳:
                        jT808PackageImpl = new JT808_0x8001Package(requestInfo.JT808Package.Header, sNumId, new JT808_0x8001()
                        {
                            MsgId = requestInfo.JT808Package.Header.MsgId,
                            JT808PlatformResult = JT808PlatformResult.Success,
                            MsgNum = requestInfo.JT808Package.Header.MsgNum
                        }, JT808GlobalConfigs);
                        break;
                    case JT808.Protocol.Enums.JT808MsgId.终端注销:
                        CloseProducer();
                        jT808PackageImpl = new JT808_0x8001Package(requestInfo.JT808Package.Header, sNumId, new JT808_0x8001()
                        {
                            MsgId = requestInfo.JT808Package.Header.MsgId,
                            JT808PlatformResult = JT808PlatformResult.Success,
                            MsgNum = requestInfo.JT808Package.Header.MsgNum
                        }, JT808GlobalConfigs);
                        break;
                    case JT808.Protocol.Enums.JT808MsgId.位置信息汇报:
                        jT808PackageImpl = new JT808_0x8001Package(requestInfo.JT808Package.Header, sNumId, new JT808_0x8001()
                        {
                            MsgId = requestInfo.JT808Package.Header.MsgId,
                            JT808PlatformResult = JT808PlatformResult.Success,
                            MsgNum = requestInfo.JT808Package.Header.MsgNum
                        }, JT808GlobalConfigs);
                        break;
                    case JT808.Protocol.Enums.JT808MsgId.终端注册:
                        InitProducer(requestInfo.JT808Package.Header.TerminalPhoneNo);
                        jT808PackageImpl = new JT808_0x8100Package(requestInfo.JT808Package.Header, sNumId, new JT808_0x8100()
                        {
                            Code = "J" + requestInfo.JT808Package.Header.TerminalPhoneNo,
                            JT808TerminalRegisterResult = JT808TerminalRegisterResult.成功,
                            MsgNum = requestInfo.JT808Package.Header.MsgNum
                        }, JT808GlobalConfigs);
                        break;
                    default:
                        break;
                }
                if (jT808PackageImpl != null)
                {
                    session.Logger.Debug("send-" + jT808PackageImpl.JT808Package.Header.MsgId.ToString() + "-" + jT808PackageImpl.JT808Package.Buffer.ToArray().ToHexString());
                    session.Logger.Debug("send-" + jT808PackageImpl.JT808Package.Header.MsgId.ToString() + "-" + JsonConvert.SerializeObject(jT808PackageImpl.JT808Package));
                    session.TrySend(jT808PackageImpl);
                }
            }
            catch (JT808Exception ex)
            {
                session.Logger.Error("JT808Exception receive-" + requestInfo.JT808Package.Buffer.ToArray().ToHexString());
                session.Logger.Error(ex.Message, ex);
            }
            catch (Exception ex)
            {
                session.Logger.Error("Exception receive-" + requestInfo.JT808Package.Buffer.ToArray().ToHexString());
                session.Logger.Error(ex);
            }
        }

        private void InitProducer(string topic)
        {
            if (MQProducer  == null)
            {
                MQConfig.Add("topic", topic);
                MQProducer = new Producer<string, string>(MQConfig, new StringSerializer(Encoding.UTF8), new StringSerializer(Encoding.UTF8));
            }
        }

        private void CloseProducer()
        {
            MQProducer.Flush(500);
            MQProducer.Dispose();
            MQProducer = null;
        }
    }
}
