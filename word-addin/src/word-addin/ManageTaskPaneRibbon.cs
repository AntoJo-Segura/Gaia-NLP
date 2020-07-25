using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace GaiaWordAddIn
{
    public partial class ManageTaskPaneRibbon
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {
            toggleButton1.Label = Properties.Resources.ShowButtonTextEN;
        }

        private void toggleButton1_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.AddinController.GaiaTaskPane.Visible = ((RibbonToggleButton)sender).Checked;
        }
    }
}
