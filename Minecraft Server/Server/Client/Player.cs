using Minecraft_Server.Server.Util;
using Minecraft_Server.Server.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Client
{
    class Player
    {
        public string username;
        public string level;
        public Vector2 Rotation;
        public Vector3 Position;
        public byte isop = 0;//0x64

        public Player(string name)
        {
            this.level = Config.level_name;
            this.username = name;
            this.Rotation = new Vector2();
            this.Position = new Vector3();
        }

        public void Save()
        {
            Stream st = File.Create(username);
        }

        public void Load()
        {
        }
    }
}
