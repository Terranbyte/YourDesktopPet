using NLua;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Your_Desktop_Pet.Core.Pet
{
#pragma warning disable IDE0051 // Remove unused private members
    class PetLuaHandler
    {
        public Lua lua;
        public bool Ready = false;

        public PetLuaHandler()
        {
            lua = new Lua();

            lua.MaximumRecursion = 6;

            Helpers.Log.WriteLine("LuaHandler", "Creating variables...");
            CreateVariables();
            Helpers.Log.WriteLine("LuaHandler", "Done!");
            Helpers.Log.WriteLine("LuaHandler", "Registering functions...");
            RegisterFunctions();
            Helpers.Log.WriteLine("LuaHandler", "Done!");
            Helpers.Log.WriteLine("LuaHandler", "Populating variables...");
            PopulateVariables();
            Helpers.Log.WriteLine("LuaHandler", "Done!");

            Ready = true;
        }

        private void CreateVariables()
        {
            lua.DoFile("Lua/PetVariables.lua");
        }

        private void RegisterFunctions()
        {
            lua["os"] = null;
            lua["io"] = null;
            lua["dofile"] = null;
            lua["package"] = null;
            lua["luanet"] = null;
            lua["load"] = null;

            lua.DoFile("Lua/LuaOverrideFunctions.lua");
            lua.DoFile("Lua/PetAPIFunctions.lua");

            lua.RegisterFunction("_Print", typeof(Helpers.Log).GetMethod("WriteLine", BindingFlags.Static | BindingFlags.Public));
            lua.RegisterFunction("_GetDesktopBounds", this, typeof(PetLuaHandler).GetMethod("GetDesktopBounds", BindingFlags.Instance | BindingFlags.NonPublic));
            lua.RegisterFunction("_GetWindows", this, typeof(PetLuaHandler).GetMethod("GetWindows", BindingFlags.Instance | BindingFlags.NonPublic));
            lua.RegisterFunction("_ReadValue", typeof(API.SaveData).GetMethod("ReadValue", BindingFlags.Static | BindingFlags.Public));
            lua.RegisterFunction("_SaveValue", typeof(API.SaveData).GetMethod("WriteValue", BindingFlags.Static | BindingFlags.Public));
            lua.RegisterFunction("_GetMousePos", typeof(API.Input.InputProvider).GetMethod("GetMousePos", BindingFlags.Static | BindingFlags.Public));
            lua.RegisterFunction("_MouseButtonDown", typeof(API.Input.InputProvider).GetMethod("MouseButtonDown", BindingFlags.Static | BindingFlags.Public));
            lua.RegisterFunction("_MouseButtonUp", typeof(API.Input.InputProvider).GetMethod("MouseButtonUp", BindingFlags.Static | BindingFlags.Public));
            lua.RegisterFunction("_IsKeyDown", typeof(API.Input.InputProvider).GetMethod("IsKeyDown", BindingFlags.Static | BindingFlags.Public));
            lua.RegisterFunction("_IsKeyHeld", typeof(API.Input.InputProvider).GetMethod("IsKeyHeld", BindingFlags.Static | BindingFlags.Public));
            lua.RegisterFunction("_IsKeyUp", typeof(API.Input.InputProvider).GetMethod("IsKeyUp", BindingFlags.Static | BindingFlags.Public));
        }

        private void PopulateVariables()
        {
            lua.NewTable("pet");
            lua.NewTable("pet.data");
            lua.DoString("desktopBounds = _GetDesktopBounds()");
            lua.DoString("windows = _GetWindows(true, true)");

            LuaTable petObject = lua.GetTable("pet");

            petObject["x"] = 0;
            petObject["y"] = 0;
            petObject["bounds"] = Rectangle.FromLTRB(0, 0, 0, 0);
            petObject["animation"] = "";
            petObject["show"] = true;
            petObject["flipX"] = false;

            lua.SetObjectToPath("pet", petObject);
        }

        private Rectangle GetDesktopBounds()
        {
            return SystemInformation.VirtualScreen;
        }

        private LuaTable GetWindows(bool removeOverlap, bool removeMaximize)
        {
            lua.NewTable("temp");
            lua.NewTable("windows");

            lua.DoString(@"temp = {}
windows = {}");

            LuaTable window = null;
            LuaTable windowsTable = lua.GetTable("windows");
            KeyValuePair<string, Rectangle>[] windows = Helpers.DesktopWindows.GetAllWindowBounds(removeOverlap, removeMaximize);

            for (int i = 0; i < windows.Length; i++)
            {
                lua.NewTable("temp");
                window = lua.GetTable("temp");
                window["name"] = windows[i].Key;
                window["bounds"] = windows[i].Value;
                windowsTable[i] = window;
            }
            
            return windowsTable;
        }
    }
#pragma warning restore IDE0051 // Remove unused private members
}
