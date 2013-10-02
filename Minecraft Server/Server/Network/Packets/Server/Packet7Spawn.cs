using Minecraft_Server.Server.Utils;

namespace Minecraft_Server.Server.Network.Packets
{
    class Packet7Spawn : Framework.Network.Packet
    {
        private byte opcode = 7;
        private sbyte id;
        private string name;
        private Vector3 pos;
        private Vector2 rot;

        public Packet7Spawn(TcpClientm d, sbyte id, string name, Vector3 pos, Vector2 rot)
        {
            this.data = d;
            this.id = id;
            this.name = name;
            this.pos = pos;
            this.rot = rot;
        }

        public static void Read(TcpClientm d)
        {
        }
        public override void Write()
        {
            //Framework.Util.Utils.TimeOut(ref this.data.Write, 300);
            this.data.Write = true;
            this.data.Write(opcode);
            this.data.Write(id);
            this.data.Write(name);
            this.data.Write(pos);
            this.data.Write(rot);
            this.data.Flush();
        }
    }
}
