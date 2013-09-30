using System.Net.Sockets;

namespace Minecraft_Server.Server.Network
{
    class TcpClientm : Framework.Network.TcpClientm
    {
        public Client.Client cli;

        new public static TcpClientm Get(TcpClient t, ushort i)
        {
            return new TcpClientm(t, i);
        }

        public TcpClientm(TcpClient t, ushort i)
            : base(t, i)
        {
            this.cli = new Client.Client(this);
            this.cli.onConnect();
        }
        public override void APacket(byte o)
        {
            Network.GetPacket(o).Invoke(this);
        }
        public override void Close()
        {
            base.Close();
            if (this.cli != null)
                this.cli.Close();
            this.cli = null;
        }
    }
}
