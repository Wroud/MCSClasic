using System;
using System.Collections;
using System.Collections.Generic;
using Minecraft_Server.Server.Client;
using Minecraft_Server.Server.Main;
using Minecraft_Server.Server.Network;
using Minecraft_Server.Framework.Util;
using System.IO;
using System.Threading;
public class ServerConf
{

    #region "ConfigStructure"
    public struct Config
    {
        public static string generator_settings = "Nothing";
        public static string level_name = "world";
        public static string server_port = "25565";
        public static string level_seed = "22";
        public static string server_ip = "127.0.0.1";
        public static string white_list = "true";
        public static string gamemode = "0";
        public static string max_players = "20";
        public static string motd = "A Minecraft Server";
        public static string Salt = "fhddfhdfhdfhdfh";
    }
    #endregion

    public static void Start()
    {

        string[] Readtxt = null;
        if (File.Exists("Config.cgf"))
        {
            Readtxt = File.ReadAllLines("Config.cgf");
            Dictionary<string, string> vars = new Dictionary<string, string>();
            foreach (string len in Readtxt)
            {
                string[] sp = len.Split('=');
                vars.Add(sp[0].Replace(" ",""), sp[1]);
            }
            Config.generator_settings = vars["generator-settings"];
            Config.level_name = vars["level-name"];
            Config.server_port = vars["server-port"];
            Config.level_seed = vars["level-seed"];
            Config.server_ip = vars["server-ip"];
            Config.white_list = vars["white-list"];
            Config.gamemode = vars["gamemode"];
            Config.max_players = vars["max-players"];
            Config.motd = vars["motd"];
            Config.Salt = vars["salt"];
            Log.Info("Конфигурация сервера загружена");
        }
        else
        {
            string[] Writetxt = new string[10];
            Writetxt[0] = "generator-settings="+Config.generator_settings;
            Writetxt[1] = "level-name=" + Config.level_name;
            Writetxt[2] = "server-port=" + Config.server_port;
            Writetxt[3] = "level-seed=" + Config.level_seed;
            Writetxt[4] = "server-ip=" + Config.server_ip;
            Writetxt[5] = "white-list=" + Config.white_list;
            Writetxt[6] = "gamemode=" + Config.gamemode;
            Writetxt[7] = "max-players=" + Config.max_players;
            Writetxt[8] = "motd=" + Config.motd;
            Writetxt[9] = "salt=" + Config.Salt;
            File.WriteAllLines("Config.cgf", Writetxt);
            Log.Info("Записан Config.cgf");
        }
    }
}