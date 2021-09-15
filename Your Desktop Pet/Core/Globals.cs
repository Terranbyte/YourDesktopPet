using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Core
{
    static class Globals
    {
        public static bool DebugMode = true;
        public static bool LuaTraceback = false;

        public static float FrameCap = 60f;
        public static float AnimationFrameRate = 12f;
        public static float ScaleFactor = 1f;
        public static Drawing.PositionOffset Offset = Drawing.PositionOffset.TopLeft;
    }
}
