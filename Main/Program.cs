using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;

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
                MessageBox.Show("Unrecognised command line argument.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (mode == Mode.InstallScheduledTask)
            {
                try
                {
                    InstallScheduledTask();

                    MessageBox.Show("Scheduled task has been installed successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch (Exception e)
                {
                    MessageBox.Show($"There was a problem installing the scheduled task: {e.Message}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return;
            }

            if (mode == Mode.InstallContextMenu)
            {
                try
                {
                    InstallContextMenu();

                    MessageBox.Show("Desktop context menu item has been installed successfully.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"There was a problem installing the context menu item: {e.Message}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return;
            }

            if (mode == Mode.Change)
            {
                try
                {
                    // get the list of possible wallpapers in the spotlight folder
                    var candidates = Inspector.GetWallpaperCandidates();

                    if (candidates.Count == 0)
                    {
                        MessageBox.Show($"There are no potential wallpaper candidates.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }

                    // now pick one at random
                    var picker = new Random();
                    var index = picker.Next(candidates.Count);
                    var selected = candidates[index];

                    // update the wallpaper
                    Wallpaper.Set(selected.FileName, Wallpaper.Style.Stretched);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"There was a problem changing the wallpaper: {e.Message}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private static void InstallContextMenu()
        {
            RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"DesktopBackground\Shell\RotateWallpaper", true);
            key.SetValue(String.Empty, "Switch Wallpaper");
            key.SetValue("HideInSafeMode", String.Empty);
            key.SetValue("Position", "Bottom");
            key.SetValue("Icon", $"{Application.ExecutablePath},0");

            var subkey = key.CreateSubKey("command", true);
            subkey.SetValue(String.Empty, $"{Application.ExecutablePath} -change");
        }

        private static void InstallScheduledTask()
        {
            try
            {
                // Get the service on the local machine
                using (TaskService ts = new TaskService())
                {
                    // Create a new task definition and assign properties
                    TaskDefinition td = ts.NewTask();
                    td.RegistrationInfo.Description = "Changes the desktop wallpaper to one of the Windows Spotlight cached images.";
                    td.Settings.AllowDemandStart = true;
                    // td.Settings.RunOnlyIfLoggedOn = true;
                    td.Settings.StartWhenAvailable = true;
                    td.Settings.WakeToRun = false;
                    td.Settings.DisallowStartIfOnBatteries = false;
                    td.Settings.StopIfGoingOnBatteries = false;

                    // Create a trigger that will fire the task at this time other day
                    var trigger = new DailyTrigger();
                    trigger.StartBoundary = DateTime.Today;
                    td.Triggers.Add(trigger);

                    // Create an action that will launch our executable whenever the trigger fires
                    var action = new ExecAction(Assembly.GetExecutingAssembly().Location, "-change");
                    td.Actions.Add(action);
                    
                    // Register the task in the root folder
                    ts.RootFolder.RegisterTaskDefinition(@"Spotlight To Desktop", td, TaskCreation.CreateOrUpdate, Environment.UserName, null, TaskLogonType.InteractiveToken);
                }

                Console.WriteLine("Scheduled task installed.");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Failed to install scheduled task: {e.Message}");
            }
        }
    }
}
