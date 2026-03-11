using DevExpress.XtraEditors;
using DevExpress.XtraWaitForm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList
{
    internal static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //WindowsFormsSettings.SetPerMonitorDpiAware();

            //DevExpress.Utils.AppearanceObject.DefaultFont = new Font("Segoe UI", 12F);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(new frmMainNEW());


            
        }
    }
}
