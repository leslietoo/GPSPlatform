using System;
using Xunit;

namespace GPS.CoordinatesTransformation.Test
{
    public class Wgs2MarsServiceTest
    {
        [Fact]
        public void Test1()
        {
            Wgs2MarsService wgs2MarsService = new Wgs2MarsService();
        }

        [Fact]
        public void Test2()
        {
            //"Lat":22.518108,"Lng":113.91146
            Wgs2MarsService wgs2MarsService = new Wgs2MarsService();
            wgs2MarsService.Wgs2Mars(22.518108, 113.911468, out double d, out double c);
        }

        [Fact]
        public void Test3()
        {
            //"Lat":22.518108,"Lng":113.91146
            Wgs2MarsService wgs2MarsService = new Wgs2MarsService();
            wgs2MarsService.Wgs2Mars(22.518108, 113.911468, out double d, out double c);
            wgs2MarsService.Mars2Wgs(d, c, out double d1, out double c1);
        }
    }
}
