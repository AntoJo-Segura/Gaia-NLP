using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Word;
using System.Windows.Forms;
using Microsoft.Office.Tools;

namespace GaiaWordAddIn
{
    public partial class AddinController
    {
        private UserControl _myUserControl1;
        private CustomTaskPane _myCustomTaskPane;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            _myUserControl1 = new TaskPaneControl();
            _myCustomTaskPane = this.CustomTaskPanes.Add(_myUserControl1, "Gaia Addin");
            _myCustomTaskPane.Visible = true;
            _myCustomTaskPane.Width = 350;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        internal void includeTextAndSelect()
        {
            Word.Range rng = Application.ActiveDocument.Range(0, 0);
            rng.Text = "Esto es un texto muy largo y se va a seleccionar completamente";
            Application.ActiveDocument.Select();
        }

        #region Código generado por VSTO

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
