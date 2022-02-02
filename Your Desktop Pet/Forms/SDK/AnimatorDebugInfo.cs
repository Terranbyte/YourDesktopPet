using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Forms.SDK
{
    public struct AnimatorDebugInfo
    {
        public string currentAnimation;
        public int numFrames;
        public int currentFrame;

        public AnimatorDebugInfo(string currentAnimation, int numFrames, int currentFrame)
        {
            this.currentAnimation = currentAnimation;
            this.numFrames = numFrames;
            this.currentFrame = currentFrame;
        }
    }
}
