using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Minecraft_Server.Framework.Util
{
    class Utils
    {
        public static void TimeOut(ref bool status,int ms)
        {
            if (!status)
                return;
            Stopwatch st = new Stopwatch();
            st.Start();
            while (status)
                if (st.ElapsedMilliseconds > ms)
                {
                    Thread.Sleep(1);
                    st.Stop();
                    return;
                }
        }
    }
}
