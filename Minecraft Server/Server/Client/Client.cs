﻿using Minecraft_Server.Framework.Network;
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
        }

        public void onSharedKey(byte[] s, byte[] v)
        {
        }

        public void onPing(byte s, string name)
        {
        }

        public void onJoin(byte protocolVersion)
        {
        }

        public void onKick()
        {
            //Net.SendPacket(254);
        }
    }
}
