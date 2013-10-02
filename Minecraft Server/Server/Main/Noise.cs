using System;

namespace Minecraft_Server.Server.Main
{
    class Noise
    {
        public int Seed;
        public int mx, mz;

        public Noise(int s, int x, int z)
        {
            Seed = s;
            mx = x;
            mz = z;
        }

        public double GNoise(int x, int y, int z, int o = 2, double a = 1, double f = 0.04, double p = 0.7)
        {
            //returns -1 to 1
            double total = 0.0;

            int ox = x, oy = y, oz = z;
            double b1 = 0, b2 = 0, b3 = 0, b4 = 0, b5 = 0, b6 = 0, b7 = 0, b8 = 0;
            bool fir = false;

            for (int i = 0; i < o; ++i)
            {
                total = total + Smooth(x * f, y * f, z * f, fir, ref ox, ref oy, ref oz, ref b1, ref b2, ref b3, ref b4, ref b5, ref b6, ref b7, ref b8) * a;
                f *= 2;
                a *= p;
                fir = true;
            }
            if (total < -2.4)
                total = -2.4;
            else if (total > 2.4)
                total = 2.4;

            return (total / 2.4);
        }

        public double NoiseGeneration(int x, int y, int z)
        {
            int n = (y * mz + z) * mx + x;

            return (1.0 - ((n * (n * n * 15731 + 789221) + Seed) & 0x7fffffff) / 1073741824.0);
        }

        public double Interpolate(double x, double y, double v)
        {
            return x * (1 - v) + y * v;
        }

        public double Interpolate(double a)
        {
            return (1 - Math.Cos(a * Math.PI)) * 0.5;
        }

        public double Smooth(double x, double y, double z, bool fir, ref int ox, ref int oy, ref int oz, ref double b1, ref double b2, ref double b3, ref double b4, ref double b5, ref double b6, ref double b7, ref double b8)
        {
            double n1, n2, n3, n4, n5, n6, n7, n8, i1, i2, i3, i4, i5, i6, v;
            int ix, iy, iz;

            ix = (int)x;
            iy = (int)y;
            iz = (int)z;

            if (ix == ox && iy == oy && iz == oz && fir)
            {
                n1 = b1;
                n2 = b2;
                n3 = b3;
                n4 = b4;
                n5 = b5;
                n6 = b6;
                n7 = b7;
                n8 = b8;
            }
            else
            {
                int px = ix + ((x < 0) ? -1 : 1);
                int py = iy + ((y < 0) ? -1 : 1);
                int pz = iz + ((z < 0) ? -1 : 1);
                b1 = n1 = NoiseGeneration(ix, iy, iz);
                b2 = n2 = NoiseGeneration(px, iy, iz);
                b3 = n3 = NoiseGeneration(ix, py, iz);
                b4 = n4 = NoiseGeneration(px, py, iz);

                b5 = n5 = NoiseGeneration(ix, iy, pz);
                b6 = n6 = NoiseGeneration(px, iy, pz);
                b7 = n7 = NoiseGeneration(ix, py, pz);
                b8 = n8 = NoiseGeneration(px, py, pz);
            }

            v = Interpolate(x - ix);
            i1 = Interpolate(n1, n2, v);
            i2 = Interpolate(n3, n4, v);
            i4 = Interpolate(n5, n6, v);
            i5 = Interpolate(n7, n8, v);
            v = Interpolate(y - iy);
            i3 = Interpolate(i1, i2, v);
            i6 = Interpolate(i4, i5, v);

            v = Interpolate(z - iz);

            ox = ix;
            oy = iy;
            oz = iz;

            return Interpolate(i3, i6, v);
        }

    }
}
