using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;

namespace Your_Desktop_Pet
{
    class Program
    {
        public static string baseDirectory = string.Empty;

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        
        [STAThread]
        static void Main(string[] args)
        {
            #region Setup
            IntPtr hWnd = GetConsoleWindow();
            ShowWindow(hWnd, SW_HIDE);
            Application.EnableVisualStyles();

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "ini files (*.ini)|*.ini";
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    baseDirectory = Directory.GetParent(dialog.FileName).FullName;
                }
            }

            if (string.IsNullOrEmpty(baseDirectory))
                return;

            FetchAndApplySettings(baseDirectory);

            Core.Helpers.Log.Create();
            Core.Helpers.Log.WriteLine("Main", "Debug start");

            if (Core.Globals.debugMode)
            {
                if (hWnd != IntPtr.Zero)
                    ShowWindow(hWnd, SW_SHOW);
            }
            #endregion

            Core.Pet.PetObject pet = new Core.Pet.PetObject(baseDirectory);
            pet.Start();

            Core.Helpers.Time.Start();

            float currentTime = 0;
            float animationTime = 0;
            float updateInterval = 1.0f / Core.Globals.frameCap;
            float animationInterval = Core.Globals.frameCap / Core.Globals.animationFrameRate;
            float totalTime = 0;

            while (!pet.shouldExit)
            {
                Core.Helpers.Time.Update();
                currentTime += Core.Helpers.Time.deltaTime;
                totalTime += Core.Helpers.Time.deltaTime;

                if (currentTime < updateInterval)
                    continue;

                Console.Title = $"FPS: {1/currentTime}";

                animationTime += 1;

                Core.API.Input.InputProvider.Update();
                pet.Update();

                if (animationTime >= animationInterval)
                {
                    pet.Draw();
                    animationTime -= animationInterval;
                }

                currentTime -= updateInterval;
            }

            Core.Helpers.Log.WriteLine("Main", "Debug end");
            Core.Helpers.Log.Destroy();
        }

        private static bool CheckFileStructure(string directory)
        {
            string[] filesToCheck = new string[]
            {
                "\\pet.ini",
                "\\Scripts\\pet.lua",
                "\\Sprites",
                "\\Assets"
            };

            foreach (string s in filesToCheck)
            {
                if (!File.Exists(directory + s) && !Directory.Exists(directory + s))
                    return false;
            }

            return true;
        }

        private static void FetchAndApplySettings(string petDirectory)
        {
            Ini.IniFile settingsFile = new Ini.IniFile(".\\settings.ini");
            Ini.IniFile petFile = new Ini.IniFile(petDirectory + "\\pet.ini");

            // App settings
            Core.Globals.debugMode = Convert.ToBoolean(settingsFile.IniReadValue("AppSettings", "DebugMode"));
            Core.Globals.luaTraceback = Convert.ToBoolean(settingsFile.IniReadValue("AppSettings", "LuaTraceback"));

            // Pet settings
            Core.Globals.frameCap = Convert.ToSingle(petFile.IniReadValue("PetSettings", "UpdateCallInterval"));
            Core.Globals.animationFrameRate = Convert.ToSingle(petFile.IniReadValue("PetSettings", "AnimationFrameRate"));
            Core.Globals.scaleFactor = Convert.ToSingle(petFile.IniReadValue("PetSettings", "SpriteScaleFactor"));
            Core.Globals.offset = (Core.Drawing.PositionOffset)Enum.Parse(typeof(Core.Drawing.PositionOffset), petFile.IniReadValue("PetSettings", "Offset"));
        }
    }
}