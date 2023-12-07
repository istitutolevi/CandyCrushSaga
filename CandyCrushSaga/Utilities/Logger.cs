using System;
using System.IO;
using System.Windows.Forms;

namespace CandyCrushSaga.Utilities
{
    internal static class Logger
    {
        internal static void LogDebugMessage(string msg)
        {
            LogToFile(msg);
        }

        internal static void Alert(string msg)
        {
            MessageBox.Show(msg);
        }

        internal static void LogToFile(string msg)
        {
            if (!File.Exists(Path.Combine(Application.StartupPath, "log.txt")))
            {
                try
                {
                    var d = File.Create(Path.Combine(Application.StartupPath, "log.txt"));
                    d.Dispose();
                }
                catch 
                { }
            }
            try
            {
                using (var writer = File.AppendText(Path.Combine(Application.StartupPath, "log.txt")))
                {
                    writer.WriteLine(DateTime.Now.ToString() + "  Message:" + msg);
                }
            }
            catch 
            { }
        }
    }
}
