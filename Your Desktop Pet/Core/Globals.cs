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
        public static readonly string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\YourDesktopPet\";
    }
}
