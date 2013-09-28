using Minecraft_Server.Framework.Util;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Minecraft_Server.Framework.Network
{
    class Network : Object
    {
        #region Static
        public static Network net;
        public delegate TcpClientm GetClient(TcpClient t, ushort i);
        public static GetClient NClient;
        #region Dublicates
        public static bool Init { get { return net.Inite; } }
        public static Read GetPacket(byte i)
        {
                return net.packets[i];
        }
        public static void AddPacket(byte i, Read p)
        {
            net.packets.Add(i,p);
        }
        public static ConcurrentQueue<Packet> MessageQueue { get { return net.messageQueue; } set { net.messageQueue = value; } }
        public static void CloseTcpClient(ushort id)
        {
            net.closeTcpClient(id);
        }
        #endregion

        public static void Initz(Network network=null,GetClient client=null)
        {
            if (network == null)
                net = new Network();
            else
                net = network;
            if (client == null)
                NClient = TcpClientm.Get;
            else
                NClient = client;
            net.Initialize();
        }
        public static void Run()
        {
            net.Start();
        }
        #endregion
        private TcpListener Socket;
        public Dictionary<ushort, TcpClientm> connects;
        public delegate void Read(Server.Network.TcpClientm data);
        public ConcurrentQueue<Packet> messageQueue;
        public Dictionary<byte, Read> packets;
        private Thread NetThread;

        public void Initialize()
        {
            this.connects = new Dictionary<ushort, TcpClientm>();
            this.packets = new Dictionary<byte, Read>();
            this.messageQueue = new ConcurrentQueue<Packet>();
            this.SetPackets();
            this.Socket = new TcpListener(IPAddress.Any, 25565);
            this.NetThread = new Thread(NetT);
            this.Inite = true;
        }

        public virtual void SetPackets() { }

        public void Start()
        {
            this.Socket.Start();
            this.Socket.BeginAcceptTcpClient(this.AcceptTcpClient, null);
            this.NetThread.Start();
        }

        private void NetT()
        {
            while (true)
            {
                for (int i = 0; i < this.messageQueue.Count; i++)
                {
                    Packet message = null;
                    if (!this.messageQueue.TryDequeue(out message)) continue;
                    message.data.Proccess = true;
                    //message.Process();
                    message.data.Proccess = false;
                }
                Thread.Sleep(1);
            }
        }

        private void AcceptTcpClient(IAsyncResult result)
        {
            TcpClient tcpClient = this.Socket.EndAcceptTcpClient(result);

            ushort id = this.ID;
            this.connects.Add(id, NClient.Invoke(tcpClient, id));
            //tcpClient.Close();

            this.Socket.BeginAcceptTcpClient(this.AcceptTcpClient, null);
        }

        public void closeTcpClient(ushort id)
        {
            if (this.connects.ContainsKey(id))
            {
                Utils.TimeOut(ref net.connects[id].Write, 2000);
                ((Server.Network.TcpClientm)this.connects[id]).Close();
                this.connects.Remove(id);
            }
        }

        private ushort ID
        {
            get
            {
                ushort[] ids = this.connects.Keys.ToArray<ushort>();
                for (ushort i = 0; i < 65535; i++)
                    if (!ids.Contains(i))
                        return i;
                return 0;
            }
        }
    }
}
