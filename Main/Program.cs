using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Win32;

namespace SpotlightToDesktop.Main
{
    static class Program
    {
        enum Mode {
            InstallScheduledTask,
            InstallContextMenu,
            Change,
            Unknown
        }

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mode = ParseArgs(args);

            if (mode == Mode.Unknown)
            {
                Alert.Error("Unrecognised command line argument.");
                return;
            }

            if (mode == Mode.InstallScheduledTask)
            {
                try
                {
                    Installer.InstallScheduledTask();

                    Alert.Information("Scheduled task has been installed successfully.");
                }
                catch (Exception e)
                {
                    Diagnostics.Log(e);
                    Alert.Error($"There was a problem installing the scheduled task: {e.Message}");
                }

                return;
            }

            if (mode == Mode.InstallContextMenu)
            {
                try
                {
                    Installer.InstallContextMenu();

                    Alert.Information("Desktop context menu item has been installed successfully.");
                }
                catch (Exception e)
                {
                    Diagnostics.Log(e);
                    Alert.Error($"There was a problem installing the context menu item: {e.Message}");
                }

                return;
            }

            if (mode == Mode.Change)
            {
                try
                {
                    // get the list of possible wallpapers in the spotlight folder
                    var candidates = Inspector.GetWallpaperCandidates();

                    // get the last one we picked 
                    var last = Settings.LastSelected;
                    if (last != null)
                    {
                        // remove it from the list - bad luck protection so it doesn't seem like we did nothing
                        candidates.RemoveAll(w => w.Key.Equals(last, StringComparison.InvariantCultureIgnoreCase));
                    }

                    // if we have nothing to choose from, display a message
                    if (candidates.Count == 0)
                    {
                        Alert.Warning($"There are no potential wallpaper candidates.");
                        return;
                    }

                    // now pick one at random
                    var picker = new Random();
                    var index = picker.Next(candidates.Count);
                    var selected = candidates[index];

                    // update the wallpaper
                    Wallpaper.Set(selected.FileName, Wallpaper.Style.Stretched);

                    // remember which one we picked for bad luck protection
                    Settings.LastSelected = selected.Key;
                }
                catch (Exception e)
                {
                    Diagnostics.Log(e);
                    Alert.Error($"There was a problem changing the wallpaper: {e.Message}");
                }
            }
        }

        private static Mode ParseArgs(string[] args)
        {
            if (args.Length > 0)
            {
                var arg = args[0].ToLowerInvariant();

                if ("-install".StartsWith(arg)) return Mode.Unknown;
                else if ("-installscheduledtask".StartsWith(arg)) return Mode.InstallScheduledTask;
                else if ("-installcontextmenu".StartsWith(arg)) return Mode.InstallContextMenu;
                else if ("-change".StartsWith(arg)) return Mode.Change;
                else return Mode.Unknown;
            }
            else
            {
                return Mode.Change;
            }
        }
    }
}
