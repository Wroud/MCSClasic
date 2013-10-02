
namespace Minecraft_Server.Server.Network.Packets
{
    class Packet13Message : Framework.Network.Packet
    {
        private byte opcode = 13;
        private sbyte id;
        private string message;

        public Packet13Message(TcpClientm d, sbyte id, string message)
        {
            this.data = d;
            this.id = id;
            this.message = message;
        }
        public static void Read(TcpClientm d)
        {
            byte c = d.NetStream.ReadByte(d);
            string mes = d.NetStream.ReadString(d);
            d.cli.onMessage(c, mes);
        }
        public override void Write()
        {
            if (message.Length <= 64)
            {
                this.data.Write = true;
                this.data.Write(opcode);
                this.data.Write(id);
                this.data.Write(message);
                this.data.Flush();
            }
            else
            {
                for (int i = 0; i < message.Length; )
                {
                    string mes;
                    if (i < message.Length - 64)
                        mes = message.Substring(i, 64);
                    else
                        mes = message.Substring(i, message.Length - i);
                    this.data.Write = true;
                    this.data.Write(opcode);
                    this.data.Write(id);
                    this.data.Write(mes);
                    this.data.Flush();
                    i += 64;
                }
            }
        }
    }
}
