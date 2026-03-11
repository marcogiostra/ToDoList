using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList.Static
{
    static public class MSGBOX
    {
        private const string AppTitle = "Dev With Giosh Office Utility";

        //@@@public static object XtraMessageBox { get; private set; }

        public static void Warning_Ok(string pMessaggio, string pTitle = "")
        {
            Cursor.Current = Cursors.Default;
            XtraMessageBox.Show(pMessaggio, (string.IsNullOrEmpty(pTitle) ? AppTitle : pTitle), MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
        public static void Warning_Error_Ok(Exception pEx, string pMessaggio, string pTitle = "")
        {
            Cursor.Current = Cursors.Default;
            XtraMessageBox.Show(pMessaggio + (Funzioni.IsDebugMode ? pEx.ToString() : pEx.Message), (string.IsNullOrEmpty(pTitle) ? AppTitle : pTitle), MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
        public static void Error_Ok(Exception pEx, string pMessaggio, string pTitle = "")
        {
            Cursor.Current = Cursors.Default;
            XtraMessageBox.Show(pMessaggio + (Funzioni.IsDebugMode ? pEx.ToString() : pEx.Message), (string.IsNullOrEmpty(pTitle) ? AppTitle : pTitle), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static bool Error_YesNo(Exception pEx, string pMessaggio, string pTitle = "")
        {
            return (XtraMessageBox.Show(pMessaggio + (Funzioni.IsDebugMode ? pEx.ToString() : pEx.Message), (string.IsNullOrEmpty(pTitle) ? AppTitle : pTitle), MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes);

        }
        public static bool Question_YesNo(string pMessaggio, string pTitle = "")
        {
            Cursor.Current = Cursors.Default;
            return (XtraMessageBox.Show(pMessaggio, (string.IsNullOrEmpty(pTitle) ? AppTitle : pTitle), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);

        }
        public static void Exclamation_Ok(string pMessaggio, string pTitle = "")
        {
            Cursor.Current = Cursors.Default;
            XtraMessageBox.Show(pMessaggio, (string.IsNullOrEmpty(pTitle) ? AppTitle : pTitle), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }
        public static void Stop_Ok(string pMessaggio, string pTitle = "")
        {
            Cursor.Current = Cursors.Default;
            XtraMessageBox.Show(pMessaggio, (string.IsNullOrEmpty(pTitle) ? AppTitle : pTitle), MessageBoxButtons.OK, MessageBoxIcon.Stop);

        }


    }
}

