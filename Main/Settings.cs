using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Win32;

namespace SpotlightToDesktop
{
    static class Settings
    {
        public static RegistryKey UserAppDataRegistry
        {
            get
            {
                return Registry.CurrentUser.CreateSubKey($"SOFTWARE\\{Application.CompanyName}\\{Application.ProductName}", true);
            }
        }

        public static string LastSelected
        {
            get
            {
                return (string)Settings.UserAppDataRegistry.GetValue("Last", null);
            }
            set
            {
                Settings.UserAppDataRegistry.SetValue("Last", value, RegistryValueKind.String);
            }
        }
    }
}
