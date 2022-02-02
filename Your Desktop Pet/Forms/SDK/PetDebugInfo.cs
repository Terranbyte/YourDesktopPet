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
        public Vector2 position;
        public Vector2 size;
        public AnimatorDebugInfo animatorInfo;

        public PetDebugInfo(Vector2 position, Vector2 size, AnimatorDebugInfo animatorInfo)
        {
            this.position = position;
            this.size = size;
            this.animatorInfo = animatorInfo;
        }
    }
}
