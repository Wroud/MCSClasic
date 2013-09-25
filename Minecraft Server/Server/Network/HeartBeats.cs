using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network
{
    class HeartBeats
    {

        static System.Timers.Timer time = new System.Timers.Timer();
        public static void Start()
        {
            time.Interval = 1000;
            time.Start();
            time.Elapsed += Time_tick;
        }
        private static void Time_tick(System.Object sender, System.EventArgs e)
        {
            // Тут надо отослать тупо get запрос с параметрами.   P.S пример на vb ниже
          //  ServerConf.Config Conf = default(ServerConf.Config);
          //  WebClient http = new WebClient();
         //  http.DownloadString("https://minecraft.net/heartbeat.jsp" + "?port=" + Conf.server_port + "&max=" + Conf.max_players + "&name=" + Conf.motd + "&public=" + Conf.white_list + "&version=7" + "&salt=" + Conf.Salt + "&users=0"));
        }
    }
}
