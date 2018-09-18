using DotNetty.Transport.Channels;
using JT808.Protocol;
using JT808.Protocol.Enums;
using JT808.Protocol.JT808PackageImpl.Reply;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;

namespace GPS.JT808NettyServer
{
    /// <summary>
    /// 消息处理业务
    /// </summary>
    public class JT808MsgIdHandler
    {
        private readonly SessionManager sessionManager;
        /// <summary>
        /// 初始化消息处理业务
        /// </summary>
        public JT808MsgIdHandler(SessionManager sessionManager)
        {
            this.sessionManager = sessionManager;
            HandlerDict = new Dictionary<JT808MsgId, Func<JT808RequestInfo, IChannelHandlerContext,IJT808Package>>
            {
                {JT808MsgId.终端鉴权, Msg0x0102},
                {JT808MsgId.终端心跳, Msg0x0002},
                {JT808MsgId.终端注销, Msg0x0003},
                {JT808MsgId.终端注册, Msg0x0100},
                {JT808MsgId.位置信息汇报,Msg0x0200 },
                {JT808MsgId.定位数据批量上传,Msg0x0704 }
            };
        }

        public Dictionary<JT808MsgId, Func<JT808RequestInfo, IChannelHandlerContext, IJT808Package>> HandlerDict { get; }

        private IJT808Package Msg0x0102(JT808RequestInfo requestInfo, IChannelHandlerContext context)
        {
            sessionManager.RegisterSession(new JT808Session(context.Channel, requestInfo.JT808Package.Header.TerminalPhoneNo));
            return new JT808_0x8001Package(requestInfo.JT808Package.Header, new JT808_0x8001()
            {
                MsgId = requestInfo.JT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = requestInfo.JT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0002(JT808RequestInfo requestInfo, IChannelHandlerContext context)
        {
            sessionManager.Heartbeat(requestInfo.JT808Package.Header.TerminalPhoneNo);
            return new JT808_0x8001Package(requestInfo.JT808Package.Header, new JT808_0x8001()
            {
                MsgId = requestInfo.JT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = requestInfo.JT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0003(JT808RequestInfo requestInfo, IChannelHandlerContext context)
        {
            sessionManager.RemoveSessionByTerminalPhoneNo(requestInfo.JT808Package.Header.TerminalPhoneNo);
            return new JT808_0x8001Package(requestInfo.JT808Package.Header, new JT808_0x8001()
            {
                MsgId = requestInfo.JT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = requestInfo.JT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0100(JT808RequestInfo requestInfo, IChannelHandlerContext context)
        {
            return new JT808_0x8100Package(requestInfo.JT808Package.Header, new JT808_0x8100()
            {
                Code = "J" + requestInfo.JT808Package.Header.TerminalPhoneNo,
                JT808TerminalRegisterResult = JT808TerminalRegisterResult.成功,
                MsgNum = requestInfo.JT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0200(JT808RequestInfo requestInfo, IChannelHandlerContext context)
        {
            return new JT808_0x8001Package(requestInfo.JT808Package.Header, new JT808_0x8001()
            {
                MsgId = requestInfo.JT808Package.Header.MsgId,
                JT808PlatformResult = JT808PlatformResult.Success,
                MsgNum = requestInfo.JT808Package.Header.MsgNum
            });
        }

        private IJT808Package Msg0x0704(JT808RequestInfo requestInfo, IChannelHandlerContext context)
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
