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
        public static void Start()
        {
            http = new WebClient();
            Log.Info("HeartBeat запущен");
            while (true)
            {
                Time_tick();
                Thread.Sleep(45000);
            }
        }
        private static void Time_tick()
        {
            http.DownloadData("https://minecraft.net/heartbeat.jsp" + "?port=" + ServerConf.Config.server_port + "&max=" + ServerConf.Config.max_players + "&name=" + ServerConf.Config.motd + "&public=" + ServerConf.Config.white_list + "&version=7" + "&salt=" + ServerConf.Config.Salt + "&users=0");
        }
    }
}
