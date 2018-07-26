using JT808.Protocol.Enums;
using System.Text;

namespace JT808.Protocol
{
    public  class JT808GlobalConfigs
    {
        static JT808GlobalConfigs()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// 字符串编码
        /// </summary>
        public Encoding JT808Encoding { get; set; } = Encoding.GetEncoding("GBK");

        /// <summary>
        /// 根据安装后终端自身的手机号转换。手机号不足 12 位，则在前补充数字，大陆手机号补充数字 0，港澳台则根据其区号进行位数补充
        /// </summary>
        public JT808PhoneNumberType jT808PhoneNumber { get; set; } = JT808PhoneNumberType.大陆;
    }
}
