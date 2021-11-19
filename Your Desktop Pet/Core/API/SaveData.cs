using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Core.API
{
    static class SaveData
    {
        public static object ReadValue(string key)
        {
            Ini.IniFile file = new Ini.IniFile(Core.Globals.dataPath + "\\pet.ini");
            try
            {
                return file.IniReadValue("PetCustomData", key);
            }
            catch
            {
                Helpers.Log.WriteLine("PetAPI", $"Error! Tried to read save value \"{key}\" but it didn't exist");
                return null;
            }
        }

        public static void WriteValue(string key, object value)
        {
            Ini.IniFile file = new Ini.IniFile(Core.Globals.dataPath + "\\pet.ini");
            file.IniWriteValue("PetCustomData", key, value.ToString());
        }
    }
}
