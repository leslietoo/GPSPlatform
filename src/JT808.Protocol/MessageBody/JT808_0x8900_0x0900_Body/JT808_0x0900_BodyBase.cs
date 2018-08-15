using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBody.JT808_0x8900_0x0900_Body
{
    public abstract class JT808_0x0900_BodyBase
    {
        public static IDictionary<byte, Type> JT808_0x0900Method { get; private set; }

        static JT808_0x0900_BodyBase()
        {
            JT808_0x0900Method = new Dictionary<byte, Type>();
        }

        public static void AddJT808LocationAttachMethod<JT808_0x0900_Body>(byte passthroughType)
            where JT808_0x0900_Body : JT808_0x0900_BodyBase
        {
            JT808_0x0900Method.Add(passthroughType, typeof(JT808_0x0900_Body));
        }
    }
}
