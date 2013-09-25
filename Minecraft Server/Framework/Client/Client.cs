using Minecraft_Server.Framework.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
