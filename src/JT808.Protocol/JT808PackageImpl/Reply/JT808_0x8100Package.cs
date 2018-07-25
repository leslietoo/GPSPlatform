using JT808.Protocol.Enums;
using JT808.Protocol.MessageBodyReply;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808PackageImpl.Reply
{
    /// <summary>
    /// 终端注册应答
    /// </summary>
    public class JT808_0x8100Package : JT808PackageBase<JT808_0x8100>
    {
        public JT808_0x8100Package(JT808Header jT808Header, int msgNum, JT808_0x8100 bodies, JT808GlobalConfigs jT808GlobalConfigs) : base(jT808Header, msgNum, bodies, jT808GlobalConfigs)
        {
        }

        protected override JT808Package Create(JT808Header jT808Header, int msgNum, JT808_0x8100 bodies, JT808GlobalConfigs jT808GlobalConfigs)
        {
            bodies.WriteBuffer(jT808GlobalConfigs);
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header();
            jT808Package.Header.MessageBodyProperty = new JT808MessageBodyProperty(bodies.Buffer.Length);
            jT808Package.Header.MsgId = JT808MsgId.终端注册应答;
            jT808Package.Header.MsgNum = (ushort)msgNum;
            jT808Package.Header.TerminalPhoneNo = jT808Header.TerminalPhoneNo;
            jT808Package.Header.WriteBuffer(jT808GlobalConfigs);
            jT808Package.Bodies = bodies;
            jT808Package.WriteBuffer(jT808GlobalConfigs);
            return jT808Package;
        }
    }
}
