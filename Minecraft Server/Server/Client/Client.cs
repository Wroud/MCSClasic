using Minecraft_Server.Server.Main;
using Minecraft_Server.Server.Network.Packets;
using Minecraft_Server.Server.Util;
using Minecraft_Server.Server.Utils;
using System;
using System.IO;

namespace Minecraft_Server.Server.Client
{
    partial class Client : Framework.Client.Client
    {

        public string username;
        public string level;
        public Vector2 Rotation;
        public Vector3 Position;
        public byte isop = 0;//0x64
        public string verfikey;
        public bool updated = false;

        public Client(Network.TcpClientm Net)
            : base(Net)
        {
            this.Net = Net;
        }

        public override void Close()
        {
            if (username != null && level != null)
            {
                Main.Main.players--;
                World.worlds[level].players.Remove((sbyte)Net.id);
                foreach (var us in Network.Network.net.connects.Values)
                    if (us.id != Net.id
                        && ((Server.Network.TcpClientm)us).cli.level == level)
                        new PacketDespawn((Server.Network.TcpClientm)us, (sbyte)this.Net.id).Write();

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
                World.worlds[level].players.Remove((sbyte)Net.id);
                this.level = world;
                this.Position = World.worlds[level].spawn;
                this.Rotation = new Vector2();
            }
            else
                if (World.MapExit(world))
                {
                    this.level = world;
                    if (!World.worlds.ContainsKey(level))
                        World.worlds.Add(level, new World(level));
                }
                else
                    this.level = Config.level_name;

            new Packet13Message(this.Net, (sbyte)-1, "&aNow you are in the " + level).Write();

            new Packet2Level(this.Net).Write();
            byte[] da = new byte[1024];
            byte[] gz = World.worlds[level].GetGzipMap();
            for (int point = 0; point < gz.Length; )
            {
                if (point + 1024 < gz.Length)
                    Array.Copy(gz, point, da, 0, 1024);
                else
                    Array.Copy(gz, point, da, 0, gz.Length - point);
                new Packet3Chunk(this.Net, da, (byte)((float)point / (float)gz.Length * 100)).Write();
                point += 1024;
            }
            new Packet4LevelFin(this.Net, World.worlds[level].size).Write();
            if (fw)
                new Packet8Position(this.Net, (sbyte)-1, Position, Rotation).Write();
            else
                new Packet7Spawn(this.Net, (sbyte)-1, username, Position, Rotation).Write();

            foreach (var us in Network.Network.net.connects.Values)
                if (us.id != Net.id && ((Server.Network.TcpClientm)us).cli.level == level)
                {
                    new Packet7Spawn(Net, (sbyte)us.id, ((Server.Network.TcpClientm)us).cli.username, ((Server.Network.TcpClientm)us).cli.Position, ((Server.Network.TcpClientm)us).cli.Rotation).Write();
                    new Packet7Spawn((Server.Network.TcpClientm)us, (sbyte)Net.id, username, Position, Rotation).Write();
                }
            World.worlds[level].players.Add((sbyte)Net.id);
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
    }
}
