using System.Diagnostics;
using System.Threading;

namespace Minecraft_Server.Framework.Util
{
    class Utils
    {
        public static void TimeOut(ref bool status, int ms)
        {
            if (!status)
                return;
            Stopwatch st = new Stopwatch();
            st.Start();
            while (status)
            {
                if (st.ElapsedMilliseconds > ms)
                {
                    st.Stop();
                    return;
                }
                Thread.Sleep(1);
            }
        }
    }
}
