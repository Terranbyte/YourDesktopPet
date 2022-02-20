using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Forms.SDK
{
    public struct SpriteDebugInfo
    {
        public string spriteName;
        public bool flipX;
        public bool shown;

        public SpriteDebugInfo(string spriteName, bool flipX, bool shown)
        {
            this.spriteName = spriteName;
            this.flipX = flipX;
            this.shown = shown;
        }
    }
}
