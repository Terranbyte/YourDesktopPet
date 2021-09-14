using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Your_Desktop_Pet.Core.Helpers
{
    static class Log
    {
        public static void Create()
        {
            if (Globals.DebugMode)
                return;

            TextWriterTraceListener listener = new TextWriterTraceListener(new StreamWriter("./debug.log", false));
            Trace.AutoFlush = true;
            Trace.Listeners.Add(listener);
        }

        public static void Destroy()
        {
            Trace.Listeners.Clear();
        }

        public static void WriteLine(string debugSignature, object msg)
        {
            if (msg == null)
                msg = "null";

            if (Globals.DebugMode)
                Console.WriteLine($"[{debugSignature}({DateTime.Now.ToString()})] {msg.ToString()}");
            else
            {
                Trace.WriteLine($"[{debugSignature}({DateTime.Now.ToString()})] {msg.ToString()}");
                //Trace.Flush();
            }
        }
    }
}
