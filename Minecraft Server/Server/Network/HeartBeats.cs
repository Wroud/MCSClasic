using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using Minecraft_Server.Framework.Util;

namespace Minecraft_Server.Server.Network
{
    class HeartBeats
    {
        public static WebClient http;
        public static Thread thread;
        public static void Start()
        {
            thread = new Thread(Thr);
            http = new WebClient();
            thread.Start();
            Log.Info("HeartBeat запущен");
        }
        private static void Thr()
        {
            while (true)
            {
                http.DownloadData("https://minecraft.net/heartbeat.jsp" 
                    + "?port=" + ServerConf.Config.server_port 
                    + "&max=" + ServerConf.Config.max_players 
                    + "&name=" + ServerConf.Config.motd 
                    + "&public=" + ServerConf.Config.white_list 
                    + "&version=7" 
                    + "&salt=" + ServerConf.Config.Salt 
                    + "&users=0");
                Thread.Sleep(45000);
            }
        }
    }
}
