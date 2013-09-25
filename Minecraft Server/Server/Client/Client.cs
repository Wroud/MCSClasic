using Craft.Net.Common;
using Minecraft_Server.Framework.Network;
using Minecraft_Server.Server.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Client
{
    class Client : Framework.Client.Client
    {

        private Network.TcpClientm Net;
        public string username { get; set; }
        public string region;
        public byte vievdis;
        public int chatvisible;
        public bool chatcolor;
        public byte diffuclity;
        public bool showCape;
        public Client(Network.TcpClientm Net)
            : base(Net)
        {
            this.Net = Net;
        }

        public void onConnect()
        {
            //Net.SendPacket(254);
            //Network.MessageQueue.Enqueue(packet);
        }

        public void onClientInfo(string s, byte dis, int chvs, bool ccol, byte dif, bool shc)
        {
            this.region = s;
            this.vievdis = dis;
            this.chatvisible = chvs;
            this.chatcolor = ccol;
            this.diffuclity = dif;
            this.showCape = shc;
        }

        public void onCommand(byte c)
        {
            var encodedKey = AsnKeyBuilder.PublicKeyToX509(Main.Main.ServerKey);
            byte[] shaData = Encoding.UTF8.GetBytes("-")
                .Concat(this.Net.SharedKey)
                .Concat(encodedKey.GetBytes()).ToArray();

            string hash = Cryptography.JavaHexDigest(shaData);
        }

        public void onSharedKey(byte[] s, byte[] v)
        {
            byte[] t = this.Net.token;
            var decryptedToken = Main.Main.CryptoServiceProvider.Decrypt(v, false);
            //if (!Array.Equals(t, decryptedToken))
                //new Packet255Kick(this.Net, "Bad token");
            //new Packet252SharedKey(this.Net, null, null).Write();

            this.Net.SharedKey = Main.Main.CryptoServiceProvider.Decrypt(s, false);
            this.Net.EncryptStream(this.Net.SharedKey);
        }

        public void onPing(byte s, string name)
        {
            string mes = "";
            if (s == 0)
            {
                mes = "сообщение" + "\u00a7" + 1 + "\u00a7" + 12;
            }
            else
            {
                mes = "\u00a7" + "1" + "\u0000" + "74" + "\u0000" + "1.6.2" + "\u0000" + "sesd sd ds" + "\u0000" + "1" + "\u0000" + "12" + "\u0000";
            }
            //new Packet255Kick(this.Net, mes).Write();
            Network.Network.CloseTcpClient(Net.id);
        }

        public void onJoin(byte protocolVersion)
        {
            //if (protocolVersion != 74)
                //new Packet255Kick(this.Net, "Bad Version");
            //else
            {
                var verifyToken = new byte[4];
                var csp = new RNGCryptoServiceProvider();
                csp.GetBytes(verifyToken);
                Net.token = verifyToken;

                var encodedKey = AsnKeyBuilder.PublicKeyToX509(Main.Main.ServerKey);
                //new Packet253Auth(this.Net, "-", encodedKey.GetBytes(), verifyToken).Write();
            }
        }

        public void onKick()
        {
            //Net.SendPacket(254);
        }
    }
}
