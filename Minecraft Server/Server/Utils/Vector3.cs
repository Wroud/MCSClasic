
namespace Minecraft_Server.Server.Utils
{
    public class Vector3
    {
        public short X;
        public short Y;
        public short Z;
        public Vector3()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }
        public Vector3(short x, short y, short z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public int Mnog
        {
            get { return this.X * this.Y * this.Z; }
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
        public static Vector3 operator *(Vector3 one, short two)
        {
            one.X *= two;
            one.Y *= two;
            one.Z *= two;
            return one;
        }
        public static Vector3 operator /(Vector3 one, short two)
        {
            one.X /= two;
            one.Y /= two;
            one.Z /= two;
            return one;
        }
    }
}
