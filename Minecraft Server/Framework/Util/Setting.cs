using System;
//using System.Collections.Generic;
using System.Text;
//using System.Threading.Tasks;
using System.IO;


namespace Minecraft_Server.Framework.Util
{
    class Setting
    {
# region "SettingsStructure"
       public struct Config
        {
         public string generator_settings ;
         public string level_name ;
         public string server_port;
         public string level_seed;
         public string server_ip;
         public string white_list;
         public string gamemode;
         public string max_players;
         public string motd;
         public string Salt;
        }
# endregion
        public static void Start()
        {
           Config Conf = new Config(); 
           string[] ReadTxt;
           if (File.Exists("Config.cgf"))
           {
               ReadTxt = File.ReadAllLines("Config.cgf");
               if (ReadTxt.Length == 10)
               {
                   Conf.generator_settings = ReadTxt[0].Substring(ReadTxt[0].IndexOf("=") + 1);
                   Conf.level_name = ReadTxt[1].Substring(ReadTxt[1].IndexOf("=") + 1);
                   Conf.server_port = ReadTxt[2].Substring(ReadTxt[2].IndexOf("=") + 1);
                   Conf.level_seed = ReadTxt[3].Substring(ReadTxt[3].IndexOf("=") + 1);
                   Conf.server_ip = ReadTxt[4].Substring(ReadTxt[4].IndexOf("=") + 1);
                   Conf.white_list = ReadTxt[5].Substring(ReadTxt[5].IndexOf("=") + 1);
                   Conf.gamemode = ReadTxt[6].Substring(ReadTxt[6].IndexOf("=") + 1);
                   Conf.max_players = ReadTxt[7].Substring(ReadTxt[7].IndexOf("=") + 1);
                   Conf.motd = ReadTxt[8].Substring(ReadTxt[8].IndexOf("=") + 1);
                   Conf.Salt = ReadTxt[9].Substring(ReadTxt[9].IndexOf("=") + 1);
                   Log.Info("Настройки сервера загружены");
               }
               else
               {
                   File.Delete("Config.cgf");
                   Log.Error("Ошибка в чтении конфигурации сервера");
                   Log.Error("Нажмите Enter для перезапуска ...");
                   Console.Read();
               }
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
           }
        }
    }
}
