using JT808.Protocol.MessageBodyRequest;
using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using MessagePack;
using MessagePack.Formatters;
using Protocol.Common.Extensions;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0200Formatter : IMessagePackFormatter<JT808_0x0200>
    {
        public JT808_0x0200 Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            throw new NotImplementedException();
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0200 value, IFormatterResolver formatterResolver)
        {
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.AlarmFlag);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.StatusFlag);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.Lat);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.Lng);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.Altitude);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.Speed);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.Direction);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.GPSTime);
            if (value.JT808LocationAttachData != null && value.JT808LocationAttachData.Count > 0)
            {
                foreach (var item in value.JT808LocationAttachData)
                {
                    try
                    {
                        //offset += formatterResolver.GetFormatter<JT808LocationAttachBase>().Serialize(ref bytes, offset, item.Value, formatterResolver);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return offset;
        }
    }
}
