using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ToDoList.Static;


namespace RicettarioMG.Form.FormAddItem
{
    public enum TipoADD
    {
        TA_LISTTODO,
        TA_MIO_ARCHIVIO,
        TA_INFO_ESTESA
    }
    public partial class frmAddItemTDL : XtraForm
    {
        #region DICHIARAZIONI
        private List<string> _CATs = new List<string>();
        public string rValue = string.Empty;
        #endregion DICHIARAZIONI

        #region Class
        public frmAddItemTDL(List<string> pCategorie, TipoADD pTipo)
        {
            _CATs = pCategorie;

            InitializeComponent();

            switch (pTipo)
            {
                case TipoADD.TA_LISTTODO:
                    this.Text = "Aggiungi una nuova categoria per le cose da fare";
                    Label1.Text = "Categoria";
                    break;
                case TipoADD.TA_MIO_ARCHIVIO:
                    this.Text = "Aggiungi una nuovca categoria per il mio arcihvio";
                    Label1.Text = "Categoria";
                    break;

                case TipoADD.TA_INFO_ESTESA:
                    this.Text = "Aggiungi una nuova categoria per le informazioni estese";
                    Label1.Text = "Categoria";
                    break;
            }
        }

        private void frmAddItemCategoria_Load(object sender, EventArgs e)
        {
            this.Tag = "NO";
        }
        #endregion Class

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvaChiudi_Click(object sender, EventArgs e)
        {
            SalvaChiudi(txtNew.Text.Trim());
        }

        private void txtNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SalvaChiudi(txtNew.Text.Trim());
            }
        }

        public void SalvaChiudi(string pNewValue)
        {
            string ans = pNewValue;

            if (string.IsNullOrEmpty(pNewValue))
            {
                MSGBOX.Warning_Ok("Il nuovo valore è nullo!");
                return;
            }

            foreach (string s in _CATs)
            {
                if (s.ToUpper() == ans.ToUpper())
                {
                    MSGBOX.Warning_Ok("IL nuovo valore esiste già!");
                    return;
                }
            }

            rValue = pNewValue;
            this.Tag = "OK";
            this.Close();


        }

    }
}

