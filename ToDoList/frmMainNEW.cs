using DevExpress.Utils;
using DevExpress.XtraBars.Ribbon.BackstageView.Accessible;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraLayout.Customization;
using DevExpress.XtraRichEdit.Import.OpenXml;
using Newtonsoft.Json;
using RicettarioMG.Form.FormAddItem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToDoList.Class;
using ToDoList.MyPErsonalControl;
using ToDoList.Static;

namespace ToDoList
{
    public partial class frmMainNEW : DevExpress.XtraEditors.XtraForm
    {
        #region DICHIARAZIONI

        private static readonly byte[] key = Encoding.UTF8.GetBytes("1234567890123456"); // 16 byte per AES-128
        private static readonly byte[] iv = Encoding.UTF8.GetBytes("6543210987654321");  // 16 byte

        #region DICHIARAZIONI_TO_DOLIST
        private string fileToDoListOLD = "d1.dwgtodo";
        private string FileCategorieToDoListOLD = "c1.dwgtodo";

        private string fileToDoList = "d1.dwg_todo";
        private string FileCategorieToDoList = "c1.dwg_todo";
        private List<string> categorieToDoList = new List<string>();

        private BindingList<ToDoItem> ToDoListItems = new BindingList<ToDoItem>();
        //private BindingSource itemsBinding = new BindingSource();
        
        int nextId_ToDoList = 0;
        #endregion DICHIARAZIONI_TO_DOLIST

        #region DICHIARAZIONI_MEMO
        private string fileMemo = "d2.dwg_memo";
        private string fileMemoOLD = "d2.dwgtodo";
        #endregion DICHIARAZIONI_MEMO

        #region DICHIARAZIONI_MIO_ARCHIVIO
        private string fileMioArchivio = "d3.dwg_MioArchivio";
        private string fileMioArchvioCategorie = "c3.dwg_MioArchivio";

        private string fileMioArchivioOLD = "d3.dwgtodo";
        private string fileMioArchvioCategorieOLD = "c3.dwgtodo";
        private List<string> categorieMioArchivio = new List<string>();

        int nextId_MioArchivio = 0;

        private BindingList<MioArchivio> MioArchivioItems = new BindingList<MioArchivio>();

        #endregion DICHIARAZIONI_MIO_ARCHIVIO

        #region DICHIARAZIONI_INFO_ESTESE
        private string fileInfoEstesehivio = "d4.dwg_InfoEstese";
        private string fileInfoEstesehivioCategorie = "c4.dwg_InfoEstese";

        private string fileInfoEstesehivioOLD = "info.json";
        private string fileInfoEstesehivioCategorieOLD = "categorie.json";

        private List<string> categorieInfoEstese = new List<string>();
        private List<CategoriaInfoEstesa> categorieInfoEsteseOLD = new List<CategoriaInfoEstesa>();

        int nextId_InfoEstese = 0;

        private BindingList<InfoeEstesa> InfoEsteseItems = new BindingList<InfoeEstesa>();

        #endregion DICHIARAZIONI_INFO_ESTESE

        #region FILEPATH
        private string dataFolder;
        private string _todoFilePath;
        private string _todoCategorieFilePath;
        private string _todoFilePathOLD;
        private string _todoCategorieFilePathOLD;
        private string _memoFilePath;
        private string _memoFilePathOLD;
        private string _mioarchivioFilePath;
        private string _mioarchivioFilePathOLD;
        private string _mioarchivioCategorieFilePath;
        private string _mioarchivioCategorieFilePathOLD;
        private string _infoesteseFilePath;
        private string _infoesteseFilePathOLD;
        private string _infoesteseCategorieFilePath;
        private string _infoesteseCategorieFilePathOLD;

        #endregion FILEPATH

        #endregion DICHIARAZIONI

        #region Class
        public frmMainNEW()
        {
            InitializeComponent();

            dataFolder = Path.Combine(Application.StartupPath, "Data");

            // Se la cartella non esiste, creala
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);

            // Percorsi completi dei file JSON
            _todoFilePath = Path.Combine(dataFolder, fileToDoList);
            _todoCategorieFilePath = Path.Combine(dataFolder, FileCategorieToDoList);
            _todoFilePathOLD = Path.Combine(dataFolder, fileToDoListOLD);
            _todoCategorieFilePathOLD = Path.Combine(dataFolder, FileCategorieToDoListOLD);
            _memoFilePath = Path.Combine(dataFolder, fileMemo);
            _memoFilePathOLD = Path.Combine(dataFolder, fileMemoOLD);
            _mioarchivioFilePath = Path.Combine(dataFolder, fileMioArchivio);
            _mioarchivioCategorieFilePath = Path.Combine(dataFolder, fileMioArchvioCategorie);
            _mioarchivioFilePathOLD = Path.Combine(dataFolder, fileMioArchivioOLD);
            _mioarchivioCategorieFilePathOLD = Path.Combine(dataFolder, fileMioArchvioCategorieOLD);

            _infoesteseFilePath = Path.Combine(dataFolder, fileInfoEstesehivio);
            _infoesteseCategorieFilePath = Path.Combine(dataFolder, fileInfoEstesehivioCategorie);
            _infoesteseFilePathOLD = Path.Combine(dataFolder, fileInfoEstesehivioOLD);
            _infoesteseCategorieFilePathOLD = Path.Combine(dataFolder, fileInfoEstesehivioCategorieOLD);


            //TO DO LIST
            InitializeToDoList();
            LoadToDoListCategorie();
            LoadToDoList();

            //MEMO
            myRichMemo.LoadText(string.Empty);
            myRichMemo.ResetFontToDefault();
            LoadMemo();

