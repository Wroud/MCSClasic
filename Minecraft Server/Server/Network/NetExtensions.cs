using Minecraft_Server.Framework.Network;
using Minecraft_Server.Framework.Util;
using Minecraft_Server.Server.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server
{
    public static class NetExtensions
    {
        public static void Flush(this TcpClientm tc)
        {
            byte[] data = tc.buffer.ToArray();
            int pos = (int)tc.buffer.Position;
            tc.Write = false;
            tc.buffer.Position = 0;
            try
            {
                tc.NetStream.WriteAsync(data, 0, pos);
            }
            catch
            {
                Log.Error("Ошибка записи пакета пользователю {0}", tc.id);
            }
        }

        public static byte[] Reverse(this byte[] tc)
        {
            byte[] n = new byte[tc.Length];
            int pos = tc.Length - 1;
            foreach (byte b in tc)
            {
                n[pos] = b;
                pos--;
            }
            tc = null;
            return n;
        }
        public static void Write(this TcpClientm tc, string n)
        {
            byte[] data = UnicodeEncoding.ASCII.GetBytes(n.ToCharArray());
            tc.write.Write(data, 0, data.Length);
            for (int i = 0; i < 64 - data.Length; i++)
                tc.write.Write((byte)32);
        }
        public static void Write(this TcpClientm tc, byte n)
        {
            tc.write.Write(n);
        }
        public static void Write(this TcpClientm tc, sbyte n)
        {
            tc.write.Write(n);
        }
        public static void Write(this TcpClientm tc, byte[] n)
        {
            tc.write.Write(n);
        }
        public static void Write(this TcpClientm tc, short n)
        {
            byte[] data = new[]
            {
                (byte)((n & 0xFF00) >> 8),
                (byte)(n & 0xFF)
            };
            tc.write.Write(data, 0, data.Length);
        }
        public static void Write(this TcpClientm tc, int n)
        {
            byte[] data = new[]
            {
                (byte)((n & 0xFF000000) >> 24),
                (byte)((n & 0xFF0000) >> 16),
                (byte)((n & 0xFF00) >> 8),
                (byte)(n & 0xFF)
            };
            tc.write.Write(data, 0, data.Length);
        }
        public unsafe static void Write(this TcpClientm tc, float n)
        {
            int nn = *(int*)&n;
            byte[] data = new[]
            {
                (byte)((nn & 0xFF000000) >> 24),
                (byte)((nn & 0xFF0000) >> 16),
                (byte)((nn & 0xFF00) >> 8),
                (byte)(nn & 0xFF)
            };
            tc.write.Write(data, 0, data.Length);
        }
        public static void Write(this TcpClientm tc, long n)
        {
            byte[] data = new[]
            {
                (byte)(((ulong)n & 0xFF00000000000000) >> 56),
                (byte)((n & 0xFF000000000000) >> 48),
                (byte)((n & 0xFF0000000000) >> 40),
                (byte)((n & 0xFF00000000) >> 32),
                (byte)((n & 0xFF000000) >> 24),
                (byte)((n & 0xFF0000) >> 16),
                (byte)((n & 0xFF00) >> 8),
                (byte)(n & 0xFF)
            };
            tc.write.Write(data, 0, data.Length);
        }

        public static string ReadString(this NetworkStream reader, TcpClientm tc)
        {
            return new string(UnicodeEncoding.ASCII.GetChars(reader.ReadBytes(tc, 64))).TrimEnd();
        }

        public static byte ReadByte(this NetworkStream reader, TcpClientm tc)
        {
            byte[] b = new byte[1];
            reader.Read(b, 0, 1);
            return b[0];
        }

        public static byte[] ReadBytes(this NetworkStream reader, TcpClientm tc, int count)
        {
            byte[] b = new byte[count];
            reader.Read(b, 0, count);
            return b;
        }

        public static short ReadInt16(this NetworkStream reader, TcpClientm tc)
        {
            int count = 2;
            byte[] b = new byte[count];
            reader.Read(b, 0, count);
            b = b.Reverse();
            return BitConverter.ToInt16(b, 0);
        }

        public static int ReadInt32(this NetworkStream reader, TcpClientm tc)
        {
            int count = 4;
            byte[] b = new byte[count];
            reader.Read(b, 0, count);
            b = b.Reverse();
            return BitConverter.ToInt32(b, 0);
        }

        public static long ReadInt64(this NetworkStream reader, TcpClientm tc)
        {
            int count = 8;
            byte[] b = new byte[count];
            reader.Read(b, 0, count);
            b = b.Reverse();
            return BitConverter.ToInt64(b, 0);
        }




        public static void Write(this TcpClientm st, Vector3 v)
        {
            st.Write(v.X);
            st.Write(v.Y);
            st.Write(v.Z);
        }
        public static void Write(this TcpClientm st, Vector2 v)
        {
            st.write.Write(new byte[] { v.X, v.Y }, 0, 2);
        }





        public static void Write(this Stream st, Vector2 v)
        {
            st.Write(new byte[] { v.X, v.Y }, 0, 2);
        }
        public static Vector2 ReadVector2(this Stream st)
        {
            byte[] b = new byte[2];
            st.Read(b, 0, 2);
            return new Vector2(b[0], b[1]);
        }





        public static void Write(this Stream st, Vector3 v)
        {
            byte[] d1 = new[]
            {
                (byte)((v.X & 0xFF00) >> 8),
                (byte)(v.X & 0xFF)
            };
            byte[] d2 = new[]
            {
                (byte)((v.Y & 0xFF00) >> 8),
                (byte)(v.Y & 0xFF)
            };
            byte[] d3 = new[]
            {
                (byte)((v.Z & 0xFF00) >> 8),
                (byte)(v.Z & 0xFF)
            };
            st.Write(d1, 0, 2);
            st.Write(d2, 0, 2);
            st.Write(d3, 0, 2);
        }
        public static Vector3 ReadVector3(this Stream st)
        {
            byte[] b = new byte[6];
            st.Read(b, 0, 6);
            short x = BitConverter.ToInt16(new byte[] { b[1], b[0] }, 0);
            short y = BitConverter.ToInt16(new byte[] { b[3], b[2] }, 0);
            short z = BitConverter.ToInt16(new byte[] { b[5], b[4] }, 0);
            return new Vector3(x, y, z);
        }
    }
}
