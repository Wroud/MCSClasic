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

        public string username;
        public string level;
        public Vector2 Rotation;
        public Vector3 Position;
        public byte isop = 0x64;//0x64

        private Network.TcpClientm Net;
        public string verfikey;

        public Client(Network.TcpClientm Net)
            : base(Net)
        {
            this.Net = Net;
        }

        public override void Close()
        {

            if (!Directory.Exists("Players\\"))
                Directory.CreateDirectory("Players\\");
            Stream st = File.Create("Players\\" + username);
            using (BinaryWriter bi = new BinaryWriter(st))
            {
                bi.Write(isop);
                bi.Write(level);
                bi.Write(Rotation);
                bi.Write(Position);
            }
        }

        public void ChangeWorld(string world, bool fw = false)
        {
            if (fw)
                foreach (var us in Network.Network.net.connects.Values)
                    if (us.id != Net.id
                        && ((Server.Network.TcpClientm)us).cli.level == level)
                        new PacketDespawn((Server.Network.TcpClientm)us, (sbyte)this.Net.id).Write();

            if (fw)
            {
                this.level = world;
                this.Position = World.worlds[level].spawn;
                this.Rotation = new Vector2();
            }
            else
                if (World.MapExit(world))
                {
                    if (!World.worlds.ContainsKey(world))
                        World.worlds.Add(world, new World(world));
                }
                else
                    this.level = world = Config.level_name;

            new Packet13Message(this.Net, (sbyte)-1, "&aNow you are in the " + level).Write();

            new Packet2Level(this.Net).Write();
            byte[] da = new byte[1024];
            byte[] gz = World.worlds[world].GetGzipMap();
            for (int point = 0; point < gz.Length; )
            {
                if (point + 1024 < gz.Length)
                    Array.Copy(gz, point, da, 0, 1024);
                else
                    Array.Copy(gz, point, da, 0, gz.Length - point);
                new Packet3Chunk(this.Net, da, (byte)((float)point / (float)gz.Length * 100)).Write();
                point += 1024;
            }
            new Packet4LevelFin(this.Net, World.worlds[world].size).Write();
            if (fw)
                new Packet8Position(this.Net, (sbyte)-1, Position, Rotation).Write();
            else
                new Packet7Spawn(this.Net, (sbyte)-1, username, Position, Rotation).Write();

            foreach (var us in Network.Network.net.connects.Values)
                if (us.id != Net.id && ((Server.Network.TcpClientm)us).cli.level == world)
                {
                    new Packet7Spawn(Net, (sbyte)us.id, ((Server.Network.TcpClientm)us).cli.username, ((Server.Network.TcpClientm)us).cli.Position, ((Server.Network.TcpClientm)us).cli.Rotation).Write();
                    new Packet7Spawn((Server.Network.TcpClientm)us, (sbyte)Net.id, username, Position, Rotation).Write();
                }
        }

        public void Load()
        {
            if (Directory.Exists("Players\\") && File.Exists("Players\\" + username))
            {
                Stream st = File.OpenRead("Players\\" + username);
                using (BinaryReader bi = new BinaryReader(st))
                {
                    isop = bi.ReadByte();
                    level = bi.ReadString();
                    if (World.MapExit(level))
                    {
                        Rotation = bi.ReadVector2();
                        Position = bi.ReadVector3();
                        this.Position += new Vector3(0, 52, 0);
                        if (!World.worlds.ContainsKey(level))
                            World.worlds.Add(level, new World(level));
                    }
                    else
                    {
                        this.level = Config.level_name;
                        this.Rotation = new Vector2();
                        this.Position = World.worlds[level].spawn;
                        this.Position += new Vector3(0, 52, 0);
                    }
                }
            }
            else
            {
                this.isop = 0;
                this.level = Config.level_name;
                this.Rotation = new Vector2();
                this.Position = World.worlds[level].spawn;
            }
        }

        public void onCBlock(Vector3 pos, byte mod, byte type)
        {
            World.worlds[level][pos] = (mod == 0) ? (byte)0 : type;
            foreach (var us in Network.Network.net.connects.Values)
                if (us.id != Net.id
                    && ((Server.Network.TcpClientm)us).cli.level == level)
                {
                    new Packet6SetBlock((Server.Network.TcpClientm)us, pos, (mod == 0) ? (byte)0 : type).Write();
                }
        }

        public void onPosition(Vector3 pos, Vector2 rot)
        {
            Position = pos;
            Rotation = rot;
        }

        public void onMessage(byte color, string mes)
        {
            string[] parts = mes.ToLower().Split(' ');
            switch (parts[0])
            {
                case "/world":
                    if (parts[1] != " " && parts[1] != level)
                        if (World.MapExit(parts[1]))
                        {
                            if (!World.worlds.ContainsKey(parts[1]))
                                World.worlds.Add(parts[1], new World(parts[1]));
                            ChangeWorld(parts[1], true);
                        }
                        else
                        {
                            if (parts.Length == 5)
                            {
                                World.worlds.Add(parts[1], new World(parts[1], new Vector3(Convert.ToInt16(parts[2]), Convert.ToInt16(parts[3]), Convert.ToInt16(parts[4]))));
                                ChangeWorld(parts[1], true);
                            }
                            else
                            {
                                new Packet13Message(this.Net, (sbyte)-1, "&aWorld " + parts[1] + " not found").Write();
                                new Packet13Message(this.Net, (sbyte)-1, "&aUse \"/world [name]\" for teleport to the world").Write();
                                new Packet13Message(this.Net, (sbyte)-1, "&aUse \"/world [name] [x] [y] [z]\" to create worlt").Write();
                                new Packet13Message(this.Net, (sbyte)-1, "&a with size x,y,z").Write();
                            }
                        }
                    break;
                default:
                    mes = "[&a" + username + "&f]: " + mes;
                    foreach (var us in Network.Network.net.connects.Values)
                        new Packet13Message((Server.Network.TcpClientm)us, (sbyte)this.Net.id, mes).Write();
                    break;
            }
        }

        public void onPing(byte s, string name)
        {
        }

        public void onJoin(byte protocolVersion, string name, string vk)
        {
            if (protocolVersion != 7)
                new PacketKick(this.Net, "Bad protocol version").Write();

            this.username = name;
            Load();
            this.verfikey = vk;

            new Packet0Indentification(this.Net, protocolVersion, 0x64);//0x64 будет оп
            ChangeWorld(this.level);
        }

        public void onKick()
        {
            //Net.SendPacket(254);
        }
    }
}
