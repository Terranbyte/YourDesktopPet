using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Forms.SDK
{
    public struct PetDebugInfo
    {
        public LuaObjectDebugInfo objectInfo;
        public AnimatorDebugInfo animatorInfo;

        public PetDebugInfo(LuaObjectDebugInfo objectInfo, AnimatorDebugInfo animatorInfo)
        {
            this.objectInfo = objectInfo;
            this.animatorInfo = animatorInfo;
        }
    }
}
