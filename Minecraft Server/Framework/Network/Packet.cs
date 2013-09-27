using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Framework.Network
{
    abstract class Packet
    {
        #region Abstract
        public TcpClientm data;
        public abstract void Write();
        #endregion


        public static byte[] ReadBytes(TcpClientm d)
        {
            short lenght = d.NetStream.ReadInt16(d);
            return d.NetStream.ReadBytes(d, lenght);
        }

        public static void WriteBytes(TcpClientm d, byte[] b)
        {
            if (b != null)
            {
                d.Write((short)b.Length);
                d.Write(b);
            }
            else
                d.Write((short)0);
        }

        public static void WriteString(TcpClientm d, string s)
        {
            d.Write((short)s.Length);
            if (s.Length > 0)
                d.Write(s);
        }
    }
}
