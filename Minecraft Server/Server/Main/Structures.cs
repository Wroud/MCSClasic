using Minecraft_Server.Server.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Main
{
    class Structures
    {

        public struct Block
        {
            public Vector3 pos;
            public byte type;
            public sbyte ste;
            public Block(Vector3 p, byte t, sbyte s)
            {
                pos = p;
                type = t;
                ste = s;
            }
        }

        public struct Message
        {
            public string message;
            public string level;
            public sbyte ovner;
            public string ovname;

            public Message(string mes, string level, sbyte ovn,string ovna)
            {
                this.message = mes;
                this.level = level;
                this.ovner = ovn;
                this.ovname = ovna;
            }
        }
    }
}
