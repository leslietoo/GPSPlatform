using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;

namespace GPS.CoordinatesTransformation
{
    /// <summary>
    /// wgs84-gcj02服务
    /// </summary>
    public class Wgs2MarsService
    {
        private readonly static double[] Lx = new double[297000];

        private readonly static double[] Ly = new double[297000];

        static Wgs2MarsService()
        {
            Assembly assm = Assembly.GetExecutingAssembly();
            string str="";
            List<string> points = new List<string>();
            using(StreamReader sr=new StreamReader(assm.GetManifestResourceStream("GPS.CoordinatesTransformation.resources.txt")))
            {
                str = sr.ReadLine();
                while (!string.IsNullOrEmpty(str))
                {
                    points.Add(str);
                    str = sr.ReadLine();
                }
            }
            int num = 0;
            foreach (string s in points)
            {
                if (Lx[num] == 0.0)
                {
                    Lx[num] = (double)int.Parse(s) / 100000.0;
                }
                else
                {
                    Ly[num] = (double)int.Parse(s) / 100000.0;
                    num++;
                }
            }
        }

        private int ID(int i, int j)
        {
            return i + 660 * j;
        }

        public bool Wgs2Mars(double latwgs, double lngwgs, out double latmars, out double lngmars)
        {
            double num = lngwgs;
            double num2 = latwgs;
            for (long num3 = 0L; num3 < 10; num3++)
            {
                if (num < 72.0 || num > 137.9 || num2 < 10.0 || num2 > 54.9)
                {
                    latmars = latwgs;
                    lngmars = lngwgs;
                    return false;
                }
                int num4 = (int)Math.Floor((num - 72.0) * 10.0);
                int num5 = (int)Math.Floor((num2 - 10.0) * 10.0);
                double num6 = Lx[ID(num4, num5)];
                double num7 = Ly[ID(num4, num5)];
                double num8 = Lx[ID(num4 + 1, num5)];
                double num9 = Ly[ID(num4 + 1, num5)];
                double num10 = Lx[ID(num4 + 1, num5 + 1)];
                double num11 = Ly[ID(num4 + 1, num5 + 1)];
                double num12 = Lx[ID(num4, num5 + 1)];
                double num13 = Ly[ID(num4, num5 + 1)];
                double num14 = (num - 72.0 - 0.1 * (double)num4) * 10.0;
                double num15 = (num2 - 10.0 - 0.1 * (double)num5) * 10.0;
                double num16 = (1.0 - num14) * (1.0 - num15) * num6 + num14 * (1.0 - num15) * num8 + num14 * num15 * num10 + (1.0 - num14) * num15 * num12 - num;
                double num17 = (1.0 - num14) * (1.0 - num15) * num7 + num14 * (1.0 - num15) * num9 + num14 * num15 * num11 + (1.0 - num14) * num15 * num13 - num2;
                num = (num + lngwgs + num16) / 2.0;
                num2 = (num2 + latwgs + num17) / 2.0;
            }
            lngmars = Math.Round(num, 6);
            latmars = Math.Round(num2, 6);
            return true;
        }

        public bool Mars2Wgs(double latmars, double lngmars, out double latwgs, out double lngwgs)
        {
            double num = lngmars;
            double num2 = latmars;
            for (long num3 = 0L; num3 < 10; num3++)
            {
                if (num < 72.0 || num > 137.9 || num2 < 10.0 || num2 > 54.9)
                {
                    latwgs = latmars;
                    lngwgs = lngmars;
                    return false;
                }
                int num4 = (int)Math.Floor((num - 72.0) * 10.0);
                int num5 = (int)Math.Floor((num2 - 10.0) * 10.0);
                double num6 = Lx[ID(num4, num5)];
                double num7 = Ly[ID(num4, num5)];
                double num8 = Lx[ID(num4 + 1, num5)];
                double num9 = Ly[ID(num4 + 1, num5)];
                double num10 = Lx[ID(num4 + 1, num5 + 1)];
                double num11 = Ly[ID(num4 + 1, num5 + 1)];
                double num12 = Lx[ID(num4, num5 + 1)];
                double num13 = Ly[ID(num4, num5 + 1)];
                double num14 = (num - 72.0 - 0.1 * (double)num4) * 10.0;
                double num15 = (num2 - 10.0 - 0.1 * (double)num5) * 10.0;
                double num16 = (1.0 - num14) * (1.0 - num15) * num6 + num14 * (1.0 - num15) * num8 + num14 * num15 * num10 + (1.0 - num14) * num15 * num12 - num;
                double num17 = (1.0 - num14) * (1.0 - num15) * num7 + num14 * (1.0 - num15) * num9 + num14 * num15 * num11 + (1.0 - num14) * num15 * num13 - num2;
                num = (num + lngmars - num16) / 2.0;
                num2 = (num2 + latmars - num17) / 2.0;
            }
            lngwgs = Math.Round(num, 6);
            latwgs = Math.Round(num2, 6);
            return true;
        }
    }
}
