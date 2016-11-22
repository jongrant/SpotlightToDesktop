using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpotlightToDesktop
{
    static class Diagnostics
    {
        public static void Log(Exception e)
        {
            string path = Path.Combine(Path.GetTempPath(), $"{Application.ProductName} Error.txt");

            var result = new StringBuilder();

            var c = e;
            do
            {
                result.AppendLine("Message:");
                result.AppendLine(c.Message);
                result.AppendLine("Stack Trace:");
                result.AppendLine(c.StackTrace);

                c = e.InnerException;
            }
            while (c != null);

            File.WriteAllText(path, result.ToString());
        }
    }
}
