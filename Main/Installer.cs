using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;

namespace SpotlightToDesktop
{
    static class Installer
    {
        public static void InstallContextMenu()
        {
            RegistryKey key = Registry.ClassesRoot.CreateSubKey(@"DesktopBackground\Shell\RotateWallpaper", true);
            key.SetValue(String.Empty, "Switch wallpaper");
            key.SetValue("HideInSafeMode", String.Empty);
            key.SetValue("Position", "Bottom");
            key.SetValue("Icon", $"{Application.ExecutablePath},0");
            // key.SetValue("CommandFlags", (0x20 | 0x40), RegistryValueKind.DWord); // top and bottom separators

            var subkey = key.CreateSubKey("command", true);
            subkey.SetValue(String.Empty, $"{Application.ExecutablePath} -change");
        }

        public static void InstallScheduledTask()
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
                    var action = new ExecAction(Application.ExecutablePath, "-change");
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