            //MIO ARCHIVIO
            InitializeMioArchivio();
            LoadMioArchivioCategorie();
            LoadMioArchivio();

            //INFO ESTESE
            InitializeInfoEstese();
            LoadInfoEsteseCategorie();
            LoadInfoEstese();

        }

        private void frmMainNEW_Load(object sender, EventArgs e)
        {
            //setting the localizer
            GridLocalizer.Active = new CustomLocalizer();
        }
        #endregion Class

        #region f()
        #region f()_CRYTION
        private string EncryptString(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encrypted;

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(plainBytes, 0, plainBytes.Length);
                    }
                    encrypted = ms.ToArray();
                }

                return Convert.ToBase64String(encrypted);
            }
        }

        private string DecryptString(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                    }
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
        #endregion f()_CRYTION

        #region f()_TO_DO_LIST
        private void InitializeToDoList()
        {
            //carica la combo dello stato
            cmbStatoToDoList.Properties.Items.Clear();
            cmbStatoToDoList.Properties.Items.Add("URGENTE");
            cmbStatoToDoList.Properties.Items.Add("MEDIO");
            cmbStatoToDoList.Properties.Items.Add("NORMALE");
            cmbStatoToDoList.SelectedIndex = 0;

            gridView1.GroupPanelText = DEV_GridView.DWG_GroupPanelText;
            gridView1.OptionsBehavior.AutoPopulateColumns = true;
            ClearToDoList();


        }
        private void ClearToDoList()
        {
            txtNotaToDo.Text = string.Empty;    
            chkScadenza.Checked = false;
            slkupCategorieToDoList.EditValue = null;
            dateFine.EditValue = DateTime.Today;
            dateCreazione.EditValue = DateTime.Today;
            txtIDToDoList.EditValue = 0;
        }
        private void LoadToDoList()
        {
            ToDoListItems = new BindingList<ToDoItem>();
            gridControl1.DataSource = null;

            if (File.Exists(_todoFilePath))
            {
                try
                {
                    string json = File.ReadAllText(_todoFilePath);

                    var list = JsonConvert.DeserializeObject<List<ToDoItem>>(json) ?? new List<ToDoItem>();

                    // se vuoi, decodifica/trasforma campi qui (non necessario se non li hai crittati singolarmente)
                    ToDoListItems = new BindingList<ToDoItem>(list);
                }
                catch (Exception ex)
                {
                    MSGBOX.Error_Ok(ex, "Errore durante la lettura della lista ToDo:\r\n\r\n");
                    return;
                }

            }

            if (!File.Exists(_todoFilePath) && File.Exists(_todoFilePathOLD))
            {
                try
                {
                    string cryptedString = File.ReadAllText(_todoFilePathOLD);
                    string json = DecryptString(cryptedString);

                    var list = JsonConvert.DeserializeObject<List<ToDoItem>>(json) ?? new List<ToDoItem>();

                    // se vuoi, decodifica/trasforma campi qui (non necessario se non li hai crittati singolarmente)
                    ToDoListItems = new BindingList<ToDoItem>(list);

                    //li salva correttamente nel nuovo formato non criptato
                    SaveToDoList();
                }
                catch (Exception ex)
                {
                    MSGBOX.Error_Ok(ex, "Errore durante la lettura della lista ToDo:\r\n\r\n");
                    return;
                }
               
            }


            nextId_ToDoList = 0;
            if (ToDoListItems.Count > 0)
            {
                foreach (var item in ToDoListItems)
                {
                    if (item.ID > nextId_ToDoList)
                        nextId_ToDoList = item.ID;
                }
            }

            GridToDoListPrepare();



        }
        private void GridToDoListPrepare()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = ToDoListItems;
            gridControl1.DataSource = bs;

            gridView1.Columns["ID"].Visible = false;

            gridView1.Columns["Fatto"].VisibleIndex = 0;
            gridView1.Columns["Fatto"].Width = 60;
            gridView1.Columns["Fatto"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;

            gridView1.Columns["Categoria"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridView1.Columns["Categoria"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridView1.Columns["Categoria"].BestFit();

            gridView1.Columns["Nota"].Caption = "Cosa fare";
            gridView1.Columns["Nota"].BestFit();

            gridView1.Columns["Stato"].BestFit();

            gridView1.Columns["DataInserimento"].BestFit();
            gridView1.Columns["DataInserimento"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridView1.Columns["DataInserimento"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            gridView1.Columns["DataScadenza"].BestFit();
            gridView1.Columns["DataScadenza"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridView1.Columns["DataScadenza"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
        }
        private void SaveToDoList()
        {
            try
            {
                var cloneList = ToDoListItems.Select(x => new ToDoItem
                {
                    ID = x.ID,
                    Nota = x.Nota,
                    DataInserimento = x.DataInserimento,
                    DataScadenza = x.DataScadenza,
                    Categoria = x.Categoria,
                    Fatto = x.Fatto,
                    Stato = x.Stato
                }).ToList();

                string json = JsonConvert.SerializeObject(cloneList, Formatting.Indented);

                File.WriteAllText(_todoFilePath, json);
            }
            catch (Exception ex)
            {
                MSGBOX.Error_Ok(ex, "Errore durante il salvataggio della lista ToDo:\r\n\r\n");
            }
        }

        private void LoadToDoListCategorie()
        {
            categorieToDoList = new List<string>();
            slkupCategorieToDoList.Clear();

            if (File.Exists(_todoCategorieFilePath))
            {
                try
                {
                    // Legge il file cifrato
                    string json = File.ReadAllText(_todoCategorieFilePath);
                   
                    // Deserializza il JSON in una lista di stringhe
                    categorieToDoList = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
                }
                catch (Exception ex)
                {
                    MSGBOX.Error_Ok(ex,"Errore durante il caricamento delle categorie della ToDo List.\r\n\r\n");
                    return;
                }
            }

            if (!File.Exists(_todoCategorieFilePath) && File.Exists(_todoCategorieFilePathOLD))
            {
                try
                {
                    // Legge il file cifrato
                    string encryptedJson = File.ReadAllText(_todoCategorieFilePathOLD);

                    // Decritta il contenuto
                    string json = DecryptString(encryptedJson);

                    // Deserializza il JSON in una lista di stringhe
                    categorieToDoList = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();

                    //li salva correttamente nel nuovo formato non criptato
                    SaveToDoListCategorie();
                }
                catch (Exception ex)
                {
                    MSGBOX.Error_Ok(ex, "Errore durante il caricamento delle categorie della ToDo List.\r\n\r\n");
                    return;
                }
            }

            categorieToDoList.Sort();
            slkupCategorieToDoList.Properties.DataSource = categorieToDoList;
        }
        private void SaveToDoListCategorie()
        {
            try
            {
                // Serializza normalmente la lista
                string json = JsonConvert.SerializeObject(categorieToDoList, Formatting.Indented);

                // Scrive la stringa cifrata nel file
                File.WriteAllText(_todoCategorieFilePath, json);

            }
            catch (Exception ex)
            {
               MSGBOX.Error_Ok(ex,"Errore durante il salvataggio delle categorie della ToDo List:\r\n\r\n");
            }
        }
        private void CancellaCategoria()
        {
            if (slkupCategorieToDoList.EditValue == null)
                return;

            string categoria = slkupCategorieToDoList.EditValue.ToString();

            // conta quanti ToDoItem usano la categoria
            var items = ToDoListItems.Where(x => x.Categoria == categoria).ToList();

            int count = items.Count;

            if (count > 0)
            {
                if (MSGBOX.Question_YesNo($"Ci sono {count} cose da fare con la categoria '{categoria}'.\nSe continui la categoria verrà rimossa dagli elementi.\n\nContinuare?"))
                {
                    // rimuove la categoria dagli item
                    foreach (var item in items)
                        if (item.Categoria == categoria)
                            item.Categoria = string.Empty;
                    
                    SaveMioArchivio();

                    gridControl1.DataSource = null;
                    GridToDoListPrepare();

                }
                else
                {
                    return;
                }


            }

            // rimuove la categoria dalla lista
            categorieToDoList.Remove(categoria);

            slkupCategorieToDoList.EditValue = null;

            SaveToDoListCategorie();
        }
        #endregion f()_TO_DO_LIST

        #region f()_MEMO
        private void LoadMemo()
        {
            if (File.Exists(_memoFilePath))
            {
                try
                {
                    string json = File.ReadAllText(_memoFilePath);
                    var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    if (data != null && data.ContainsKey("Note"))
                    {
                        // Decifra il valore della nota
                        myRichMemo.LoadRtfText(data["Note"]);
                    }
                }
                catch (Exception ex)
                {
                    MSGBOX.Error_Ok(ex, "Errore durante il caricamento del memo:\r\n\r\n");
                }
            }

            if (!File.Exists(_memoFilePath) && File.Exists(_memoFilePathOLD))
            {
                try
                {
                    if (File.Exists(_memoFilePathOLD))
                    {
                        string json = File.ReadAllText(_memoFilePathOLD);
                        var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                        if (data != null && data.ContainsKey("Note"))
                        {
                            // Decifra il valore della nota
                            myRichMemo.LoadRtfText(DecryptString(data["Note"]));
                        }

                        SaveMemo();
                    }
                }
                catch (Exception ex)
                {
                    MSGBOX.Error_Ok(ex, "Errore durante il caricamento del memo:\r\n\r\n");
                }
            }

           
        }


        private void SaveMemo()
        {
            try
            {
                // Cifra solo il valore della nota
                var data = new Dictionary<string, string>
                {
                    ["Note"] = myRichMemo.GetRtfText()
                };

                string json = JsonConvert.SerializeObject(data, Formatting.Indented);

                File.WriteAllText(_memoFilePath, json);
            }
            catch (Exception ex)
            {
                MSGBOX.Error_Ok(ex, "Errore durante il salvataggio del memo:\r\n\r\n");
            }
        }
        #endregion f()_MEMO

        #region f()_MIO_ARCHIVIO
        private void InitializeMioArchivio()
        {
            gridView3.GroupPanelText = DEV_GridView.DWG_GroupPanelText;
            gridView1.OptionsBehavior.AutoPopulateColumns = true;
            ClearMioArchivio();
        }
        private void ClearMioArchivio()
        {
            txtInformazioneMioArchivio.Text = string.Empty;
            slkupCategorieMioArchivio.EditValue = null;
            txtIDMioArchivio.EditValue = 0;
            txtValoreMioArchivio.Text = string.Empty;
            txtValore2MioArchivio.Text = string.Empty;
        }

        private void LoadMioArchivio()
        {
            MioArchivioItems = new BindingList<MioArchivio>();
            gridView3.OptionsBehavior.AutoPopulateColumns = true;
            gridControl2.DataSource = null;
            nextId_MioArchivio = 0;

            if (File.Exists(_mioarchivioFilePath))
            {
                try
                {
                    string json = File.ReadAllText(_mioarchivioFilePath);

                    var list = JsonConvert.DeserializeObject<List<MioArchivio>>(json) ?? new List<MioArchivio>();

                    // se vuoi, decodifica/trasforma campi qui (non necessario se non li hai crittati singolarmente)
                    MioArchivioItems = new BindingList<MioArchivio>(list);

                }
                catch (Exception ex)
                {
                    MSGBOX.Error_Ok(ex, "Errore durante la lettura del mio arcihvio:\r\n\r\n");
                    return;
                }


                foreach (var item in MioArchivioItems)
                {
                    item.Valore = DecryptString(item.Valore);
                    if (!string.IsNullOrEmpty( item.Valore2))
                        item.Valore2 = DecryptString(item.Valore2);

                    if (item.ID > nextId_MioArchivio)
                        nextId_MioArchivio = item.ID;
                }
            }

            if (!File.Exists(_mioarchivioFilePath) && File.Exists(_mioarchivioFilePathOLD))
            {
                
                try
                {
                    if (File.Exists(_mioarchivioFilePathOLD))
                    {
                        string encryptedJson = File.ReadAllText(_mioarchivioFilePathOLD);
                        string json = DecryptString(encryptedJson);

                        var list = JsonConvert.DeserializeObject<List<MioArchivio>>(json) ?? new List<MioArchivio>();

                        // se vuoi, decodifica/trasforma campi qui (non necessario se non li hai crittati singolarmente)
                        MioArchivioItems  = new BindingList<MioArchivio>(list);

                        foreach (var item in MioArchivioItems)
                        {
                            if (item.ID > nextId_MioArchivio)
                                nextId_MioArchivio = item.ID;
                        }

                        SaveMioArchivio();
                    }
                    
                }
                catch (Exception ex)
                {
                    MSGBOX.Error_Ok(ex, "Errore durante la lettura del mio arcihvio:\r\n\r\n");
                    return;
                }
                
            }

            GridMioArchioPrepare();
        }

        private void GridMioArchioPrepare()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = MioArchivioItems;
            gridControl2.DataSource = bs;

            gridView3.Columns["ID"].Visible = false;

            gridView3.Columns["Categoria"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridView3.Columns["Categoria"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridView3.Columns["Categoria"].BestFit();

            gridView3.Columns["Nota"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridView3.Columns["Nota"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridView3.Columns["Nota"].BestFit();

            gridView3.Columns["Valore"].Caption = "Utente/ID";
            gridView3.Columns["Valore"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridView3.Columns["Valore"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridView3.Columns["Valore"].BestFit();

            gridView3.Columns["Valore2"].Caption = "Password";
            gridView3.Columns["Valore2"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridView3.Columns["Valore2"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridView3.Columns["Valore2"].BestFit();
        }

        private void SaveMioArchivio()
        {
            try
            {
                // (se vuoi cifrare anche i singoli campi non è necessario se cripti l'intero JSON)
                var cloneList = MioArchivioItems.Select(x => new MioArchivio
                {
                    ID = x.ID,
                    Nota = x.Nota,       // mantieni in chiaro qui: verrà cifrato insieme al JSON
                    Valore = EncryptString(x.Valore),
                    Valore2 = EncryptString(x.Valore2),
                    Categoria = x.Categoria
                }).ToList();

                string json = JsonConvert.SerializeObject(cloneList, Formatting.Indented);

                File.WriteAllText(_mioarchivioFilePath, json);
            }
            catch (Exception ex)
            {
                MSGBOX.Error_Ok(ex, "Errore durante il salvataggio del mio arcihvio:\r\n\r\n");
            }
           
        }

        private void LoadMioArchivioCategorie()
        {
            categorieMioArchivio = new List<string>();
            slkupCategorieMioArchivio.Clear();

            if (File.Exists(_mioarchivioCategorieFilePath))
            {
                try
                {
                    string json = File.ReadAllText(_mioarchivioCategorieFilePath);

                    categorieMioArchivio = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();

                }
                catch (Exception ex)
                {
                    MSGBOX.Error_Ok(ex, "Errore durante il caricamento delle categorie del Mio Archivio:\r\n\r\n");
                    return;
                }
            }

            if (!File.Exists(_mioarchivioCategorieFilePath) && File.Exists(_mioarchivioCategorieFilePathOLD))
            {
                if (File.Exists(_mioarchivioCategorieFilePathOLD))
                {
                    try
                    {
                        string encryptedJson = File.ReadAllText(_mioarchivioCategorieFilePathOLD);
                        string json = DecryptString(encryptedJson);

                        categorieMioArchivio = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
                        SaveMioArchivioCategorie();

                    }
                    catch (Exception ex)
                    {
                        MSGBOX.Error_Ok(ex, "Errore durante il caricamento delle categorie del Mio Archivio:\r\n\r\n");
                        return;
                    }
                }
             
            }

            categorieMioArchivio.Sort();
            slkupCategorieMioArchivio.Properties.DataSource = categorieMioArchivio;


        }
        private void SaveMioArchivioCategorie()
        {
    
            try
            {
                // Serializza normalmente la lista
                string json = JsonConvert.SerializeObject(categorieMioArchivio, Formatting.Indented);

                // Scrive la stringa cifrata nel file
                File.WriteAllText(_mioarchivioCategorieFilePath, json);
            }
            catch (Exception ex)
            {
                MSGBOX.Error_Ok(ex,"Errore durante il salvataggio delle categorie del Mio Archivio:\r\n\r\n");
            }
        }
        private void CancellaCategoriaMioArchivio()
        {
            if (slkupCategorieMioArchivio.EditValue == null)
                return;

            string categoria = slkupCategorieMioArchivio.EditValue.ToString();

            // conta quanti Items usano la categoria
            var items = MioArchivioItems.Where(x => x.Categoria == categoria).ToList();
            int count = items.Count;

            if (count > 0)
            {
                if (MSGBOX.Question_YesNo($"Ci sono {count} informazioni con la categoria '{categoria}'.\nSe continui la categoria verrà rimossa dagli elementi.\n\nContinuare?"))
                {
                    // rimuove la categoria dagli item
                    foreach (var item in items)
                        if (item.Categoria == categoria)
                            item.Categoria = string.Empty;

                    SaveMioArchivio();

                    gridControl2.DataSource = null;
                    GridMioArchioPrepare();
                }
                else
                    return;
     
            }

            // rimuove la categoria dalla lista
            categorieMioArchivio.Remove(categoria);

            slkupCategorieMioArchivio.EditValue = null;

            SaveMioArchivioCategorie();

        }
        #endregion f()_MIO_ARCHIVIO

        #region f()_INFO_ESTESE
        private void InitializeInfoEstese()
        {
            gridView5.GroupPanelText = DEV_GridView.DWG_GroupPanelText;
            gridView1.OptionsBehavior.AutoPopulateColumns = true;
            myRichInfoEstese.LoadText(string.Empty);
            myRichInfoEstese.ResetFontToDefault();
            ClearInfoEstese();
        }
        private void ClearInfoEstese()
        {
            slkupCategorieInfoEstese.EditValue = null;
            txtIDInfoEstese.EditValue = "0";
            txtTitoloInfoEstese.Text = string.Empty;
            myRichInfoEstese.LoadText(string.Empty);

        }

        private void LoadInfoEstese()
        {
            InfoEsteseItems = new BindingList<InfoeEstesa>();
            gridView5.OptionsBehavior.AutoPopulateColumns = true;
            gridControl3.DataSource = null;
            nextId_InfoEstese = 0;

            if (File.Exists(_infoesteseFilePath))
            {
                
                try
                {
                    string json = File.ReadAllText(_infoesteseFilePath);

                    var list = JsonConvert.DeserializeObject<List<InfoeEstesa>>(json) ?? new List<InfoeEstesa>();

                    // se vuoi, decodifica/trasforma campi qui (non necessario se non li hai crittati singolarmente)
                    InfoEsteseItems = new BindingList<InfoeEstesa>(list);

                }
                catch (Exception ex)
                {
                    MSGBOX.Error_Ok(ex, "Errore durante la lettura del mio arcihvio:\r\n\r\n");
                    return;
                }
           
                
            }

            if (!File.Exists(_infoesteseFilePath) && File.Exists(_infoesteseFilePathOLD))
            {

                try
                {
                    if (File.Exists(_infoesteseFilePathOLD))
                    {
                        string json = File.ReadAllText(_infoesteseFilePathOLD);

                        var list = JsonConvert.DeserializeObject<List<InfoeEstesa>>(json) ?? new List<InfoeEstesa>();

                        // se vuoi, decodifica/trasforma campi qui (non necessario se non li hai crittati singolarmente)
                        InfoEsteseItems = new BindingList<InfoeEstesa>(list);

                        SaveInfoEstese();
                    }

                }
                catch (Exception ex)
                {
                    MSGBOX.Error_Ok(ex, "Errore durante la lettura delle infromazioni estese:\r\n\r\n");
                    return;
                }

            }


            GridInfoEstesePrepare();

            foreach (var item in InfoEsteseItems)
            {
                if (item.ID > nextId_InfoEstese)
                    nextId_InfoEstese = item.ID;
            }
        }

        private void GridInfoEstesePrepare()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = InfoEsteseItems;
            gridControl3.DataSource = bs;

            gridView5.Columns["ID"].Visible = false;

            gridView5.Columns["Categoria"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gridView5.Columns["Categoria"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridView5.Columns["Categoria"].BestFit();

            gridView5.Columns["Titolo"].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Near;
            gridView5.Columns["Titolo"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
            gridView5.Columns["Titolo"].BestFit();

        }

        private void SaveInfoEstese()
        {
            try
            {
                // (se vuoi cifrare anche i singoli campi non è necessario se cripti l'intero JSON)
                var cloneList = InfoEsteseItems.Select(x => new InfoeEstesa
                {
                    ID = x.ID,
                    Testo = x.Testo,    
                    Titolo = x.Titolo,
                    Categoria = x.Categoria
                }).ToList();

                string json = JsonConvert.SerializeObject(cloneList, Formatting.Indented);

                File.WriteAllText(_infoesteseFilePath, json);
            }
            catch (Exception ex)
            {
                MSGBOX.Error_Ok(ex, "Errore durante il salvataggio delle infromazioni estese:\r\n\r\n");
            }
        }

        private void LoadInfoEsteseCategorie()
        {
            categorieInfoEstese= new List<string>();
            slkupCategorieInfoEstese.Clear();

            if (File.Exists(_infoesteseCategorieFilePath))
            {

                try
                {
                    string json = File.ReadAllText(_infoesteseCategorieFilePath);

                    categorieInfoEstese = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();

                }
                catch (Exception ex)
                {
                    MSGBOX.Error_Ok(ex, "Errore durante il caricamento delle categorie delle infromazioni estese:\r\n\r\n");
                    return;
                }
                
            }

            if (!File.Exists(_infoesteseCategorieFilePath) && File.Exists(_infoesteseCategorieFilePathOLD))
            {
                if (File.Exists(_infoesteseCategorieFilePathOLD))
                {
                    try
                    {
                        var json = File.ReadAllText(_infoesteseCategorieFilePathOLD);
                        categorieInfoEsteseOLD = JsonConvert.DeserializeObject<List<CategoriaInfoEstesa>>(json) ?? new List<CategoriaInfoEstesa>();

                        categorieInfoEstese = new List<string>();

                        foreach(CategoriaInfoEstesa item in categorieInfoEsteseOLD)
                        {
                            categorieInfoEstese.Add(item.Nome.ToString());
                        }

                        CancellaCategoriaInfoEstesa();

                    }
                    catch (Exception ex)
                    {
                        MSGBOX.Error_Ok(ex, "Errore durante il caricamento delle categorie delle infromazioni estese:\r\n\r\n");
                        return;
                    }
                }

            }

            categorieInfoEstese.Sort();
            slkupCategorieInfoEstese.Properties.DataSource = categorieInfoEstese;
        }
        private void SaveInfoEsteseCategorie()
        {
            try
            {
                // Serializza normalmente la lista
                string json = JsonConvert.SerializeObject(categorieInfoEstese, Formatting.Indented);

                // Scrive la stringa cifrata nel file
                File.WriteAllText(_infoesteseCategorieFilePath, json);
            }
            catch (Exception ex)
            {
                MSGBOX.Error_Ok(ex, "Errore durante il salvataggio delle categorie delle infromazioni estese:\r\n\r\n");
            }
        }

        private void CancellaCategoriaInfoEstesa()
        {
            if (slkupCategorieInfoEstese.EditValue == null)
                return;

            string categoria = slkupCategorieInfoEstese.EditValue.ToString();

            // conta quanti Items usano la categoria
            var items = InfoEsteseItems.Where(x => x.Categoria == categoria).ToList();
            int count = items.Count;

            if (count > 0)
            {
                if (MSGBOX.Question_YesNo($"Ci sono {count} informazioni estese con la categoria '{categoria}'.\nSe continui la categoria verrà rimossa dagli elementi.\n\nContinuare?"))
                {
                    // rimuove la categoria dagli item
                    foreach (var item in items)
                        if (item.Categoria == categoria)
                            item.Categoria = string.Empty;

                    SaveInfoEstese();

                    gridControl3.DataSource = null;
                    GridInfoEstesePrepare();
                }
                else
                    return;

            }

            // rimuove la categoria dalla lista
            categorieInfoEstese.Remove(categoria);

            slkupCategorieInfoEstese.EditValue = null;

            SaveInfoEsteseCategorie();
        }
        #endregion f()_INFO_ESTESE

        #endregion f()

        #region TO_DO_LIST
        private void slkupCategorieToDoList_AddNewValue(object sender, DevExpress.XtraEditors.Controls.AddNewValueEventArgs e)
        {
            frmAddItemTDL f = new frmAddItemTDL(categorieToDoList, TipoADD.TA_LISTTODO );
            f.ShowDialog();
            if (f.Tag.ToString() == "OK")
            {
                string nuovo = f.rValue;

                categorieToDoList.Add(nuovo);   // lista collegata al datasource
                categorieToDoList.Sort();

                SaveToDoListCategorie();

                slkupCategorieToDoList.Properties.DataSource = null;
                slkupCategorieToDoList.Properties.DataSource = categorieToDoList;

                slkupCategorieToDoList.EditValue = nuovo;

                e.NewValue = nuovo;

                
            }
            f.Dispose();


        }

        private void slkupCategorieToDoList_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (slkupCategorieToDoList.EditValue == null)
                    return;

                CancellaCategoria();
            }

            
        }

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            ClearToDoList();
        }

        private void btnSalvaToDoList_Click(object sender, EventArgs e)
        {
            txtNotaToDo.Text = txtNotaToDo.Text.Trim();
            bool IsNew = false;

            if (string.IsNullOrWhiteSpace(txtNotaToDo.Text))
            {
                MessageBox.Show("Inserisci una nota prima di salvare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtIDToDoList.EditValue != null && Convert.ToInt32(txtIDToDoList.EditValue) == 0)
            {
                nextId_ToDoList++;
                txtIDToDoList.EditValue = nextId_ToDoList;
                IsNew  = true;
            }

            var item = new ToDoItem
            {
                ID = Convert.ToInt32(txtIDToDoList.EditValue),
                Nota = txtNotaToDo.Text,
                DataInserimento = DateTime.Today,
                DataScadenza = chkScadenza.Checked ? Convert.ToDateTime(dateFine.EditValue) : (DateTime?)null,
                Fatto = chkScadenza.Checked,
                Stato = cmbStatoToDoList.SelectedItem.ToString(),
                Categoria = slkupCategorieToDoList.EditValue != null ? slkupCategorieToDoList.EditValue.ToString() : string.Empty
            };

            if(IsNew)
                ToDoListItems.Add(item);
            else
            {
                foreach (var itemOLD in ToDoListItems)
                {
                    if (itemOLD.ID == Convert.ToInt32(txtIDToDoList.EditValue))
                    {
                        itemOLD.Fatto = chkScadenza.Checked;
                        itemOLD.Nota = txtNotaToDo.Text;
                        itemOLD.DataScadenza = chkScadenza.Checked ? Convert.ToDateTime(dateFine.EditValue) : (DateTime?)null;
                        itemOLD.Stato = cmbStatoToDoList.SelectedItem.ToString();
                        itemOLD.Categoria = slkupCategorieToDoList.EditValue != null ? slkupCategorieToDoList.EditValue.ToString() : string.Empty;
                        break;
                    }
                }
            }

            SaveToDoList();
            ClearToDoList();

            gridControl1.DataSource = null;
            GridToDoListPrepare();

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            gridView1_KeyDown(gridView1, new KeyEventArgs(Keys.Return));
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (!string.IsNullOrEmpty(gridView1.GetFocusedRowCellValue("ID").ToString()))
                {
                    int _id = (Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID")));
                    ToDoItem item = ToDoListItems.FirstOrDefault(x => x.ID == _id);
                    if (item != null)
                    {
                        txtIDToDoList.EditValue = item.ID;
                        chkScadenza.Checked = item.DataScadenza.HasValue;
                        dateCreazione.EditValue = item.DataInserimento;
                        dateFine.EditValue = item.DataScadenza ?? DateTime.Today;
                        slkupCategorieToDoList.EditValue = null;
                        if (!string.IsNullOrEmpty(item.Categoria))
                            slkupCategorieToDoList.EditValue = item.Categoria;
                        txtNotaToDo.Text = item.Nota;
                        cmbStatoToDoList.EditValue = item.Stato;
                        chkScadenza.Checked = item.Fatto;
                    }

                }

            }
            else
            {
                if (gridView1.RowCount > 0)
                {
                    if (!string.IsNullOrEmpty(gridView1.GetFocusedRowCellValue("ID").ToString()))
                    {
                        int _id = (Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID")));
                        ToDoItem item = ToDoListItems.FirstOrDefault(x => x.ID == _id);
                        if (item != null)
                        {
                            if (MSGBOX.Question_YesNo("Vuoi eliminare questa elemento?"))
                            {
                                ToDoListItems.Remove(item);
                                SaveToDoList();
                                gridControl1.DataSource = null;
                                GridToDoListPrepare();
                            }

                        }
                    }
                }
            }

        }

        #endregion TO_DO_LIST

        #region MEMO
        private void btnSalvaMemo_Click(object sender, EventArgs e)
        {
            SaveMemo();
        }

        #endregion MEMO

        #region MIO_ARCHIVIO
        private void btnAnnullaMioArchivio_Click(object sender, EventArgs e)
        {
            ClearMioArchivio();
        }
        private void btnSalvaMioArchivio_Click(object sender, EventArgs e)
        {
            txtInformazioneMioArchivio.Text = txtInformazioneMioArchivio.Text.Trim();
            bool IsNew = false;

            if (string.IsNullOrWhiteSpace(txtInformazioneMioArchivio.Text))
            {
                MessageBox.Show("Inserisci una informazione prima di salvare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (txtIDMioArchivio.EditValue != null && Convert.ToInt32(txtIDMioArchivio.EditValue) == 0)
            {
                nextId_MioArchivio++;
                txtIDMioArchivio.EditValue = nextId_MioArchivio;
                IsNew = true;
            }

            var item = new MioArchivio
            {
                ID = Convert.ToInt32(txtIDMioArchivio.EditValue),
                Nota = txtInformazioneMioArchivio.Text,
                Valore = txtValoreMioArchivio.Text.Trim(),
                Valore2 = txtValore2MioArchivio.Text.Trim(),
                Categoria = slkupCategorieMioArchivio.EditValue != null ? slkupCategorieMioArchivio.EditValue.ToString() : string.Empty
            };


            if (IsNew)
                MioArchivioItems.Add(item);
            else
            {
                foreach (var itemOLD in MioArchivioItems)
                {
                    if (itemOLD.ID == Convert.ToInt32(txtIDMioArchivio.EditValue))
                    {
                        itemOLD.Nota = txtInformazioneMioArchivio.Text;
                        itemOLD.Valore = txtValoreMioArchivio.Text.Trim();
                        itemOLD.Valore2 = txtValore2MioArchivio.Text.Trim();
                        itemOLD.Categoria = slkupCategorieMioArchivio.EditValue != null ? slkupCategorieMioArchivio.EditValue.ToString() : string.Empty;
                        break;
                    }
                }
            }

            SaveMioArchivio();
            ClearMioArchivio();

            gridControl2.DataSource = null;
            GridMioArchioPrepare();

        }
        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            gridView3_KeyDown(gridView3, new KeyEventArgs(Keys.Return));
        }
        private void gridView3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (!string.IsNullOrEmpty(gridView3.GetFocusedRowCellValue("ID").ToString()))
                {
                    int _id = (Convert.ToInt32(gridView3.GetFocusedRowCellValue("ID")));
                    MioArchivio item = MioArchivioItems.FirstOrDefault(x => x.ID == _id);
                    if (item != null)
                    {
                        txtIDMioArchivio.EditValue = item.ID;
                        txtValoreMioArchivio.Text = item.Valore;
                        txtValore2MioArchivio.Text = item.Valore2;
                        txtInformazioneMioArchivio.Text = item.Nota;
                        slkupCategorieMioArchivio.EditValue = null;
                        if (!string.IsNullOrEmpty(item.Categoria))
                            slkupCategorieMioArchivio.EditValue = item.Categoria;

                    }

                }

            }
            else
            {
                if (gridView3.RowCount > 0)
                {
                    if (!string.IsNullOrEmpty(gridView3.GetFocusedRowCellValue("ID").ToString()))
                    {
                        int _id = (Convert.ToInt32(gridView3.GetFocusedRowCellValue("ID")));
                        MioArchivio item = MioArchivioItems.FirstOrDefault(x => x.ID == _id);
                        if (item != null)
                        {
                            if (MSGBOX.Question_YesNo("Vuoi eliminare questa elemento?"))
                            {
                                MioArchivioItems.Remove(item);
                                SaveMioArchivio();
                                gridControl2.DataSource = null;
                                GridMioArchioPrepare();
                            }

                        }
                    }
                }
            }
        }
        private void slkupCategorieMioArchivio_AddNewValue(object sender, AddNewValueEventArgs e)
        {
            frmAddItemTDL f = new frmAddItemTDL(categorieMioArchivio, TipoADD.TA_MIO_ARCHIVIO );
            f.ShowDialog();
            if (f.Tag.ToString() == "OK")
            {
                string nuovo = f.rValue;

                categorieMioArchivio.Add(nuovo);   // lista collegata al datasource
                categorieMioArchivio.Sort();

                SaveMioArchivioCategorie();

                slkupCategorieMioArchivio.Properties.DataSource = null;
                slkupCategorieMioArchivio.Properties.DataSource = categorieMioArchivio;

                slkupCategorieMioArchivio.EditValue = nuovo;

                e.NewValue = nuovo;


            }
            f.Dispose();
        }

        private void slkupCategorieMioArchivio_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (slkupCategorieMioArchivio.EditValue == null)
                    return;

                CancellaCategoriaMioArchivio();
            }
        }

        #endregion MIO_ARCHIVIO

        #region INFO_ESTESE
        private void btnViewGridToLoadInfoEstese_Click(object sender, EventArgs e)
        {
            gridControl3.BringToFront();
        }
        private void btnAnnullaInfoSalvate_Click(object sender, EventArgs e)
        {
            myRichInfoEstese.BringToFront();
            ClearInfoEstese();

        }
        private void btnSalvaInfoEstese_Click(object sender, EventArgs e)
        {
            txtTitoloInfoEstese.Text = txtTitoloInfoEstese.Text.Trim();
            bool IsNew = false;

            if (string.IsNullOrWhiteSpace(txtTitoloInfoEstese.Text))
            {
                MSGBOX.Warning_Ok("Inserisci un titolo prima di salvare.");
                return;
            }


            if (txtIDInfoEstese.EditValue != null && Convert.ToInt32(txtIDInfoEstese.EditValue) == 0)
            {
                nextId_InfoEstese++;
                txtIDInfoEstese.EditValue = nextId_InfoEstese;
                IsNew = true;
            }

            var item = new InfoeEstesa
            {
                ID = Convert.ToInt32(txtIDInfoEstese.EditValue),
                Titolo = txtTitoloInfoEstese.Text,
                Testo = myRichInfoEstese.GetRtfText(),
                Categoria = slkupCategorieInfoEstese.EditValue != null ? slkupCategorieInfoEstese.EditValue.ToString() : string.Empty
            };


            if (IsNew)
                InfoEsteseItems.Add(item);
            else
            {
                foreach (var itemOLD in InfoEsteseItems)
                {
                    if (itemOLD.ID == Convert.ToInt32(txtIDInfoEstese.EditValue))
                    {
                        itemOLD.Titolo = txtTitoloInfoEstese.Text;
                        itemOLD.Testo = myRichInfoEstese.GetRtfText();
                        itemOLD.Categoria = slkupCategorieInfoEstese.EditValue != null ? slkupCategorieInfoEstese.EditValue.ToString() : string.Empty;
                        break;
                    }
                }
            }

            SaveInfoEstese();
            ClearInfoEstese();

            gridControl3.DataSource = null;
            GridInfoEstesePrepare();

        }
        private void gridView5_DoubleClick(object sender, EventArgs e)
        {
            gridView5_KeyDown(gridView5, new KeyEventArgs(Keys.Return));
        }
        private void gridView5_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Return)
            {
                if (!string.IsNullOrEmpty(gridView5.GetFocusedRowCellValue("ID").ToString()))
                {
                    int _id = (Convert.ToInt32(gridView5.GetFocusedRowCellValue("ID")));
                    InfoeEstesa item = InfoEsteseItems.FirstOrDefault(x => x.ID == _id);
                    if (item != null)
                    {
                        txtIDInfoEstese.EditValue = item.ID;
                        txtTitoloInfoEstese.Text = item.Titolo;
                        slkupCategorieInfoEstese.EditValue = null;
                        if (!string.IsNullOrEmpty(item.Categoria))
                            slkupCategorieInfoEstese.EditValue = item.Categoria;
                        string formattedText = item.Testo;
                        myRichInfoEstese.LoadRtfText(formattedText);
                        myRichInfoEstese.BringToFront();

                    }

                }

            }
            else
            {
                if (gridView5.RowCount > 0)
                {
                    if (!string.IsNullOrEmpty(gridView5.GetFocusedRowCellValue("ID").ToString()))
                    {
                        int _id = (Convert.ToInt32(gridView5.GetFocusedRowCellValue("ID")));
                        InfoeEstesa item = InfoEsteseItems.FirstOrDefault(x => x.ID == _id);
                        if (item != null)
                        {
                            if (MSGBOX.Question_YesNo("Vuoi eliminare questa elemento?"))
                            {
                                InfoEsteseItems.Remove(item);
                                SaveInfoEstese();
                                gridControl3.DataSource = null;
                                GridInfoEstesePrepare();
                            }
                           
                        }
                    }
                }
            }
        }
        private void slkupCategorieInfoEstese_AddNewValue(object sender, AddNewValueEventArgs e)
        {
            frmAddItemTDL f = new frmAddItemTDL(categorieMioArchivio, TipoADD.TA_INFO_ESTESA);
            f.ShowDialog();
            if (f.Tag.ToString() == "OK")
            {
                string nuovo = f.rValue;

                categorieInfoEstese.Add(nuovo);   // lista collegata al datasource
                categorieInfoEstese.Sort();

                SaveInfoEsteseCategorie();

                slkupCategorieInfoEstese.Properties.DataSource = null;
                slkupCategorieInfoEstese.Properties.DataSource = categorieInfoEstese;

                slkupCategorieInfoEstese.EditValue = nuovo;

                e.NewValue = nuovo;


            }
            f.Dispose();
        }
        private void slkupCategorieInfoEstese_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (slkupCategorieInfoEstese.EditValue == null)
                    return;

                CancellaCategoriaInfoEstesa();
            }
        }
        #endregion INFO_ESTESE
    }
}