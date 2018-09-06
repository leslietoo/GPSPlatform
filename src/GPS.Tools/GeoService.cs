using System;
using System.Collections.Generic;
using System.Text;

namespace GPS.Tools
{
    /// <summary>
    /// 地理位置经纬度服务
    /// </summary>
    public class GeoService
    {
        /// <summary>
        /// 计算两点间距离
        /// </summary>
        /// <param name="prevLng"></param>
        /// <param name="prevLat"></param>
        /// <param name="curLng"></param>
        /// <param name="curLat"></param>
        /// <returns></returns>
        public double GetDisBetweenTwoPoint(double prevLng, double prevLat, double curLng, double curLat)
        {
            double t = 0.0;
            if (prevLng == 0 || prevLat == 0 || curLng == 0 || curLat == 0) return 0;
            double alpha1 = prevLng / 180 * Math.PI;
            double alpha2 = curLng / 180 * Math.PI;
            double beta1 = prevLng / 180 * Math.PI;
            double beta2 = curLat / 180 * Math.PI;

            t = (2 * Math.Asin(Math.Sqrt(Math.Sin((beta1 - beta2) / 2) * Math.Sin((beta1 - beta2) / 2) +
                 Math.Cos(beta1) * Math.Cos(beta2) * Math.Sin((alpha1 - alpha2) / 2) * Math.Sin((alpha1 - alpha2) / 2)))) * 6371.004;
            double result = Math.Round(t, 2) * 1.03;
            return result;
        }
    }
}
