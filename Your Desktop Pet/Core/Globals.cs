using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Core
{
    static class Globals
    {
        public static bool debugMode = true;
        public static bool luaTraceback = false;

        public static float frameCap = 60f;
        public static float animationFrameRate = 12f;
        public static float scaleFactor = 1f;
        public static Drawing.PositionOffset offset = Drawing.PositionOffset.TopLeft;
    }
}
