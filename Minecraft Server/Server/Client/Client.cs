using Minecraft_Server.Framework.Network;
using Minecraft_Server.Server.Main;
using Minecraft_Server.Server.Network.Packets;
using Minecraft_Server.Server.Util;
using Minecraft_Server.Server.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Client
{
    class Client : Framework.Client.Client
    {

        private Network.TcpClientm Net;
        public string verfikey;
        public Player player;

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

        public void onCBlock(Vector3 pos, byte mod, byte type)
        {
            World.worlds[this.player.level][pos] = (mod == 0) ? (byte)0 : type;
            foreach (var us in Network.Network.net.connects.Values)
                if (us.id != Net.id && ((Minecraft_Server.Server.Network.TcpClientm)us).cli.player.level == this.player.level)
                {
                    new Packet6SetBlock((Minecraft_Server.Server.Network.TcpClientm)us, pos, (mod == 0) ? (byte)0 : type).Write();
                }
        }

        public void onPosition(Vector3 pos,Vector2 rot)
        {
            this.player.Position = pos;
            this.player.Rotation = rot;
        }

        public void onMessage(byte color, string mes)
        {
            mes = "[&a" + this.player.username + "&f]: " + mes;
            foreach (var us in Network.Network.net.connects.Values)
                new Packet13Message((Minecraft_Server.Server.Network.TcpClientm)us, (sbyte)this.Net.id, mes).Write();
        }

        public void onPing(byte s, string name)
        {
        }

        public void onJoin(byte protocolVersion, string name, string vk)
        {
            if (protocolVersion != 7)
                new PacketKick(this.Net, "Bad protocol version").Write();

            player = new Player(name);
            this.verfikey = vk;

            new Packet0Indentification(this.Net, protocolVersion, 0);//0x64 будет оп
            Thread.Sleep(10);
            new Packet2Level(this.Net).Write();
            Thread.Sleep(10);
            byte[] da = new byte[1024];
            byte[] gz = World.worlds[player.level].GetGzipMap();
            for (int point = 0; point < gz.Length; )
            {
                if (point + 1024 < gz.Length)
                    Array.Copy(gz, point, da, 0, 1024);
                else
                    Array.Copy(gz, point, da, 0, gz.Length - point);
                new Packet3Chunk(this.Net, da, (byte)(point / gz.Length * 100)).Write();
                Thread.Sleep(10);
                point += 1024;
            }
            new Packet4LevelFin(this.Net, World.worlds[player.level].size).Write();
            Thread.Sleep(10);
            new Packet7Spawn(this.Net, (sbyte)-1, player.username, World.worlds[player.level].spawn, new Vector2()).Write();

            foreach (var us in Network.Network.net.connects.Values)
                if (us.id != Net.id && ((Minecraft_Server.Server.Network.TcpClientm)us).cli.player.level == player.level)
                {
                    new Packet7Spawn(Net, (sbyte)us.id, ((Minecraft_Server.Server.Network.TcpClientm)us).cli.player.username, ((Minecraft_Server.Server.Network.TcpClientm)us).cli.player.Position, ((Minecraft_Server.Server.Network.TcpClientm)us).cli.player.Rotation).Write();
                    new Packet7Spawn((Minecraft_Server.Server.Network.TcpClientm)us, (sbyte)Net.id, player.username, World.worlds[player.level].spawn, new Vector2()).Write();
                }
        }

        public void onKick()
        {
            //Net.SendPacket(254);
        }
    }
}
