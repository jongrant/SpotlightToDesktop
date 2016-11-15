using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpotlightToDesktop.Silent
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // get the list of possible wallpapers in the spotlight folder
            List<ImageInfo> candidates = Inspector.GetWallpaperCandidates();
            
            // now pick one at random
            var picker = new Random();
            var index = picker.Next(candidates.Count);
            var selected = candidates[index];

            // update the wallpaper
            Wallpaper.Set(selected.FileName, Wallpaper.Style.Stretched);
        }
    }
}
