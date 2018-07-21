using JT808.Protocol.Enums;
using JT808.Protocol.MessageBodyReply;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808PackageImpl.Reply
{
    /// <summary>
    /// 终端通用应答包
    /// </summary>
    public class JT808_0x0001Package : JT808PackageBase<JT808_0x0001>
    {
        public JT808_0x0001Package(JT808Header jT808Header, int msgNum, JT808_0x0001 bodies, JT808GlobalConfigs jT808GlobalConfigs) : base(jT808Header, msgNum, bodies, jT808GlobalConfigs)
        {
        }

        protected override JT808Package Create(JT808Header jT808Header, int msgNum, JT808_0x0001 bodies, JT808GlobalConfigs jT808GlobalConfigs)
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header();
            jT808Package.Header.DataLength = 5;
            jT808Package.Header.MsgId = JT808MsgId.终端通用应答;
            jT808Package.Header.MsgNum = msgNum;
            jT808Package.Header.TerminalPhoneNo = jT808Header.TerminalPhoneNo;
            jT808Package.Bodies = bodies;
            jT808Package.CommonWriteBuffer(jT808GlobalConfigs);
            return jT808Package;
        }
    }
}
