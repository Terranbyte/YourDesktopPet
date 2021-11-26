using MoonSharp;
using MoonSharp.Interpreter;
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
        public Script lua;
        public bool ready = false;

        public PetLuaHandler()
        {
            lua = new Script();

            Helpers.Log.WriteLine("LuaHandler", "Creating variables...");
            CreateVariables();
            Helpers.Log.WriteLine("LuaHandler", "Done!");
            Helpers.Log.WriteLine("LuaHandler", "Registering functions...");
            RegisterFunctions();
            Helpers.Log.WriteLine("LuaHandler", "Done!");
            Helpers.Log.WriteLine("LuaHandler", "Populating variables...");
            PopulateVariables();
            Helpers.Log.WriteLine("LuaHandler", "Done!");

            ready = true;
        }

        private void CreateVariables()
        {
            lua.DoFile("Lua/PetVariables.lua");
        }

        private void RegisterFunctions()
        {
            lua.DoFile("Lua/LuaOverrideFunctions.lua");
            lua.DoFile("Lua/PetAPIFunctions.lua");

            lua.Globals["_Print"] = (Action<string, object>)Helpers.Log.WriteLine;
            lua.Globals["_GetDesktopBounds"] = (Func<Rectangle>)GetDesktopBounds;
            lua.Globals["_GetWindows"] = (Func<bool, bool, Table>)GetWindows;
            lua.Globals["_ReadValue"] = (Func<string, object>)API.SaveData.ReadValue;
            lua.Globals["_SaveValue"] = (Action<string, object>)API.SaveData.WriteValue;
            lua.Globals["_GetMousePos"] = (Func<Point>)API.Input.InputProvider.GetMousePos;
            lua.Globals["_MouseButtonDown"] = (Func<int, bool>)API.Input.InputProvider.MouseButtonDown;
            lua.Globals["_MouseButtonUp"] = (Func<int, bool>)API.Input.InputProvider.MouseButtonUp;
            lua.Globals["_IsKeyDown"] = (Func<string, bool>)API.Input.InputProvider.IsKeyDown;
            lua.Globals["_IsKeyHeld"] = (Func<string, bool>)API.Input.InputProvider.IsKeyHeld;
            //lua.Globals["_IsKeyUp"] = (Func<string, bool>)API.Input.InputProvider.IsKeyUp;
        }

        private void PopulateVariables()
        {
            lua.NewTable("pet");
            lua.NewTable("pet.data");
            lua.DoString("desktopBounds = _GetDesktopBounds()");
            lua.DoString("windows = _GetWindows(true, true)");

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

        private Table GetWindows(bool removeOverlap, bool removeMaximize)
        {
            Script tempLua = new Script();
            Table window = new Table(tempLua);
            Table windowsTable = new Table(lua);
            KeyValuePair<string, Rectangle>[] windows = Helpers.DesktopWindows.GetAllWindowBounds(removeOverlap, removeMaximize);

            for (int i = 0; i < windows.Length; i++)
            {
                window.Clear();
                window["name"] = windows[i].Key;
                window["bounds"] = windows[i].Value;
                windowsTable[i] = window;
            }
            
            return windowsTable;
        }
    }
#pragma warning restore IDE0051 // Remove unused private members
}
