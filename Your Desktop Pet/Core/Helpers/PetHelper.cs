using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Core.Helpers
{
    public static class PetHelper
    {
        public static bool CheckPetFileStructure(string petPath, out string error)
        {
            bool success = true;
            string[] filesToCheck = new string[]
            {
                "\\pet.ini",
                "\\icon.png",
                "\\Scripts\\pet.lua",
            };

            error = null;

            foreach (string s in filesToCheck)
            {
                if (!File.Exists(petPath + s) && !Directory.Exists(petPath + s))
                {
                    if (string.IsNullOrEmpty(error))
                        error = $"Missing file(s) \"{s.Substring(1)}\"";
                    else
                        error += $", \"{s.Substring(1)}\"";
                    success = false;
                }
            }

            return success;
        }

        public static void FixProjectFileStructure(string projectPath)
        {
            string[] filesToCheck = new string[]
            {
                "\\Sprites",
                "\\Scripts",
                "\\Assets",
            };

            foreach (string s in filesToCheck)
            {
                if (!Directory.Exists(projectPath + s))
                {
                    Directory.CreateDirectory(projectPath + s);
                }
            }
        }
    }
}
