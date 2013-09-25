using Craft.Net.Common;
using Craft.Net.Networking;
using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet252SharedKey : Framework.Network.Packet
    {
        private byte opcode = 252;

        private byte[] sharedSecret;
        private byte[] verifyToken;

        public Packet252SharedKey(TcpClientm data)
        {
            this.data = data;
        }

        public Packet252SharedKey(TcpClientm d, byte[] s, byte[] v)
        {
            this.data = d;
            this.sharedSecret = s;
            this.verifyToken = v;
        }

        public static void Read(TcpClientm d)
        {
            byte[] s = ReadBytes(d);
            byte[] v = ReadBytes(d);
            d.cli.onSharedKey(s, v);
        }

        public override void Write()
        {
            this.data.Write = true;
            this.data.Write(this.opcode);
            WriteBytes(this.data, this.sharedSecret);
            WriteBytes(this.data, this.verifyToken);
            this.data.Flush();
        }
    }
}
