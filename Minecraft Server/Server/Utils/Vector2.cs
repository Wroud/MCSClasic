
namespace Minecraft_Server.Server.Utils
{
    public class Vector2
    {
        public byte X;
        public byte Y;
        public Vector2()
        {
            this.X = 0;
            this.Y = 0;
        }
        public Vector2(byte x, byte y)
        {
            this.X = x;
            this.Y = y;
        }
        public static Vector2 operator +(Vector2 one, Vector2 two)
        {
            one.X += two.X;
            one.Y += two.Y;
            return one;
        }
        public static Vector2 operator -(Vector2 one, Vector2 two)
        {
            one.X -= two.X;
            one.Y -= two.Y;
            return one;
        }
        public static Vector2 operator *(Vector2 one, byte two)
        {
            one.X *= two;
            one.Y *= two;
            return one;
        }
        public static Vector2 operator /(Vector2 one, byte two)
        {
            one.X /= two;
            one.Y /= two;
            return one;
        }
    }
}
