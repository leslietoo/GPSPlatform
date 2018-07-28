using JT808.Protocol.Extensions;
using JT808.Protocol.JT808RequestProperties;
using JT808.Protocol.MessageBodyRequest;
using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using MessagePack;
using MessagePack.Formatters;
using Protocol.Common.Extensions;
using System;
using System.Collections.Generic;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0200Formatter : IMessagePackFormatter<JT808_0x0200>
    {
        public JT808_0x0200 Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808_0x0200 jT808_0X0200 = new JT808_0x0200();
            jT808_0X0200.AlarmFlag = BinaryExtensions.ReadInt32Little(bytes, offset);
            offset += 4;
            jT808_0X0200.StatusFlag = BinaryExtensions.ReadInt32Little(bytes, offset);
            offset += 4;
            jT808_0X0200.Lat = BinaryExtensions.ReadInt32Little(bytes, offset);
            offset += 4;
            jT808_0X0200.Lng = BinaryExtensions.ReadInt32Little(bytes, offset);
            JT808StatusProperty jT808StatusProperty = new JT808StatusProperty(Convert.ToString(jT808_0X0200.StatusFlag, 2).PadLeft(32, '0'));
            if (jT808StatusProperty.Bit28 == '1')//西经
            {
                jT808_0X0200.Lng = -jT808_0X0200.Lng;
            }
            if (jT808StatusProperty.Bit29 == '1')//南纬
            {
                jT808_0X0200.Lat = -jT808_0X0200.Lat;
            }
            offset += 4;
            jT808_0X0200.Altitude = BinaryExtensions.ReadUInt16Little(bytes, offset);
            offset += 2;
            jT808_0X0200.Speed = BinaryExtensions.ReadUInt16Little(bytes, offset);
            offset += 2;
            jT808_0X0200.Direction = BinaryExtensions.ReadUInt16Little(bytes, offset);
            offset += 2;
            jT808_0X0200.GPSTime=BinaryExtensions.ReadDateTimeLittle(bytes, offset, 6);
            offset += 6;
            // 位置附加信息
            jT808_0X0200.JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>();
            if (bytes.Length > 28)
            {
                int attachOffset = 0;
                ReadOnlyMemory<byte> locationAttachMemory = bytes;
                ReadOnlySpan<byte> locationAttachSpan = locationAttachMemory.Span.Slice(28);
                while (locationAttachSpan.Length > attachOffset)
                {
                    int attachId = 1;
                    int attachLen = 1;
                    try
                    {
                        Type jT808LocationAttachType;
                        if (JT808LocationAttachBase.JT808LocationAttachMethod.TryGetValue(locationAttachSpan[attachOffset], out jT808LocationAttachType))
                        {
                            int attachContentLen = locationAttachSpan[attachOffset + 1];
                            int locationAttachTotalLen = attachId + attachLen + attachContentLen;
                            byte[] attachBuffer = locationAttachSpan.Slice(attachOffset, locationAttachTotalLen).ToArray();
                            object attachImplObj = formatterResolver.GetFormatterDynamic(jT808LocationAttachType);
                            dynamic attachImpl = JT808FormatterResolverExtensions.JT808DynamicDeserialize(attachImplObj, attachBuffer, attachOffset, formatterResolver,out readSize);
                            attachOffset = attachOffset + locationAttachTotalLen;
                            jT808_0X0200.JT808LocationAttachData.Add(attachImpl.AttachInfoId, attachImpl);
                        }
                        else
                        {
                            int attachContentLen = locationAttachSpan[attachOffset + 1];
                            int locationAttachTotalLen = attachId + attachLen + attachContentLen;
                            attachOffset = attachOffset + locationAttachTotalLen;
                        }
                    }
                    catch (Exception ex)
                    {
                        int attachContentLen = locationAttachSpan[attachOffset + 1];
                        int locationAttachTotalLen = attachId + attachLen + attachContentLen;
                        attachOffset = attachOffset + locationAttachTotalLen;
                    }
                }
                offset= offset + attachOffset;
            }
            readSize = offset;
            return jT808_0X0200;
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
                        object attachImplObj = formatterResolver.GetFormatterDynamic(item.Value.GetType());
                        offset = JT808FormatterResolverExtensions.JT808DynamicSerialize(attachImplObj, ref bytes, offset, item.Value, formatterResolver);
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
