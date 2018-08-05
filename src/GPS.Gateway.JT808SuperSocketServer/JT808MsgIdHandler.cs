using JT808.Protocol;
using JT808.Protocol.Enums;
using JT808.Protocol.JT808PackageImpl.Reply;
using JT808.Protocol.MessageBodyReply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS.Gateway.JT808SuperSocketServer
{
    /// <summary>
    /// 消息处理业务
    /// </summary>
    public class JT808MsgIdHandler
    {
        /// <summary>
        /// 初始化消息处理业务
        /// </summary>
        public JT808MsgIdHandler()
        {
            HandlerDict = new Dictionary<JT808MsgId, Func<JT808Package, JT808Session<JT808RequestInfo>, IJT808Package>>
            {
                {JT808MsgId.终端鉴权, Msg0x0102},
                {JT808MsgId.终端心跳, Msg0x0002},
                {JT808MsgId.终端注销, Msg0x0003},
                {JT808MsgId.终端注册, Msg0x0100},
                {JT808MsgId.位置信息汇报,Msg0x0200 },
                {JT808MsgId.定位数据批量上传,Msg0x0704 }
            };
        }

        public Dictionary<JT808MsgId, Func<JT808Package, JT808Session<JT808RequestInfo>, IJT808Package>> HandlerDict { get; }

        private IJT808Package Msg0x0102(JT808Package jT808Package, JT808Session<JT808RequestInfo> session)
        {
            return new JT808_0x8001Package(jT808Package.Header, new JT808_0x8001()
            {
                MsgId = jT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = jT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0002(JT808Package jT808Package, JT808Session<JT808RequestInfo> session)
        {
            return new JT808_0x8001Package(jT808Package.Header, new JT808_0x8001()
            {
                MsgId = jT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = jT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0003(JT808Package jT808Package, JT808Session<JT808RequestInfo> session)
        {
            return new JT808_0x8001Package(jT808Package.Header, new JT808_0x8001()
            {
                MsgId = jT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = jT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0100(JT808Package jT808Package, JT808Session<JT808RequestInfo> session)
        {
            return new JT808_0x8100Package(jT808Package.Header, new JT808_0x8100()
            {
                Code = "J" + jT808Package.Header.TerminalPhoneNo,
                JT808TerminalRegisterResult = JT808TerminalRegisterResult.成功,
                MsgNum = jT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0200(JT808Package jT808Package, JT808Session<JT808RequestInfo> session)
        {
            ((JT808Server)session.AppServer)?.JT808_0X0200_Producer.ProduceAsync(JT808Serializer.Serialize(jT808Package));
            return new JT808_0x8001Package(jT808Package.Header, new JT808_0x8001()
            {
                MsgId = jT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = jT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0704(JT808Package jT808Package, JT808Session<JT808RequestInfo> session)
        {
            return new JT808_0x8001Package(jT808Package.Header, new JT808_0x8001()
            {
                MsgId = jT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = jT808Package.Header.MsgNum
            });
        }
    }
}
