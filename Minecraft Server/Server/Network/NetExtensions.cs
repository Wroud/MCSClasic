using Minecraft_Server.Framework.Network;
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
            tc.NetStream.WriteAsync(data, 0, pos);
        }

        public static byte[] Reverse(this byte[] tc)
        {
            byte[] n = new byte[tc.Length];
            int pos = tc.Length-1;
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
            byte[] data = UnicodeEncoding.BigEndianUnicode.GetBytes(n.ToCharArray());
            if (tc.encrypted)
                data = tc.encryptCipher.ProcessBytes(data);
            tc.write.Write(data, 0, data.Length);
        }
        public static void Write(this TcpClientm tc, byte n)
        {
            if (tc.encrypted)
                n = tc.encryptCipher.ProcessByte(n)[0];
            tc.write.Write(n);
        }
        public static void Write(this TcpClientm tc, byte[] n)
        {
            if (tc.encrypted)
                n = tc.encryptCipher.ProcessBytes(n);
            tc.write.Write(n);
        }
        public static void Write(this TcpClientm tc, short n)
        {
            byte[] data = new[]
            {
                (byte)((n & 0xFF00) >> 8),
                (byte)(n & 0xFF)
            };
            if (tc.encrypted)
                data = tc.encryptCipher.ProcessBytes(data);
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
            if (tc.encrypted)
                data = tc.encryptCipher.ProcessBytes(data);
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
            if (tc.encrypted)
                data = tc.encryptCipher.ProcessBytes(data);
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
            if (tc.encrypted)
                data = tc.encryptCipher.ProcessBytes(data);
            tc.write.Write(data, 0, data.Length);
        }

        public static string ReadString(this NetworkStream reader, TcpClientm tc, short len)
        {
            string s = "";
            for (int i = 0; i < len; i++)
            {
                byte[] b = reader.ReadBytes(tc, 2);
                s += UnicodeEncoding.BigEndianUnicode.GetChars(b)[0];
            }
            return s;
        }

        public static byte ReadByte(this NetworkStream reader, TcpClientm tc)
        {
            byte[] b = new byte[1];
            reader.Read(b, 0, 1);
            if (tc.encrypted)
                return tc.decryptCipher.ProcessByte(b[0])[0];
            else
                return b[0];
        }

        public static byte[] ReadBytes(this NetworkStream reader, TcpClientm tc, int count)
        {
            byte[] b = new byte[count];
            reader.Read(b, 0, count);
            if (tc.encrypted)
                b = tc.decryptCipher.ProcessBytes(b);
            return b;
        }

        public static short ReadInt16(this NetworkStream reader, TcpClientm tc)
        {
            int count = 2;
            byte[] b = new byte[count];
            reader.Read(b, 0, count);
            if (tc.encrypted)
                b = tc.decryptCipher.ProcessBytes(b);
            b = b.Reverse();
            return BitConverter.ToInt16(b, 0);
        }

        public static int ReadInt32(this NetworkStream reader, TcpClientm tc)
        {
            int count = 4;
            byte[] b = new byte[count];
            reader.Read(b, 0, count);
            if (tc.encrypted)
                b = tc.decryptCipher.ProcessBytes(b);
            b = b.Reverse();
            return BitConverter.ToInt32(b, 0);
        }

        public static long ReadInt64(this NetworkStream reader, TcpClientm tc)
        {
            int count = 8;
            byte[] b = new byte[count];
            reader.Read(b, 0, count);
            if (tc.encrypted)
                b = tc.decryptCipher.ProcessBytes(b);
            b = b.Reverse();
            return BitConverter.ToInt64(b, 0);
        }
    }
}
