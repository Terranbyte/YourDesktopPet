using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Forms.SDK
{
    public struct LuaObjectDebugInfo
    {
        public string name;
        public Vector2 position;
        public Vector2 size;

        public LuaObjectDebugInfo(string name, Vector2 position, Vector2 size)
        {
            this.name = name;
            this.position = position;
            this.size = size;
        }
    }
}
