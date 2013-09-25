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
            if (Readtxt.Length == 10)
            {
                Conf.generator_settings = Readtxt[0].Substring(Readtxt[0].IndexOf("=") + 1);
                Conf.level_name = Readtxt[1].Substring(Readtxt[1].IndexOf("=") + 1);
                Conf.server_port = Readtxt[2].Substring(Readtxt[2].IndexOf("=") + 1);
                Conf.level_seed = Readtxt[3].Substring(Readtxt[3].IndexOf("=") + 1);
                Conf.server_ip = Readtxt[4].Substring(Readtxt[4].IndexOf("=") + 1);
                Conf.white_list = Readtxt[5].Substring(Readtxt[5].IndexOf("=") + 1);
                Conf.gamemode = Readtxt[6].Substring(Readtxt[6].IndexOf("=") + 1);
                Conf.max_players = Readtxt[7].Substring(Readtxt[7].IndexOf("=") + 1);
                Conf.motd = Readtxt[8].Substring(Readtxt[8].IndexOf("=") + 1);
                Conf.motd = Readtxt[9].Substring(Readtxt[9].IndexOf("=") + 1);
            }
            else
            {
                File.Delete("Config.cgf");
             //   Console.WriteLine("Error to read configuration." + Constants.vbCrLf + "Click Enter to restart...");
                Console.Read();
            }
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