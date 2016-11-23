using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpotlightToDesktop
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void OnInstallContextMenu(object sender, EventArgs e)
        {
            Program.PerformInstallContextMenu();
        }

        private void OnInstallScheduledTask(object sender, EventArgs e)
        {
            Program.PerformInstallScheduledTask();
        }
    }
}
