using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Utils
{
    class Vector3
    {
        public double X;
        public double Y;
        public double Z;
        public Vector3()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }
        public Vector3(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public static Vector3 operator +(Vector3 one, Vector3 two)
        {
            one.X += two.X;
            one.Y += two.Y;
            one.Z += two.Z;
            return one;
        }
        public static Vector3 operator -(Vector3 one, Vector3 two)
        {
            one.X -= two.X;
            one.Y -= two.Y;
            one.Z -= two.Z;
            return one;
        }
        public static Vector3 operator *(Vector3 one, double two)
        {
            one.X *=two;
            one.Y *= two;
            one.Z *= two;
            return one;
        }
        public static Vector3 operator /(Vector3 one, double two)
        {
            one.X /= two;
            one.Y /= two;
            one.Z /= two;
            return one;
        }
    }
}
