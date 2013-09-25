using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet254ServerPing : Framework.Network.Packet
    {
        public byte opcode = 254;
        public byte succes;
        public string addres;

        public Packet254ServerPing(TcpClientm d, byte s, string a)
        {
            this.data = d;
            this.succes = s;
            this.addres = a;
        }

        public static void Read(TcpClientm d)
        {
            byte s;
            string a = "";
            try
            {
                s = d.NetStream.ReadByte(d);
                try
                {
                    d.NetStream.ReadByte(d);
                    ReadString(d, 255);
                    d.NetStream.ReadInt16(d);
                    s = d.NetStream.ReadByte(d);
                    if (s >= 73)
                    {
                        a = ReadString(d, 255);
                        int port = d.NetStream.ReadInt32(d);
                    }
                }
                catch
                {
                }
            }
            catch
            {
                s = 0;
            }
            d.cli.onPing(s, a);
        }
        public override void Write()
        {
        }
    }
}
