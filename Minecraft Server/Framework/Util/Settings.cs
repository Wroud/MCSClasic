using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
public class ServerConf
{

    #region "ConfigStructure"
    public struct Config
    {
        public string generator_settings;
        public string level_name;
        public string server_port;
        public string level_seed;
        public string server_ip;
        public string white_list;
        public string gamemode;
        public string max_players;
        public string motd;
        public string Salt;
    }
    #endregion

    public string Start()
    {

        Config Conf = new Config();
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
            Conf.generator_settings = vars["generator-settings"];
            Conf.level_name = vars["level-name"];
            Conf.server_port = vars["server-port"];
            Conf.level_seed = vars["level-seed"];
            Conf.server_ip = vars["server-ip"];
            Conf.white_list = vars["white-list"];
            Conf.gamemode = vars["gamemode"];
            Conf.max_players = vars["max-players"];
            Conf.motd = vars["motd"];
            Conf.Salt = vars["salt"];
            return "Configuration file is loading";
        }
        else
        {
            string[] Writetxt = new string[10];
            Writetxt[0] = "generator-settings=Nothing";
            Writetxt[1] = "level-name=world";
            Writetxt[2] = "server-port=25565";
            Writetxt[3] = "level-seed=22";
            Writetxt[4] = "server-ip=127.0.0.1";
            Writetxt[5] = "white-list=false";
            Writetxt[6] = "gamemode=0";
            Writetxt[7] = "max-players=20";
            Writetxt[8] = "motd=A Minecraft Server";
            Writetxt[9] = "salt=fhddfhdfhdfhdfh";
            File.WriteAllLines("Config.cgf", Writetxt);
            return "Configuration file create";
        }
    }
}