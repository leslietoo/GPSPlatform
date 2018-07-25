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

namespace JT808.Protocol.Test.MessageBodyRequest
{
    public class JT808_0x0200Test: JT808PackageBase
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
            jT808UploadLocationRequest.WriteBuffer(jT808GlobalConfigs);
            string hex = jT808UploadLocationRequest.Buffer.ToArray().ToHexString();
        }

        [Fact]
        public void Test2()
        {
            byte[] bodys = "00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 02 58 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200(bodys);
            jT808UploadLocationRequest.ReadBuffer(jT808GlobalConfigs);
            Assert.Equal(1, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12.222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132.444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal(2, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(100, ((JT808LocationAttachImpl0x01)jT808UploadLocationRequest.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x01]).Mileage);
            Assert.Equal(55, ((JT808LocationAttachImpl0x02)jT808UploadLocationRequest.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x02]).Oil);
        }

        [Fact]
        public void Test3()
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
            jT808UploadLocationRequest.JT808LocationAttachData.Add(0x06, new JT808LocationAttachImpl0x06
            {
                 Age=18,
                 Gender=1,
                 UserName="smallchi"
            });
            jT808UploadLocationRequest.WriteBuffer(jT808GlobalConfigs);
            string hex = jT808UploadLocationRequest.Buffer.ToArray().ToHexString();
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
           

            byte[] bodys = "00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 02 58 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37 06 0D 73 6D 61 6C 6C 63 68 69 00 00 00 12 01".ToHexBytes();
            JT808_0x0200 jT808UploadLocationRequest = new JT808_0x0200(bodys);
            jT808UploadLocationRequest.ReadBuffer(jT808GlobalConfigs);
            Assert.Equal(1, jT808UploadLocationRequest.AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808UploadLocationRequest.GPSTime);
            Assert.Equal(12.222222, jT808UploadLocationRequest.Lat);
            Assert.Equal(132.444444, jT808UploadLocationRequest.Lng);
            Assert.Equal(60, jT808UploadLocationRequest.Speed);
            Assert.Equal(2, jT808UploadLocationRequest.StatusFlag);
            Assert.Equal(100, ((JT808LocationAttachImpl0x01)jT808UploadLocationRequest.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x01]).Mileage);
            Assert.Equal(55, ((JT808LocationAttachImpl0x02)jT808UploadLocationRequest.JT808LocationAttachData[JT808LocationAttachBase.AttachId0x02]).Oil);
            Assert.Equal(18, ((JT808LocationAttachImpl0x06)jT808UploadLocationRequest.JT808LocationAttachData[0x06]).Age);
            Assert.Equal(1, ((JT808LocationAttachImpl0x06)jT808UploadLocationRequest.JT808LocationAttachData[0x06]).Gender);
            Assert.Equal("smallchi", ((JT808LocationAttachImpl0x06)jT808UploadLocationRequest.JT808LocationAttachData[0x06]).UserName);
        }

        [Fact]
        public void Test5()
        {
            byte[] bytes = "7E 02 00 00 26 01 38 12 34 56 78 00 85 00 00 00 01 00 00 00 02 00 CB 73 55 00 DE 19 A3 00 22 02 30 00 22 18 07 19 13 59 23 01 04 00 00 00 0C 02 02 00 20 6E 7E".ToHexBytes();
            JT808Package jT808Package = new JT808Package(bytes);
            jT808Package.ReadBuffer(jT808GlobalConfigs);
        }

        [Fact]
        public void Test6()
        {
            byte[] bytes = "7E 02 00 00 3D 01 35 10 26 00 01 04 7D 02 00 00 00 00 00 08 00 03 01 57 8E 40 06 CA 39 66 FF AE 00 14 00 00 18 07 21 02 31 17 01 04 00 00 80 52 10 01 63 2A 02 00 00 30 01 14 31 01 09 56 02 0A 00 57 08 00 00 00 00 00 00 00 00 89 7E".ToHexBytes();
            //"7E 02 00 3D 01 35 10 26 00 01 04 7E 00 90 01 7E"


            JT808Package jT808Package = new JT808Package(bytes);
            jT808Package.ReadBuffer(jT808GlobalConfigs);

            var bytes1 = MessagePackSerializer.Serialize(jT808Package);
            string hex = bytes1.ToHexString();
        }

        [Fact]
        public void Test7()
        {
            Span<byte> bytes = "02 00 00 3D 01 35 10 26 00 01 00 A6 00 00 00 00 00 08 00 03 01 57 8F 45 06 CA 39 10 FF 84 00 14 00 00 18 07 20 08 41 00 01 04 00 00 77 B5 10 01 63 2A 02 00 00 30 01 17 31 01 06 56 02 0A 00 57 08 00 00 00 00 00 00 00 00".ToHexBytes();
            var result = bytes.ToXor(0, bytes.Length);
        }

        [Fact]
        public void Test8()
        {
            Span<byte> bytes = "02 00 00 3D 01 35 10 26 00 01 04 7D 02 00 00 00 00 00 08 00 03 01 57 8E 40 06 CA 39 66 FF AE 00 14 00 00 18 07 21 02 31 17 01 04 00 00 80 52 10 01 63 2A 02 00 00 30 01 14 31 01 09 56 02 0A 00 57 08 00 00 00 00 00 00 00 00".ToHexBytes();
            var result = bytes.ToXor(0, bytes.Length);
        }
    }
}
