using MoonSharp;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Your_Desktop_Pet.Core.Lua
{
#pragma warning disable IDE0051 // Remove unused private members
    class PetLuaHandler
    {
        public Script lua;
        internal bool ready;

        public PetLuaHandler()
        {
            lua = new Script(CoreModules.Preset_SoftSandbox ^ CoreModules.Dynamic);

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
            lua.DoFile("LuaScripts/PetVariables.lua");

            UserData.RegisterType<API.Input.VirtualKey>();
            lua.Globals["virtualKey"] = UserData.CreateStatic<API.Input.VirtualKey>();
        }

        private void RegisterFunctions()
        {
            //lua.DoFile("LuaScripts/PetAPIFunctions.lua");

            lua.Globals["_Print"] = (Action<string, object>)Helpers.Log.WriteLine;

            lua.Globals["_GetDesktopBounds"] = (Func<Table>)GetDesktopBounds;
            lua.Globals["_GetWindows"] = (Func<bool, bool, Table>)GetWindows;
            lua.Globals["_TableLength"] = (Func<Table, int>)GetTableLength;
            lua.Globals["_AABBFromXYWH"] = (Func<int, int, int, int, Table>)AABBFromXYWH;
            lua.Globals["_IsCollidingAABB"] = (Func<Table, Table, bool>)LuaHelper.AABBColliding;

            lua.Globals["_ReadValue"] = (Func<string, object>)API.SaveData.ReadValue;
            lua.Globals["_SaveValue"] = (Action<string, object>)API.SaveData.WriteValue;

            lua.Globals["_GetMousePos"] = (Func<Point>)API.Input.InputProvider.GetMousePos;
            lua.Globals["_MouseButtonDown"] = (Func<int, bool>)API.Input.InputProvider.MouseButtonDown;
            lua.Globals["_MouseButtonUp"] = (Func<int, bool>)API.Input.InputProvider.MouseButtonUp;
            lua.Globals["_IsKeyDown"] = (Func<API.Input.VirtualKey, bool>)API.Input.InputProvider.IsKeyDown;
            lua.Globals["_IsKeyHeld"] = (Func<API.Input.VirtualKey, bool>)API.Input.InputProvider.IsKeyHeld;
            //lua.Globals["_IsKeyUp"] = (Func<string, bool>)API.Input.InputProvider.IsKeyUp;

            lua.DoFile("LuaScripts/LuaOverrideFunctions.lua");
        }

        private Table AABBFromXYWH(int x, int y, int w, int h)
        {
            return LuaHelper.AABBFromXYWH(x, y, w, h, lua);
        }

        private void PopulateVariables()
        {
            lua.DoString("desktopBounds = _GetDesktopBounds()");
            lua.DoString("windows = _GetWindows(true, true)");

            Table petTable = new Table(lua);

            petTable["x"] = 10;
            petTable["y"] = 0;
            petTable["AABB"] = LuaHelper.RectToTable(Rectangle.FromLTRB(0, 0, 0, 0), lua);
            petTable["animation"] = "";
            petTable["show"] = true;
            petTable["flipX"] = false;

            lua.Globals.Set(DynValue.NewString("pet"), DynValue.NewTable(petTable));
        }

        private Table GetDesktopBounds()
        {
            return LuaHelper.RectToTable(SystemInformation.VirtualScreen, lua);
        }

        private Table GetWindows(bool removeOverlap, bool removeMaximize)
        {
            Table window = new Table(lua);
            Table windowsTable = new Table(lua);
            KeyValuePair<string, Rectangle>[] windows = Helpers.DesktopWindows.GetAllWindowBounds(removeOverlap, removeMaximize);

            for (int i = 0; i < windows.Length; i++)
            {
                window = new Table(lua);
                window["name"] = windows[i].Key;
                window["AABB"] = LuaHelper.RectToTable(windows[i].Value, lua);
                windowsTable.Set(DynValue.NewNumber(i), DynValue.NewTable(window));
            }
            
            return windowsTable;
        }

        private int GetTableLength(Table table)
        {
            return table.Length;
        }
    }
#pragma warning restore IDE0051 // Remove unused private members
}
