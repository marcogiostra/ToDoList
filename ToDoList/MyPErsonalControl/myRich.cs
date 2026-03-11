using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList.MyPErsonalControl
{
    public partial class myRich : DevExpress.XtraEditors.XtraUserControl
    {
        private string originalRtf;

        #region Class
        public myRich()
        {
            InitializeComponent();
        }

        private void myRich_Load(object sender, EventArgs e)
        {
            Document doc = richEditControl1.Document;

            // Documento vuoto → imposta font di default
            doc.DefaultCharacterProperties.FontName = "Segoe UI";
            doc.DefaultCharacterProperties.FontSize = 12f;
        }
        #endregion Class

        //Resetta il font alle impostazioni di default
        public void ResetFontToDefault()
        {
            Document doc = richEditControl1.Document;
            doc.BeginUpdate();
            try
            {
                // Imposta il font di default
                doc.DefaultCharacterProperties.FontName = "Segoe UI";
                doc.DefaultCharacterProperties.FontSize = 12f;
                // Applica il font di default a tutto il documento
                doc.SelectAll();
                CharacterProperties cp = doc.BeginUpdateCharacters(doc.Selection);
                cp.FontName = "Segoe UI";
                cp.FontSize = 12f;
                doc.EndUpdateCharacters(cp);
            }
            finally
            {
                doc.EndUpdate();
            }
        }

        //Carica testo semplice (stringa)
        public void LoadText(string text)
        {
            richEditControl1.Document.Text = text;
        }

        //Carica da file (DOCX, RTF, TXT, ecc.)
        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                richEditControl1.LoadDocument(filePath);
            }
        }

        //Salva su file
        public void SaveToFile(string filePath)
        {
            var ext = Path.GetExtension(filePath).ToLower();

            switch (ext)
            {
                case ".rtf":
                    richEditControl1.SaveDocument(filePath, DocumentFormat.Rtf);
                    break;
                case ".docx":
                    richEditControl1.SaveDocument(filePath, DocumentFormat.OpenXml);
                    break;
                case ".txt":
                    richEditControl1.SaveDocument(filePath, DocumentFormat.PlainText);
                    break;
                default:
                    richEditControl1.SaveDocument(filePath, DocumentFormat.Rtf);
                    break;
            }
        }

        //Ottieni il testo come stringa (senza formattazione)
        public string GetPlainText()
        {
            return richEditControl1.Text;
        }

        //Ottieni il testo come RTF (con formattazione)
        public string GetRtfText()
        {
       

            // Se il documento non è stato modificato, ritorniamo esattamente l’RTF originale
            if (!richEditControl1.Modified)
                return originalRtf;

            using (var ms = new MemoryStream())
            {
                richEditControl1.SaveDocument(ms, DocumentFormat.Rtf);
                return System.Text.Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        //Imposta il testo come RTF (con formattazione)
        public void LoadRtfText(string rtf)
        {
     

            if (string.IsNullOrEmpty(rtf))
                return;

            originalRtf = rtf;  // Salviamo l'RTF originale

            var bytes = System.Text.Encoding.UTF8.GetBytes(rtf);
            using (var ms = new MemoryStream(bytes))
            {
                richEditControl1.LoadDocument(ms, DocumentFormat.Rtf);
            }

            richEditControl1.Modified = false; // resetta lo stato modificato
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            // Quando ucRich prende il focus, lo passo al RichEditControl
            if (richEditControl1 != null && richEditControl1.CanFocus)
            {
                richEditControl1.Focus();
            }



        }

    }
}
