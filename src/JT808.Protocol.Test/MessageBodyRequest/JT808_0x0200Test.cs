using JT808.Protocol.MessageBodyRequest;
using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Protocol.Common.Extensions;
using JT808.Protocol.Test.JT808LocationAttach;
using System.IO;
using MessagePack;
using MessagePack.Formatters;
using MessagePack.Resolvers;
using JT808.Protocol.JT808Formatters;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using JT808.Protocol.Test.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using JT808.Protocol.Enums;

namespace JT808.Protocol.Test.MessageBodyRequest
{
    public class JT808_0x0200Test : JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200();
            jT808UploadLocationRequest.AlarmFlag = 1;
            jT808UploadLocationRequest.Altitude = 40;
            jT808UploadLocationRequest.GPSTime = DateTime.Parse("2018-07-15 10:10:10");
            jT808UploadLocationRequest.Lat = 12222222;
            jT808UploadLocationRequest.Lng = 132444444;
            jT808UploadLocationRequest.Speed = 60;
            jT808UploadLocationRequest.Direction = 0;
            jT808UploadLocationRequest.StatusFlag = 2;
            jT808UploadLocationRequest.JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>();
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x01, new JT808LocationAttachImpl0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x02, new JT808LocationAttachImpl0x02
            {
                Oil = 55
            });
            var hex = MessagePackSerializer.Serialize(jT808UploadLocationRequest).ToHexString();
        }

        [Fact]
        public void Test2()
        {
            byte[] bodys = "00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = MessagePackSerializer.Deserialize<JT808_0x0200>(bodys);
            Assert.Equal(1, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal(2, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(100, ((JT808LocationAttachImpl0x01)jT808UploadLocationRequest.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x01]).Mileage);
            Assert.Equal(55, ((JT808LocationAttachImpl0x02)jT808UploadLocationRequest.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x02]).Oil);
        }

        [Fact]
        public void Test3()
        {
            //00 00 00 01 
            //00 00 00 02 
            //00 BA 7F 0E 
            //07 E4 F1 1C 
            //00 28 
            //00 3C 
            //00 00 
            //18 07 15 10 10 10 
            //01 
            //  04 
            //      00 00 00 64 
            //02 
            //  02 
            //      00 37 
            //06
            //  0D 
            //      00 00 00 12 01 00 00 00 00 00 00 00 00"
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200();
            jT808UploadLocationRequest.AlarmFlag = 1;
            jT808UploadLocationRequest.Altitude = 40;
            jT808UploadLocationRequest.GPSTime = DateTime.Parse("2018-07-15 10:10:10");
            jT808UploadLocationRequest.Lat = 12222222;
            jT808UploadLocationRequest.Lng = 132444444;
            jT808UploadLocationRequest.Speed = 60;
            jT808UploadLocationRequest.Direction = 0;
            jT808UploadLocationRequest.StatusFlag = 2;
            jT808UploadLocationRequest.JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>();
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x01, new JT808LocationAttachImpl0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x02, new JT808LocationAttachImpl0x02
            {
                Oil = 55
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(0x06, new JT808LocationAttachImpl0x06
            {
                Age = 18,
                Gender = 1,
                UserName = "smallchi"
            });
            var hex = MessagePackSerializer.Serialize(jT808UploadLocationRequest).ToHexString();
        }

        static JT808_0x0200Test()
        {
            JT808LocationAttachBase.AddJT808LocationAttachMethod<JT808LocationAttachImpl0x06>(0x06);
            MessagePack.Resolvers.CompositeResolver.RegisterAndSetAsDefault(
               new IMessagePackFormatter[]
               {
                   // for example, register reflection infos(can not serialize in default)
                   new JT808MessageBodyPropertyFormatter(),
                   new JT808PackageFromatter(),
                   new JT808HeaderFormatter(),
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
                   new JT808_0x0200_0x06Formatter(),
               },
               new IFormatterResolver[]
               {
                    ContractlessStandardResolver.Instance
               });
        }

        [Fact]
        public void Test4()
        {
            // 1.添加自定义附加信息扩展 AddJT808LocationAttachMethod 
            byte[] bodys = "00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37 06 0D 00 00 00 12 01 73 6D 61 6C 6C 63 68 69".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = MessagePackSerializer.Deserialize<JT808_0x0200>(bodys);
            Assert.Equal(1, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal(2, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(100, ((JT808LocationAttachImpl0x01)jT808UploadLocationRequest.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x01]).Mileage);
            Assert.Equal(55, ((JT808LocationAttachImpl0x02)jT808UploadLocationRequest.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x02]).Oil);
            Assert.Equal(18, ((JT808LocationAttachImpl0x06)jT808UploadLocationRequest.JT808LocationAttachData[0x06]).Age);
            Assert.Equal(1, ((JT808LocationAttachImpl0x06)jT808UploadLocationRequest.JT808LocationAttachData[0x06]).Gender);
            Assert.Equal("smallchi", ((JT808LocationAttachImpl0x06)jT808UploadLocationRequest.JT808LocationAttachData[0x06]).UserName);
        }

        [Fact]
        public void Test5_1()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                 MsgId= Enums.JT808MsgId.位置信息汇报,
                 MsgNum=8888,
                 TerminalPhoneNo="112233445566",
                 MessageBodyProperty=new JT808MessageBodyProperty(38),
            };
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200();
            jT808UploadLocationRequest.AlarmFlag = 1;
            jT808UploadLocationRequest.Altitude = 40;
            jT808UploadLocationRequest.GPSTime = DateTime.Parse("2018-07-15 10:10:10");
            jT808UploadLocationRequest.Lat = 12222222;
            jT808UploadLocationRequest.Lng = 132444444;
            jT808UploadLocationRequest.Speed = 60;
            jT808UploadLocationRequest.Direction = 0;
            jT808UploadLocationRequest.StatusFlag = 2;
            jT808UploadLocationRequest.JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>();
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x01, new JT808LocationAttachImpl0x01
            {
                Mileage = 100
            });
            jT808UploadLocationRequest.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x02, new JT808LocationAttachImpl0x02
            {
                Oil = 55
            });
            jT808Package.Bodies = jT808UploadLocationRequest;
            var hex = MessagePackSerializer.Serialize(jT808Package).ToHexString();
        }

        [Fact]
        public void Test5()
        {
            //7E 
            //02 00 
            //00 33 
            //11 22 33 44 55 66 
            //22 B8 
            //00 00 00 01 
            //00 00 00 02 
            //00 BA 7F 0E 
            //07 E4 F1 1C 
            //00 28 
            //00 3C 
            //00 00 
            //18 07 15 10 10 10 
            //01 
            //  04 
            //     00 00 00 64 
            //02 
            //   02 
            //     00 37 
            //42 7E
            byte[] bytes = "7E 02 00 00 26 11 22 33 44 55 66 22 B8 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 00 3C 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37 57 7E".ToHexBytes();
            var jT808Package = MessagePackSerializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.位置信息汇报, jT808Package.Header.MsgId);

            Assert.Equal(38, jT808Package.Header.MessageBodyProperty.DataLength);
            Assert.Equal(8888, jT808Package.Header.MsgNum);
            Assert.Equal("112233445566", jT808Package.Header.TerminalPhoneNo);
            //Assert.Equal(1, jT808Package.Header.MessageBodyProperty.DataLength);
            Assert.False(jT808Package.Header.MessageBodyProperty.IsPackge);
            Assert.Equal(0, jT808Package.Header.MessageBodyProperty.PackageIndex);
            Assert.Equal(0, jT808Package.Header.MessageBodyProperty.PackgeCount);
            Assert.Equal(JT808EncryptMethod.None, jT808Package.Header.MessageBodyProperty.Encrypt);

            JT808_0x0200 jT808UploadLocationRequest = (JT808_0x0200)jT808Package.Bodies;
            Assert.Equal(1, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal(2, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(100, ((JT808LocationAttachImpl0x01)jT808UploadLocationRequest.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x01]).Mileage);
            Assert.Equal(55, ((JT808LocationAttachImpl0x02)jT808UploadLocationRequest.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x02]).Oil);
        }

        [Fact]
        public void Test6()
        {
            byte[] bytes = "7E 02 00 00 6D 01 35 10 26 00 01 2A 9C 00 00 00 00 00 08 00 01 01 57 99 5C 06 CA 26 AC 02 72 00 00 01 48 18 07 22 16 01 10 01 04 00 00 80 73 10 01 63 2A 02 00 00 30 01 17 56 02 0A 00 53 31 06 01 CC 00 24 93 16 97 41 01 CC 00 26 39 13 BB 47 01 CC 00 24 93 14 03 47 01 CC 00 24 93 16 98 4A 01 CC 00 26 39 12 54 4B 01 CC 00 24 93 12 67 4F 57 08 00 00 00 00 00 00 00 00 66 7E".ToHexBytes();
            // 没有解出位置信息自定义附加协议字段  只解出标准附加协议
            //"7E 
            //02 00 
            //00 6D 
            //01 35 10 26 00 01 
            //2A 9C 
            //00 00 00 00 
            //00 08 00 01 
            //01 57 99 5C 
            //06 CA 26 AC 
            //02 72 
            //00 00 
            //01 48 
            //18 07 22 16 01 10 
            //01 
            //  04 
            //      00 00 80 73 
            //2A 
            //  02 
            //      00 00 
            //30 
            //  01 
            //      17 
            //01 
            //7E

            //7E 
            //02 00 
            //00 6D 
            //01 35 10 26 00 01 
            //2A 9C 
            //00 00 00 00 
            //00 08 00 01 
            //01 57 99 5C 
            //06 CA 26 AC 
            //02 72 
            //00 00 
            //01 48 
            //18 07 22 16 01 10 
            //01 
            //04 
            //00 00 80 73 
            //10 
            //01 
            //63 
            //2A 
            //02 
            //00 00 
            //30 
            //01 
            //17 
            //56 
            //02 
            //0A 00 
            //53 
            //31 
            //06 01 CC 00 24 93 
            //16 97 41 01 CC 00 
            //26 39 13 BB 47 01 
            //CC 00 24 93 14 03 
            //47 01 CC 00 24 93 
            //16 98 4A 01 CC 00 
            //26 39 12 54 4B 01 
            //CC 00 24 93 12 67 4F 
            //57 
            //08 
            //00 00 00 00 00 00 00 00 
            //66 
            //7E
            JT808Package jT808Package = new JT808Package();
            var bytes1 = MessagePackSerializer.Serialize(jT808Package);
            string hex = bytes1.ToHexString();
        }
    }
}
