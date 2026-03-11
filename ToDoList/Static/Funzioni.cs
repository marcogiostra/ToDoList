using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList.Static
{
    public enum eKEYs
    {
        Key_NOKEY = 0,
        Key_DELETE = 1,
        Key_INSERT = 2,
        Key_ENTER = 3,
        Key_F1 = 4,
        Key_F2 = 5,
        Key_F9 = 6,
        Key_F10 = 7,
        Key_F11 = 8,
        Key_F12 = 9,
        Key_E = 10

    }

    public class Funzioni
    {
        private static string _SaveoOrginaPath = "C:\\";
        private static string SaveOriginaPath
        {
            get
            {
                return _SaveoOrginaPath;
            }

            set
            {
                _SaveoOrginaPath = value;
            }
        }

 
        #region TEST
        public static bool IsDebugMode
        {
            get
            {
                bool answer = false;
#if DEBUG
                answer = true;
#endif
                return answer;
            }

        }
        public static bool IsDesignMode
        {
            get
            {
                return (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            }
        }

        /*
        public static bool TestComboINT (int pValue, ComboBox pCombo, cComboSqlClient pCmbSqql)
        {
            if ((pValue == 0 && pCombo.SelectedIndex == -1) || (pValue > 0 && pCombo.SelectedIndex > -1 && pValue == Convert.ToInt32(pCmbSqql.SelectedKey)))
                return true;

            return false;
        }

        public static bool TestComboSTRING(string pValue, ComboBox pCombo, cComboSqlClient pCmbSqql)
        {
            if ((string.IsNullOrEmpty(pValue) && pCombo.SelectedIndex == -1) || (!string.IsNullOrEmpty(pValue) && pCombo.SelectedIndex > -1 && pValue == pCmbSqql.SelectedKey))
                return true;

            return false;
        }
        */
        #endregion TEST

        #region KEYDOWN
        public static eKEYs KeyDownControlWithCTRL(Control pControl, KeyEventArgs pE)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                return MasterKeyDown(pE);
            }

            return eKEYs.Key_NOKEY;
        }
        public static eKEYs KeyDownGridViewWithCTRL(KeyEventArgs pE)
        {

            // Verifica se i tasti Ctrl e Canc sono premuti
            if (pE.Control)
            {
                return MasterKeyDown(pE);

            }

            return eKEYs.Key_NOKEY;
        }

        public static eKEYs KeyDownGridViewWithSHIFT(KeyEventArgs pE)
        {

            // Verifica se i tasti Ctrl e Canc sono premuti
            if (pE.Shift)
            {
                return MasterKeyDown(pE);

            }

            return eKEYs.Key_NOKEY;
        }

        private static eKEYs MasterKeyDown(KeyEventArgs pE)
        {
            switch (pE.KeyCode)
            {
                case Keys.Delete:
                    return eKEYs.Key_DELETE;

                case Keys.Insert:
                    return eKEYs.Key_INSERT;

                case Keys.Enter:
                    return eKEYs.Key_ENTER;

                case Keys.F1:
                    return eKEYs.Key_F1;

                case Keys.F2:
                    return eKEYs.Key_F2;

                case Keys.F9:
                    return eKEYs.Key_F9;

                case Keys.F10:
                    return eKEYs.Key_F10;

                case Keys.F11:
                    return eKEYs.Key_F11;

                case Keys.F12:
                    return eKEYs.Key_F12;

                case Keys.E:
                    return eKEYs.Key_E;
            }

            return eKEYs.Key_NOKEY;
        }
        #endregion KEYDOWN

        #region MOUSE
        public static void PicMouseEntere(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            pic.BackColor = Color.FromArgb(255, 229, 241, 251);
            pic.Tag = "blu";
            pic.Refresh();
        }
        public static void PicMouseLeach(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            pic.BackColor = Color.Gainsboro;
            pic.Tag = "Gainsboro";
            pic.Refresh();
        }
        #endregion MOUSE

        #region DEVEXPRESS

        /// <summary>
        /// FUNZIONE: esporta una GridView in Excel dato un FilePath
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="filePath"></param>
        private static void DEV_ExportGridToExcel_FromPath(GridView pGridView, string pFilePath)
        {
            // Configura le opzioni di esportazione
            XlsxExportOptionsEx exportOptions = new XlsxExportOptionsEx
            {
                ExportType = DevExpress.Export.ExportType.WYSIWYG,  // Esporta solo le colonne visibili
                AllowGrouping = DevExpress.Utils.DefaultBoolean.False,  // Evita raggruppamenti nell'export
                ShowGridLines = true  // Mostra le linee della griglia nel file Excel
            };

            // Esporta il GridView nel file Excel specificato
            pGridView.ExportToXlsx(pFilePath, exportOptions);
        }

        /// <summary>
        /// FUNZIONE: esporta una GridView in Excel
        /// </summary>
        /// <param name="pGridView"></param>

        public static void DEV_ExportGridToExcel(GridView pGridView)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                Title = "Indica Excel File"


            };

            if (Directory.Exists(SaveOriginaPath))
                saveFileDialog.InitialDirectory = SaveOriginaPath;


            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(saveFileDialog.FileName))
                {
                    if (!MSGBOX.Question_YesNo("IL file già esiste. Vuoi sovrascriverlo?", "Esprotazione griglia in Excel"))
                        return;
                }

                string filePath = saveFileDialog.FileName;
                DEV_ExportGridToExcel_FromPath(pGridView, filePath);
                string file = Path.GetFileNameWithoutExtension(filePath);
                SaveOriginaPath = filePath.Substring(0, filePath.Length - file.Length - 5);
            }

        }


        #endregion DEVEXPRESS

        #region LASCIO_E_RILANCIO
        public static void RestartApplicationMANUTE()
        {
            // Ottieni il percorso dell'eseguibile dell'applicazione
            string appPath = Application.ExecutablePath;

            // Avvia una nuova istanza dell'applicazione
            Process.Start(appPath);

            // Chiudi l'istanza corrente
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        #endregion LASCIO_E_RILANCIO

        #region f()_STRING
        public static bool STR_StartsWithNumber(string pInput)
        {
            return !string.IsNullOrEmpty(pInput) && char.IsDigit(pInput[0]);
        }
        public static bool STR_StartsWithLetterAndColon(string input)
        {
            return input.Length >= 2 && char.IsLetter(input[0]) && input[1] == ':';
        }
        public static bool STR_ContainsManute(string pInput)
        {
            return !string.IsNullOrEmpty(pInput) && pInput.ToUpper().Contains("MANUTE");
        }
        #endregion f()_STRING

        /*
        public static void LookEditSearchString(LookUpEdit pLook, SearchLookUpEdit pSearch, string pKey, List<ClasseLista_String_String> pItems)
        {
            if (string.IsNullOrEmpty(pKey))
                if (pLook != null)
                    pLook.EditValue = null;
                else
                    pSearch.EditValue = null;
            else
            {
                var predefinitoItem = pItems.FirstOrDefault(x => x.Key == pKey);
                if (predefinitoItem != null)
                {
                    if (pLook != null)
                        pLook.EditValue = predefinitoItem.Key;
                    else
                        pSearch.EditValue = predefinitoItem.Key;

                }
            }
        }

        public static void LookEditSearchInt(LookUpEdit pLook, SearchLookUpEdit pSearch, int pKey, List<ClasseLista_Int_String> pItems)
        {
            if (pKey < 1)
                if (pLook != null)
                    pLook.EditValue = null;
                else
                    pSearch.EditValue = null;
            else
            {
                var predefinitoItem = pItems.FirstOrDefault(x => x.Key == pKey);
                if (predefinitoItem != null)
                {
                    if (pLook != null)
                        pLook.EditValue = predefinitoItem.Key;
                    else
                        pSearch.EditValue = predefinitoItem.Key;

                }
            }
        }



    }
*/

        public static string GetVersion()
        {
            // Ottieni l'oggetto Assembly corrente
            var assembly = System.Reflection.Assembly.GetEntryAssembly();

            // Ottieni l'oggetto dell'attributo AssemblyVersion
            var versionAttribute = assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyFileVersionAttribute), false) as System.Reflection.AssemblyFileVersionAttribute[];

            // Se non c'è alcun attributo di versione del file, restituisci una stringa vuota
            if (versionAttribute == null || versionAttribute.Length == 0)
                return string.Empty;

            // Ottieni la versione del file
            var version = versionAttribute[0].Version;

            return version;
        }

        public static Int64 GetVersionNumeric()
        {
            // Ottieni l'oggetto Assembly corrente
            var assembly = System.Reflection.Assembly.GetEntryAssembly();

            // Ottieni l'oggetto dell'attributo AssemblyVersion
            var versionAttribute = assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyFileVersionAttribute), false) as System.Reflection.AssemblyFileVersionAttribute[];

            // Se non c'è alcun attributo di versione del file, restituisci una stringa vuota
            if (versionAttribute == null || versionAttribute.Length == 0)
                return 0;

            // Ottieni la versione del file
            var version = versionAttribute[0].Version;

            return 0;
        }

    }
}
