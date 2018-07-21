using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Protocol.Common.Extensions;
using JT808.Protocol.MessageBodyRequest;
using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;

namespace JT808.Protocol.Test.MessageBodyRequest
{
    public class JT808_0x0704Test : JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            JT808_0x0704 jT808_0X0704 = new JT808_0x0704();
            jT808_0X0704.Count = 2;
            jT808_0X0704.LocationType = JT808_0x0704.BatchLocationType.正常位置批量汇报;
            jT808_0X0704.Positions = new List<JT808_0x0200>();

            JT808_0x0200 JT808_0x0200_1 = new JT808_0x0200();
            JT808_0x0200_1.AlarmFlag = 1;
            JT808_0x0200_1.Altitude = 40;
            JT808_0x0200_1.GPSTime = DateTime.Parse("2018-07-15 10:10:10");
            JT808_0x0200_1.Lat = 12.222222;
            JT808_0x0200_1.Lng = 132.444444;
            JT808_0x0200_1.Speed = 60;
            JT808_0x0200_1.Direction = 0;
            JT808_0x0200_1.StatusFlag = 2;
            JT808_0x0200_1.JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>();
            JT808_0x0200_1.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x01, new JT808LocationAttachImpl0x01
            {
                Mileage = 100
            });
            JT808_0x0200_1.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x02, new JT808LocationAttachImpl0x02
            {
                Oil = 55
            });
            jT808_0X0704.Positions.Add(JT808_0x0200_1);

            JT808_0x0200 JT808_0x0200_2 = new JT808_0x0200();
            JT808_0x0200_2.AlarmFlag = 2;
            JT808_0x0200_2.Altitude = 41;
            JT808_0x0200_2.GPSTime = DateTime.Parse("2018-07-15 10:10:30");
            JT808_0x0200_2.Lat = 13.333333;
            JT808_0x0200_2.Lng = 132.555555;
            JT808_0x0200_2.Speed = 54;
            JT808_0x0200_2.Direction = 120;
            JT808_0x0200_2.StatusFlag = 1;
            JT808_0x0200_2.JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>();
            JT808_0x0200_2.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x01, new JT808LocationAttachImpl0x01
            {
                Mileage = 96
            });
            JT808_0x0200_2.JT808LocationAttachData.Add(JT808LocationAttachBase.AttachId0x02, new JT808LocationAttachImpl0x02
            {
                Oil = 66
            });
            jT808_0X0704.Positions.Add(JT808_0x0200_2);
            jT808_0X0704.WriteBuffer(jT808GlobalConfigs);
            string hex = jT808_0X0704.Buffer.ToArray().ToHexString();
        }

        [Fact]
        public void Test2()
        {
            byte[] bodys = "00 02 00 00 26 00 00 00 01 00 00 00 02 00 BA 7F 0E 07 E4 F1 1C 00 28 02 58 00 00 18 07 15 10 10 10 01 04 00 00 00 64 02 02 00 37 00 26 00 00 00 02 00 00 00 01 00 CB 73 55 07 E6 A3 23 00 29 02 1C 00 78 18 07 15 10 10 30 01 04 00 00 00 60 02 02 00 42".ToHexBytes();
            JT808_0x0704 jT808_0X0704 = new JT808_0x0704(bodys);
            jT808_0X0704.ReadBuffer(jT808GlobalConfigs);
            Assert.Equal(2, jT808_0X0704.Count);
            Assert.Equal(JT808_0x0704.BatchLocationType.正常位置批量汇报, jT808_0X0704.LocationType);
            Assert.Equal(1, jT808_0X0704.Positions[0].AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:10"), jT808_0X0704.Positions[0].GPSTime);
            Assert.Equal(12.222222, jT808_0X0704.Positions[0].Lat);
            Assert.Equal(132.444444, jT808_0X0704.Positions[0].Lng);
            Assert.Equal(0, jT808_0X0704.Positions[0].Direction);
            Assert.Equal(60, jT808_0X0704.Positions[0].Speed);
            Assert.Equal(2, jT808_0X0704.Positions[0].StatusFlag);
            Assert.Equal(100, ((JT808LocationAttachImpl0x01)jT808_0X0704.Positions[0].JT808LocationAttachData[JT808LocationAttachBase.AttachId0x01]).Mileage);
            Assert.Equal(55, ((JT808LocationAttachImpl0x02)jT808_0X0704.Positions[0].JT808LocationAttachData[JT808LocationAttachBase.AttachId0x02]).Oil);

            Assert.Equal(2, jT808_0X0704.Positions[1].AlarmFlag);
            Assert.Equal(DateTime.Parse("2018-07-15 10:10:30"), jT808_0X0704.Positions[1].GPSTime);
            Assert.Equal(13.333333, jT808_0X0704.Positions[1].Lat);
            Assert.Equal(132.555555, jT808_0X0704.Positions[1].Lng);
            Assert.Equal(54, jT808_0X0704.Positions[1].Speed);
            Assert.Equal(120, jT808_0X0704.Positions[1].Direction);
            Assert.Equal(1, jT808_0X0704.Positions[1].StatusFlag);
            Assert.Equal(96, ((JT808LocationAttachImpl0x01)jT808_0X0704.Positions[1].JT808LocationAttachData[JT808LocationAttachBase.AttachId0x01]).Mileage);
            Assert.Equal(66, ((JT808LocationAttachImpl0x02)jT808_0X0704.Positions[1].JT808LocationAttachData[JT808LocationAttachBase.AttachId0x02]).Oil);
        }
    }
}
