using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Minecraft_Server;
using Minecraft_Server.Framework.Util;
using System.Threading;

namespace Minecraft_Server.Framework.Network
{
    public class TcpClientm
    {
        private TcpClient tcp;
        public NetworkStream NetStream;
        public bool Proccess = false;
        public bool Write = false;
        public BinaryWriter write;
        public MemoryStream buffer;
        private byte[] opcode;
        public ushort id;
        public string ip;
        public int port;

        public static TcpClientm Get(TcpClient t, ushort i)
        {
            return new TcpClientm(t, i);
        }

        public TcpClientm(TcpClient t, ushort i)
        {
            this.tcp = t;

            IPEndPoint address = this.tcp.Client.RemoteEndPoint as IPEndPoint;

            this.ip = address.Address.ToString();
            this.port = address.Port;

            this.id = i;
            this.NetStream = this.tcp.GetStream();
            this.buffer = new MemoryStream();
            this.write = new BinaryWriter(this.buffer);

            this.opcode = new byte[1];
            this.NetStream.BeginRead(this.opcode, 0, 1, this.AcceptPacket, null);
        }
        public virtual void Close()
        {
            if (this.tcp != null)
                this.tcp.Close();
            this.tcp = null;
            this.buffer.Dispose();
            this.write = null;
            this.NetStream = null;
            this.opcode = null;
            this.ip = null;
        }
        private void AcceptPacket(IAsyncResult result)
        {
            Utils.TimeOut(ref Proccess,2000);
            try
            {
                if (!this.tcp.Connected)
                {
                    Network.CloseTcpClient(this.id);
                    return;
                }
                int bytesRead = this.NetStream.EndRead(result);

                    if (bytesRead == 1)
                    {
                        Log.Info("Get packet: " + this.opcode[0]);
                        APacket(this.opcode[0]);
                    }
                    else
                        Network.CloseTcpClient(this.id);
                    this.NetStream.BeginRead(this.opcode, 0, 1, this.AcceptPacket, null);
            }
            catch
            {
                Network.CloseTcpClient(id);
            }
        }
        public virtual void APacket(byte o){}
    }
}
