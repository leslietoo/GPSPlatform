using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using JT808.Protocol.MessageBodyReply;
using JT808.Protocol.MessageBodyRequest;
using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using System;
using System.Text;

namespace JT808.Protocol
{
    public static class JT808GlobalConfigs
    {
        static JT808GlobalConfigs()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// 根据安装后终端自身的手机号转换。手机号不足 12 位，则在前补充数字，大陆手机号补充数字 0，港澳台则根据其区号进行位数补充
        /// </summary>
        public static JT808PhoneNumberType jT808PhoneNumber = JT808PhoneNumberType.大陆;

        public static void RegisterMessagePackFormatter(params IMessagePackFormatter[] jT808LocationAttachFormatter)
        {
            var formatters = new IMessagePackFormatter[]
            {
                   // for example, register reflection infos(can not serialize in default)
                   new JT808PackageFromatter(),
                   new JT808HeaderFormatter(),
                   new JT808HeaderMessageBodyPropertyFormatter(),
                   new JT808_0x0001Formatter(),
                   new JT808_0x8001Formatter(),
                   new JT808_0x8100Formatter(),
                   new JT808_0x0704Formatter(),
                   new JT808_0x0200Formatter(),
                   new JT808_0x0200_0x01Formatter(),
                   new JT808_0x0200_0x02Formatter(),
                   new JT808_0x0200_0x03Formatter(),
                   new JT808_0x0200_0x04Formatter(),
                   new JT808_0x0200_0x11Formatter(),
                   new JT808_0x0200_0x12Formatter(),
                   new JT808_0x0200_0x13Formatter(),
                   new JT808_0x0200_0x25Formatter(),
                   new JT808_0x0200_0x2AFormatter(),
                   new JT808_0x0200_0x2BFormatter(),
                   new JT808_0x0200_0x30Formatter(),
                   new JT808_0x0200_0x31Formatter(),
            };
            var resolvers = new IFormatterResolver[]
            {
                  ContractlessStandardResolver.Instance
            };
            if (jT808LocationAttachFormatter != null)
            {
                Array.Copy(jT808LocationAttachFormatter, formatters, jT808LocationAttachFormatter.Length);
            }
            CompositeResolver.RegisterAndSetAsDefault(formatters, resolvers);
        }

        public static void RegisterJT808LocationAttach<TJT808LocationAttach>(byte sttachInfoId)
               where TJT808LocationAttach : JT808LocationAttachBase
        {
            JT808LocationAttachBase.AddJT808LocationAttachMethod<TJT808LocationAttach>(sttachInfoId);
        }
    }
}
