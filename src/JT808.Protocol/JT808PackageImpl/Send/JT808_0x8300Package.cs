using JT808.Protocol.Enums;
using JT808.Protocol.MessageBodySend;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808PackageImpl.Send
{
    public class JT808_0x8300Package : JT808PackageBase<JT808_0x8300>
    {
        public JT808_0x8300Package(JT808Header jT808Header, int msgNum, JT808_0x8300 bodies, JT808GlobalConfigs jT808GlobalConfigs) : base(jT808Header, msgNum, bodies, jT808GlobalConfigs)
        {
        }

        protected override JT808Package Create(JT808Header jT808Header, int msgNum, JT808_0x8300 bodies, JT808GlobalConfigs jT808GlobalConfigs)
        {
            bodies.WriteBuffer(jT808GlobalConfigs);
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header();
            jT808Package.Header.DataLength = bodies.Buffer.Length;
            jT808Package.Header.MsgId = JT808MsgId.文本信息下发;
            jT808Package.Header.MsgNum = msgNum;
            jT808Package.Header.TerminalPhoneNo = jT808Header.TerminalPhoneNo;
            jT808Package.Header.WriteBuffer(jT808GlobalConfigs);
            jT808Package.Bodies = bodies;
            jT808Package.WriteBuffer(jT808GlobalConfigs);
            return jT808Package;
        }
    }
}
