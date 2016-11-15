using Microsoft.Win32.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpotlightToDesktop.Main
{
    static class Program
    {
        enum Mode {
            Install,
            List,
            Change,
            Help,
            Unknown
        }

        static void Main(string[] args)
        {
            var mode = ParseArgs(args);

            if (mode == Mode.Unknown)
            {
                Console.Error.WriteLine("Unrecognised command line argument.");
            }

            if (mode == Mode.Help || mode == Mode.Unknown)
            {
                var exeName = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
                Console.WriteLine($"Usage: {exeName} ( -install | -list | -change )");
                return;
            }

            if (mode == Mode.Install)
            {
                InstallScheduledTask();

                return;
            }

            // get the list of possible wallpapers in the spotlight folder
            List<ImageInfo> candidates = Inspector.GetWallpaperCandidates();

            if (mode == Mode.List)
            {
                foreach (var image in candidates)
                {
                    Console.WriteLine($"{Path.GetFileName(image.FileName)}: Type = {image.Format}, Size = {image.Dimensions.Width} x {image.Dimensions.Height}");
                }
            }
            else if (mode == Mode.Change)
            {
                // now pick one at random
                var picker = new Random();
                var index = picker.Next(candidates.Count);
                var selected = candidates[index];

                // update the wallpaper
                Wallpaper.Set(selected.FileName, Wallpaper.Style.Stretched);
            }
        }

        private static Mode ParseArgs(string[] args)
        {
            if (args.Length > 0)
            {
                var arg = args[0].ToLowerInvariant();

                if ("-install".StartsWith(arg)) return Mode.Install;
                else if ("-list".StartsWith(arg)) return Mode.List;
                else if ("-change".StartsWith(arg)) return Mode.Change;
                else if ("-help".StartsWith(arg)) return Mode.Help;
                else if ("-?".StartsWith(arg)) return Mode.Help;
                else return Mode.Unknown;
            }
            else
            {
                return Mode.Change;
            }
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

                    // Create an action that will launch Notepad whenever the trigger fires
                    if (File.Exists("SpotlightToDesktopSilent.exe"))
                    {
                        var action = new ExecAction(Path.GetFullPath("SpotlightToDesktopSilent.exe"));
                        td.Actions.Add(action);
                    }
                    else
                    {
                        var action = new ExecAction(Assembly.GetExecutingAssembly().Location, "-change");
                        td.Actions.Add(action);
                    }

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
