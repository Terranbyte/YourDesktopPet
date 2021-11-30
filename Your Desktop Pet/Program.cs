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
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            Core.Helpers.Log.Create();
            Core.Helpers.Log.WriteLine("Main", "Debug start");

            if (!Directory.Exists(Core.Globals.dataPath))
                Directory.CreateDirectory(Core.Globals.dataPath);

            Application.Run(Forms.FormManager.Current.CreateForm<Forms.Launcher>());
        }
    }
}