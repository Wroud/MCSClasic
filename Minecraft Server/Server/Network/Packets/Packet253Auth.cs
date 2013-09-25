using Craft.Net.Common;
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
    class Packet253Auth : Framework.Network.Packet
    {
        private byte opcode = 253;
        private byte[] token;
        private byte[] key;
        private string sid;

        public Packet253Auth(TcpClientm data)
        {
            this.data = data;
        }

        public Packet253Auth(TcpClientm data,string s, byte[] k, byte[] t)
        {
            this.data = data;
            this.sid = s;
            this.key = k;
            this.token = t;
        }
        public override void Write()
        {
            this.data.Write = true;
            this.data.Write(this.opcode);
            WriteString(this.data, this.sid);
            WriteBytes(this.data, this.key);
            WriteBytes(this.data, this.token);
            this.data.Flush();
        }
    }
}
