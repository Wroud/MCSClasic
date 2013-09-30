using Minecraft_Server.Framework.Network;

namespace Minecraft_Server.Framework.Client
{
    public class Client
    {
        private TcpClientm Net;

        public Client(TcpClientm Net)
        {
            this.Net = Net;
        }

        public virtual void onConnect() { }
        public virtual void Close() { }
    }
}
