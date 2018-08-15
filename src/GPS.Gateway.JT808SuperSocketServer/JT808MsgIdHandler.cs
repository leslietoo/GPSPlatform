using JT808.Protocol;
using JT808.Protocol.Enums;
using JT808.Protocol.JT808PackageImpl.Reply;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPS.PubSub.Abstractions;

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
            HandlerDict = new Dictionary<JT808MsgId, Func<JT808RequestInfo, JT808Session<JT808RequestInfo>, IJT808Package>>
            {
                {JT808MsgId.终端鉴权, Msg0x0102},
                {JT808MsgId.终端心跳, Msg0x0002},
                {JT808MsgId.终端注销, Msg0x0003},
                {JT808MsgId.终端注册, Msg0x0100},
                {JT808MsgId.位置信息汇报,Msg0x0200 },
                {JT808MsgId.定位数据批量上传,Msg0x0704 }
            };
        }

        public Dictionary<JT808MsgId, Func<JT808RequestInfo, JT808Session<JT808RequestInfo>, IJT808Package>> HandlerDict { get; }

        private IJT808Package Msg0x0102(JT808RequestInfo requestInfo, JT808Session<JT808RequestInfo> session)
        {
            return new JT808_0x8001Package(requestInfo.JT808Package.Header, new JT808_0x8001()
            {
                MsgId = requestInfo.JT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = requestInfo.JT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0002(JT808RequestInfo requestInfo, JT808Session<JT808RequestInfo> session)
        {
            return new JT808_0x8001Package(requestInfo.JT808Package.Header, new JT808_0x8001()
            {
                MsgId = requestInfo.JT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = requestInfo.JT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0003(JT808RequestInfo requestInfo, JT808Session<JT808RequestInfo> session)
        {
            return new JT808_0x8001Package(requestInfo.JT808Package.Header, new JT808_0x8001()
            {
                MsgId = requestInfo.JT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = requestInfo.JT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0100(JT808RequestInfo requestInfo, JT808Session<JT808RequestInfo> session)
        {
            return new JT808_0x8100Package(requestInfo.JT808Package.Header, new JT808_0x8100()
            {
                Code = "J" + requestInfo.JT808Package.Header.TerminalPhoneNo,
                JT808TerminalRegisterResult = JT808TerminalRegisterResult.成功,
                MsgNum = requestInfo.JT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0200(JT808RequestInfo requestInfo, JT808Session<JT808RequestInfo> session)
        {
            ((JT808Server)session.AppServer)?.ProducerFactory.CreateProducer((ushort)JT808MsgId.位置信息汇报).ProduceAsync("", requestInfo.OriginalBuffer);
            return new JT808_0x8001Package(requestInfo.JT808Package.Header, new JT808_0x8001()
            {
                MsgId = requestInfo.JT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = requestInfo.JT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0704(JT808RequestInfo requestInfo, JT808Session<JT808RequestInfo> session)
        {
            return new JT808_0x8001Package(requestInfo.JT808Package.Header, new JT808_0x8001()
            {
                MsgId = requestInfo.JT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = requestInfo.JT808Package.Header.MsgNum
            });
        }
    }
}
