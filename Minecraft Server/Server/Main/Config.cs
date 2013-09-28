using System;
using System.Collections;
using System.Collections.Generic;
using Minecraft_Server.Server.Client;
using Minecraft_Server.Server.Main;
using Minecraft_Server.Server.Network;
using Minecraft_Server.Framework.Util;
using System.IO;
using System.Threading;

namespace Minecraft_Server.Server.Util
{
    public struct Config
    {
        public static string generator_settings = "Nothing";
        public static string level_name = "world";
        public static string server_name = "DaunDaun";
        public static string server_port = "25565";
        public static string level_seed = "22";
        public static string server_ip = "127.0.0.1";
        public static string white_list = "true";
        public static string gamemode = "0";
        public static string max_players = "20";
        public static string motd = "A Minecraft Server";
        public static string Salt = "fhddfhdfhdfhdfh";
    }
}