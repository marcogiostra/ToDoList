using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace ToDoList
{
    public partial class frmMain : Form
    {
        #region DICXHIARAZIONI


        #region DICHIARAZIONI_TO_DOLIST
        private string fileToDoList = "d1.dwgtodo";

        private string FileCategorieToDoList = "c1.dwgtodo";
        private List<string> categorie = new List<string>();

        int nextId = 0;

        private ContextMenuStrip dgvContextMenu;

        private string lastSortedColumn = "";
        private bool sortAscending = true; // true = crescente, false = decrescente


        private BindingList<ToDoItem> items = new BindingList<ToDoItem>();
        private BindingSource itemsBinding = new BindingSource();

        #endregion DICHIARAZIONI_TO_DOLIST

        #region DICHIARAZIONI_MIO_ARCHIVIO
        private string fileMioArchivio = "d3.dwgtodo";

        private string fileMioArchvioCategorie = "c3.dwgtodo";
        private List<string> categorie2 = new List<string>();

        int nextId2 = 0;

        private ContextMenuStrip dgvContextMenu2;

        private string lastSortedColumn2 = "";
        private bool sortAscending2 = true; // true = crescente, false = decrescente


        private BindingList<MioArchivio> DBitems = new BindingList<MioArchivio>();
        private BindingSource DBitemsBinding = new BindingSource();

        private static readonly byte[] key = Encoding.UTF8.GetBytes("1234567890123456"); // 16 byte per AES-128
        private static readonly byte[] iv = Encoding.UTF8.GetBytes("6543210987654321");  // 16 byte
        #endregion DICHIARAZIONI_MIO_ARCHIVIO

        #region DICHIARAZIONI_MEMO
        private string fileMemo = "d2.dwgtodo";
        #endregion DICHIARAZIONI_MEMO

        #region FILEPATH
        private string dataFolder;
        private string _todoFilePath;
        private string _todoCategorieFilePath;
        private string _memoFilePath;
        private string _mioarchivioFilePath;
        private string _mioarchivioCategorieFilePath;
        #endregion FILEPATH


        #endregion DICHIARAZIONI

        #region Class

        public frmMain()
        {
            InitializeComponent();

            dataFolder = Path.Combine(Application.StartupPath, "Data");

            // Se la cartella non esiste, creala
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);

            // Percorsi completi dei file JSON
            _todoFilePath = Path.Combine(dataFolder, fileToDoList);
            _todoCategorieFilePath = Path.Combine(dataFolder, FileCategorieToDoList);
            _memoFilePath = Path.Combine(dataFolder, fileMemo);
            _mioarchivioFilePath = Path.Combine(dataFolder, fileMioArchivio);
            _mioarchivioCategorieFilePath = Path.Combine(dataFolder, fileMioArchvioCategorie);

            //TO DO LIST
            InitializeCustom();
            InitializeContextMenu();

            //MIO ARCHIVIO
            InitializeCustom2();
            InitializeContextMenu2();

            //Memo
            LoadMemo();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveMemo();
        }
        #endregion Class

        #region TO_DO_LIST

        #region f()

        #region MENU_TODO-LISt

        private void InitializeContextMenu()
        {
            dgvContextMenu = new ContextMenuStrip();

            var cancellaNota = new ToolStripMenuItem("Cancella nota corrente");
            cancellaNota.Click += CancellaNota_Click;

            dgvContextMenu.Items.Add(cancellaNota);

            // Non assegnare direttamente dgv.ContextMenuStrip!
            dgv.MouseDown += dgv_MouseDown;
        }
     
        #endregion MENU_TODO-LISt

        #region CATEGORIE
        private void LoadCategorie()
        {
            if (File.Exists(_todoCategorieFilePath))
            {
                try
                {
                    // Legge il file cifrato
                    string encryptedJson = File.ReadAllText(_todoCategorieFilePath);

                    // Decritta il contenuto
                    string json = DecryptString(encryptedJson);

                    // Deserializza il JSON in una lista di stringhe
                    categorie = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
                }
                catch
                {
                    categorie = new List<string>();
                }
            }
            else
                categorie = new List<string>();

            // Popola la ComboBox
            cmbCategoria.Items.Clear();
            cmbCategoria.Items.AddRange(categorie.ToArray());

            // Popola la ComboBox
            UpdateComboBoxCategorie();
        }

        private void UpdateComboBoxCategorie()
        {
            // Prendi tutti gli item di cmbCategoria
            List<string> itemsCombo = new List<string>();
            foreach (var obj in cmbCategoria.Items)
                itemsCombo.Add(obj.ToString());

            var col = dgv.Columns["Categoria"] as DataGridViewComboBoxColumn;
            if (col != null)
            {
                // Conserva i valori già presenti nelle righe
                List<string> valoriRighe = new List<string>();
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells["Categoria"].Value != null)
                        valoriRighe.Add(row.Cells["Categoria"].Value.ToString());
                    else
                        valoriRighe.Add(""); // per non perdere l’indice
                }

                // Aggiorna gli item della colonna
                col.Items.Clear();
                col.Items.AddRange(itemsCombo.ToArray());

                // Reinserisci i valori esistenti nelle righe
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    string val = valoriRighe[i];
                    if (!string.IsNullOrEmpty(val) && !col.Items.Contains(val))
                    {
                        col.Items.Add(val);
                    }
                    dgv.Rows[i].Cells["Categoria"].Value = val;
                }
            }

        }
        private void SaveCategorie()
        {
            try
            {
                // Serializza normalmente la lista
                string json = JsonConvert.SerializeObject(categorie, Formatting.Indented);

                // Cripta l'intero JSON (usa la tua EncryptString)
                string encryptedJson = EncryptString(json);

                // Scrive la stringa cifrata nel file
                File.WriteAllText(_todoCategorieFilePath, encryptedJson);

            }
            catch
            {
                // gestire eventuali errori
            }
        }
        #endregion CATEGORIE

        private void InitializeCustom()
        {
            cmbStato.Items.AddRange(new[] { "URGENTE", "MEDIO", "NORMALE" });
            cmbStato.SelectedIndex = 2;
            //cmbCategoria.Items.AddRange(new[] { "Lavoro", "Personale", "Altro" });
            cmbCategoria.SelectedIndex = -1;

            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToAddRows = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = false; // ora la griglia è modificabile

            // 🔹 Definizione colonne
            var colCategoria = new DataGridViewComboBoxColumn()
            {
                Name = "Categoria",
                HeaderText = "Cat.",
                DataPropertyName = "Categoria",
                Width = 120,
                FlatStyle = FlatStyle.Flat
            };
            dgv.Columns.Add(colCategoria);
            //colCategoria.Items.AddRange("Lavoro", "Personale", "Altro"); // esempio di categorie
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Nota",
                HeaderText = "Da fare",
                DataPropertyName = "Nota",
                Width = 250,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "DataInserimento",
                HeaderText = "Data Ins.",
                DataPropertyName = "DataInserimento",
                Width = 120,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "DataScadenza",
                HeaderText = "Data Scad.",
                DataPropertyName = "DataScadenza",
                Width = 120,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgv.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "Fatto",
                HeaderText = "Fatto",
                DataPropertyName = "Fatto",
                Width = 60,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            //Colonna combo per lo stato
            var colStato = new DataGridViewComboBoxColumn()
            {
                Name = "Stato",
                HeaderText = "Stato",
                DataPropertyName = "Stato",
                Width = 100,
                FlatStyle = FlatStyle.Flat,
                DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton,
                SortMode = DataGridViewColumnSortMode.Automatic
            };
            colStato.Items.AddRange("URGENTE", "MEDIO", "NORMALE");
            dgv.Columns.Add(colStato);
            //
            dgv.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ID",
                DataPropertyName = "ID",
                Width = 50,
                ReadOnly = true,
                Visible = false // <-- non visibile nella griglia
            });

            //Rimuove l'evidenziazione della selezione
            dgv.DefaultCellStyle.SelectionBackColor = dgv.DefaultCellStyle.BackColor;
            dgv.DefaultCellStyle.SelectionForeColor = dgv.DefaultCellStyle.ForeColor;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = dgv.AlternatingRowsDefaultCellStyle.BackColor;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = dgv.AlternatingRowsDefaultCellStyle.ForeColor;

            // Colori alternati
            dgv.EnableHeadersVisualStyles = true;
            dgv.RowsDefaultCellStyle.BackColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230);

            // 🔹 Gestione evento validazione riga per aggiornare il JSON
            dgv.RowValidated += dgv_RowValidated;

            LoadCategorie();

            LoadData();
            RefreshGrid();

            items.ListChanged += Items_ListChanged;
        }
        private void Items_ListChanged(object sender, ListChangedEventArgs e)
        {
            // e.ListChangedType: ItemChanged, ItemAdded, ItemDeleted, Reset, ecc.
            if (e.ListChangedType == ListChangedType.ItemChanged ||
                e.ListChangedType == ListChangedType.ItemAdded ||
                e.ListChangedType == ListChangedType.ItemDeleted)
            {
                // salva — puoi aggiungere debounce se vuoi per evitare salvataggi troppo frequenti
                SaveData();
            }
        }

        #region JSON_TODOLIST

        private void SaveData()
        {
            try
            {
                var cloneList = items.Select(x => new ToDoItem
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

                // Cripta tutta la stringa JSON (EncryptString deve restituire stringa base64 o simile)
                string encryptedJson = EncryptString(json);

                File.WriteAllText(_todoFilePath, encryptedJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il salvataggio dei dati:\n" + ex.Message,
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                if (File.Exists(_todoFilePath))
                {
                    string encryptedJson = File.ReadAllText(_todoFilePath);
                    string json = DecryptString(encryptedJson);

                    var list = JsonConvert.DeserializeObject<List<ToDoItem>>(json) ?? new List<ToDoItem>();

                    // se vuoi, decodifica/trasforma campi qui (non necessario se non li hai crittati singolarmente)
                    items = new BindingList<ToDoItem>(list);

                }
                else
                {
                    items = new BindingList<ToDoItem>();
                }

                dgv.DataSource = items;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il caricamento dei dati:\n" + ex.Message,
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                items = new BindingList<ToDoItem>();
                dgv.DataSource = items;
            }
        }

        /*
        private void SaveData()
        {
            try
            {
                var list = items.ToList(); // Conversione per sicurezza
                string json = JsonConvert.SerializeObject(list, Formatting.Indented);
                File.WriteAllText(_todoFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il salvataggio dei dati:\n" + ex.Message,
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        


        private void LoadData()
        {
            try
            {
                if (File.Exists(_todoFilePath))
                {
                    string json = File.ReadAllText(_todoFilePath);
                    var list = JsonConvert.DeserializeObject<List<ToDoItem>>(json);

                    if (list != null)
                        items = new BindingList<ToDoItem>(list);
                    else
                        items = new BindingList<ToDoItem>();
                }
                else
                {
                    items = new BindingList<ToDoItem>();
                }

                dgv.DataSource = items;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il caricamento dei dati:\n" + ex.Message,
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                items = new BindingList<ToDoItem>();
                dgv.DataSource = items;
            }
        }

       */

        #endregion JSON_TODOLIST

        #region REFRESH_DGV
        private void RefreshGrid()
        {
            itemsBinding.DataSource = items;
            dgv.DataSource = itemsBinding;

            AdjustStatoColumnWidth();
        }

        private void AdjustStatoColumnWidth()
        {
            if (items.Count == 0)
                return;

            if (dgv.Columns["Stato"] == null) return;

            int maxWidth = 0;
            using (Graphics g = dgv.CreateGraphics())
            {
                foreach (var item in items)
                {
                    
                    string text = item.Stato ?? "";
                    int w = (int)g.MeasureString(text, dgv.Font).Width;
                    if (w > maxWidth) maxWidth = w;
                }
            }

            int maxWidth2 = 0;
            using (Graphics g = dgv.CreateGraphics())
            {
                foreach (var item in items)
                {

                    string text = item.Categoria ?? "";
                    int w = (int)g.MeasureString(text, dgv.Font).Width;
                    if (w > maxWidth2) maxWidth2 = w;
                }
            }

            /*
            for (int i = 0; i < dgv.Columns.Count; i++)
                dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            */

            // Aggiungi un po’ di padding (ad esempio 20px)
            dgv.Columns["Stato"].Width = maxWidth + 40;

            // Aggiungi un po’ di padding (ad esempio 20px)
            dgv.Columns["Categoria"].Width = maxWidth2 + 40;

            // Nota si espande
            dgv.Columns["Nota"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }
        #endregion REFRESH_DGV

        private void ClearInput()
        {
            txtNotaToDo.Clear();
            //chkFatto.Checked = false;
            chkScadenza.Checked = false;
            dtpScadenza.Enabled = false;
            cmbStato.SelectedIndex = 1;
        }


        #endregion f()

        #region EVENTI

        private void dgv_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            if (dgv.Rows.Count == 0)
                return; // nessuna riga → nessun menu

            var hit = dgv.HitTest(e.X, e.Y);

            // Mostra menu solo se clic su una cella (non header)
            if (hit.Type == DataGridViewHitTestType.Cell && hit.RowIndex >= 0)
            {
                // Seleziona solo la riga cliccata (senza CurrentCell!)
                dgv.ClearSelection();
                dgv.Rows[hit.RowIndex].Selected = true;

                // Mostra menu nella posizione cliccata
                dgvContextMenu.Show(dgv, new Point(e.X, e.Y));
            }
        }


        private void CancellaNota_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
                return;

            var selectedRow = dgv.SelectedRows[0];
            if (selectedRow == null)
                return;

            var boundItem = selectedRow.DataBoundItem as ToDoItem;
            if (boundItem == null)
                return;

            ToDoItem selectedItem = boundItem;

            var confirm = MessageBox.Show(
                $"Vuoi cancellare la nota:\n\n{selectedItem.Nota}?",
                "Conferma cancellazione",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                // 🔹 Rimuove l'oggetto dalla BindingList
                items.Remove(selectedItem);

                // 🔹 Il DataGridView si aggiorna automaticamente
                //    grazie al BindingList

                // 🔹 Salva il file JSON (se non usi il salvataggio automatico)
                SaveData();
            }
        }
        private void chkScadenza_CheckedChanged(object sender, EventArgs e)
        {
            dtpScadenza.Enabled = chkScadenza.Checked;
        }

        private void btnSAlva_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNotaToDo.Text))
            {
                MessageBox.Show("Inserisci una nota prima di salvare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Seleziona una categoria. Se non esiste aggiungila.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            nextId++;

            var item = new ToDoItem
            {
                ID = nextId,
                Nota = txtNotaToDo.Text.Trim(),
                DataInserimento = DateTime.Today,
                DataScadenza = chkScadenza.Checked ? dtpScadenza.Value.Date : (DateTime?)null,
                Fatto = false,
                Stato = cmbStato.SelectedItem.ToString(),
                Categoria = cmbCategoria.SelectedItem?.ToString() ?? null
            };

            items.Add(item);
            SaveData();
            RefreshGrid();
            ClearInput();
        }

   


        private void dgv_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv.Rows.Count)
                return;

            var row = dgv.Rows[e.RowIndex];
            if (row.DataBoundItem is ToDoItem updatedItem)
            {
                var item = items.FirstOrDefault(x => x.ID == updatedItem.ID);
                if (item != null)
                {
                    item.Nota = updatedItem.Nota;
                    item.DataScadenza = updatedItem.DataScadenza;
                    item.Fatto = updatedItem.Fatto;
                    item.Stato = updatedItem.Stato;
                    item.Categoria = updatedItem.Categoria;

                    SaveData();
                }
            }
        }


        private void dgv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (items == null || items.Count == 0)
                return;

            string colName = dgv.Columns[e.ColumnIndex].DataPropertyName;
            if (string.IsNullOrEmpty(colName))
                return;

            // Inverti il verso se clicco sulla stessa colonna
            if (lastSortedColumn == colName)
                sortAscending = !sortAscending;
            else
                sortAscending = true;

            lastSortedColumn = colName;

            // 🔹 Crea una nuova lista ordinata
            List<ToDoItem> sorted;
            switch (colName)
            {
                case "Categoria":
                    sorted = sortAscending
                        ? items.OrderBy(x => x.Categoria).ToList()
                        : items.OrderByDescending(x => x.Categoria).ToList();
                    break;

                case "Nota":
                    sorted = sortAscending
                        ? items.OrderBy(x => x.Nota).ToList()
                        : items.OrderByDescending(x => x.Nota).ToList();
                    break;

                case "DataInserimento":
                    sorted = sortAscending
                        ? items.OrderBy(x => x.DataInserimento).ToList()
                        : items.OrderByDescending(x => x.DataInserimento).ToList();
                    break;

                case "DataScadenza":
                    sorted = sortAscending
                        ? items.OrderBy(x => x.DataScadenza).ToList()
                        : items.OrderByDescending(x => x.DataScadenza).ToList();
                    break;

                case "Fatto":
                    sorted = sortAscending
                        ? items.OrderBy(x => x.Fatto).ToList()
                        : items.OrderByDescending(x => x.Fatto).ToList();
                    break;

                case "Stato":
                    sorted = sortAscending
                        ? items.OrderBy(x => x.Stato).ToList()
                        : items.OrderByDescending(x => x.Stato).ToList();
                    break;

                default:
                    sorted = items.ToList();
                    break;
            }

            // 🔹 Ricrea la BindingList e ricollega la griglia
            items = new BindingList<ToDoItem>(sorted);
            dgv.DataSource = items;

            // 🔹 Rimuovi selezioni e focus
            dgv.ClearSelection();

            // 🔹 Eventuale salvataggio automatico
            SaveData();
        }


        private void label2_Click(object sender, EventArgs e)
        {
            string input = InputBox.Show("Inserisci una nuova categoria:", "Nuova Categoria");
            if (!string.IsNullOrEmpty(input))
            {
                // Controlla se la categoria esiste già
                if (!categorie.Contains(input))
                {
                    // Aggiungi il nuovo item
                    cmbCategoria.Items.Add(input);

                    // Riordina alfabeticamente ignorando maiuscole/minuscole
                    List<string> sorted = cmbCategoria.Items.Cast<string>()
                        .OrderBy(x => x, StringComparer.CurrentCultureIgnoreCase)
                        .ToList();

                    cmbCategoria.Items.Clear();
                    cmbCategoria.Items.AddRange(sorted.ToArray());

                    // Seleziona automaticamente l’item appena inserito
                    cmbCategoria.SelectedItem = sorted.FirstOrDefault(x =>
                        x.Equals(input, StringComparison.CurrentCultureIgnoreCase));

                    // Salva nel JSON aggiornando la lista 'categorie'
                    categorie = sorted;
                    SaveCategorie();

                    // Aggiorna anche la colonna Categoria della griglia
                    UpdateComboBoxCategorie();

                    
                }
                else
                {
                    // Se già esiste, selezionalo semplicemente
                    cmbCategoria.SelectedItem = input;

                    MessageBox.Show("Categoria già presente.",
                                    "Attenzione",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }

            }
        }
        #endregion EVENTI

        #endregion TO_DO_LIST

        #region MIO_ARCHIVIO
        #region f()

        #region MENU_MIO_ARCHIVIO

        private void InitializeContextMenu2()
        {
            dgvContextMenu2 = new ContextMenuStrip();

            var cancellaNota = new ToolStripMenuItem("Cancella nota corrente");
            cancellaNota.Click += CancellaNota2_Click;

            dgvContextMenu2.Items.Add(cancellaNota);

            // Non assegnare direttamente dgv.ContextMenuStrip!
            dgv2.MouseDown += dgv2_MouseDown;
        }

        #endregion MENU_MIO_ARCHIVIO

        #region CATEGORIE_MIO_ARCHIVIO
        private void SaveCategorie2()
        {
            try
            {
                // Serializza normalmente la lista
                string json = JsonConvert.SerializeObject(categorie2, Formatting.Indented);

                // Cripta l'intero JSON (usa la tua EncryptString)
                string encryptedJson = EncryptString(json);

                // Scrive la stringa cifrata nel file
                File.WriteAllText(_mioarchivioCategorieFilePath, encryptedJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il salvataggio delle categorie:\n" + ex.Message,
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCategorie2()
        {
            try
            {
                if (File.Exists(_mioarchivioCategorieFilePath))
                {
                    // Legge il file cifrato
                    string encryptedJson = File.ReadAllText(_mioarchivioCategorieFilePath);

                    // Decritta il contenuto
                    string json = DecryptString(encryptedJson);

                    // Deserializza il JSON in una lista di stringhe
                    categorie2 = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
                }
                else
                {
                    categorie2 = new List<string>();
                }

                // Popola la ComboBox
                cmbCategoria2.Items.Clear();
                cmbCategoria2.Items.AddRange(categorie2.ToArray());

                // Aggiorna eventuali logiche aggiuntive
                UpdateComboBoxCategorie2();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il caricamento delle categorie:\n" + ex.Message,
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);

                categorie2 = new List<string>();
                cmbCategoria2.Items.Clear();
            }
        }



        private void UpdateComboBoxCategorie2()
        {
            // Prendi tutti gli item di cmbCategoria
            List<string> itemsCombo = new List<string>();
            foreach (var obj in cmbCategoria2.Items)
                itemsCombo.Add(obj.ToString());

            var col = dgv2.Columns["Categoria"] as DataGridViewComboBoxColumn;
            if (col != null)
            {
                // Conserva i valori già presenti nelle righe
                List<string> valoriRighe = new List<string>();
                foreach (DataGridViewRow row in dgv2.Rows)
                {
                    if (row.Cells["Categoria"].Value != null)
                        valoriRighe.Add(row.Cells["Categoria"].Value.ToString());
                    else
                        valoriRighe.Add(""); // per non perdere l’indice
                }

                // Aggiorna gli item della colonna
                col.Items.Clear();
                col.Items.AddRange(itemsCombo.ToArray());

                // Reinserisci i valori esistenti nelle righe
                for (int i = 0; i < dgv2.Rows.Count; i++)
                {
                    string val = valoriRighe[i];
                    if (!string.IsNullOrEmpty(val) && !col.Items.Contains(val))
                    {
                        col.Items.Add(val);
                    }
                    dgv2.Rows[i].Cells["Categoria"].Value = val;
                }
            }

        }
        #endregion CATEGORIE_MIO_ARCHIVIO

        private void InitializeCustom2()
        {
            cmbCategoria2.SelectedIndex = -1;

            dgv2.AutoGenerateColumns = false;
            dgv2.AllowUserToAddRows = false;
            dgv2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv2.MultiSelect = false;
            dgv2.ReadOnly = false; // ora la griglia è modificabile

            // 🔹 Definizione colonne
            var colCategoria = new DataGridViewComboBoxColumn()
            {
                Name = "Categoria",
                HeaderText = "Categoria",
                DataPropertyName = "Categoria",
                Width = 120,
                FlatStyle = FlatStyle.Flat
            };
            dgv2.Columns.Add(colCategoria);
            //colCategoria.Items.AddRange("Lavoro", "Personale", "Altro"); // esempio di categorie
            dgv2.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Nota",
                HeaderText = "Informazione",
                DataPropertyName = "Nota",
                Width = 250,
                SortMode = DataGridViewColumnSortMode.Automatic
            });
            dgv2.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Valore",
                HeaderText = "Valore",
                DataPropertyName = "Valore",
                Width = 250,
                SortMode = DataGridViewColumnSortMode.Automatic
            }); dgv2.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ID",
                DataPropertyName = "ID",
                Width = 50,
                ReadOnly = true,
                Visible = false // <-- non visibile nella griglia
            });

            //Rimuove l'evidenziazione della selezione
            dgv2.DefaultCellStyle.SelectionBackColor = dgv2.DefaultCellStyle.BackColor;
            dgv2.DefaultCellStyle.SelectionForeColor = dgv2.DefaultCellStyle.ForeColor;
            dgv2.AlternatingRowsDefaultCellStyle.SelectionBackColor = dgv2.AlternatingRowsDefaultCellStyle.BackColor;
            dgv2.AlternatingRowsDefaultCellStyle.SelectionForeColor = dgv2.AlternatingRowsDefaultCellStyle.ForeColor;

            // Colori alternati
            dgv2.EnableHeadersVisualStyles = true;
            dgv2.RowsDefaultCellStyle.BackColor = Color.White;
            dgv2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 255, 230);

            // 🔹 Gestione evento validazione riga per aggiornare il JSON
            dgv2.RowValidated += dgv2_RowValidated;

            LoadCategorie2();

            LoadData2();
            RefreshGrid2();

            items.ListChanged += Items2_ListChanged;
        }
        private void Items2_ListChanged(object sender, ListChangedEventArgs e)
        {
            // e.ListChangedType: ItemChanged, ItemAdded, ItemDeleted, Reset, ecc.
            if (e.ListChangedType == ListChangedType.ItemChanged ||
                e.ListChangedType == ListChangedType.ItemAdded ||
                e.ListChangedType == ListChangedType.ItemDeleted)
            {
                // salva — puoi aggiungere debounce se vuoi per evitare salvataggi troppo frequenti
                SaveData2();
            }
        }

        #region JSON_MIO_ARCHIVIO
        /*
        private void SaveData2()
        {
            try
            {
                //var list = DBitems.ToList(); // Conversione per sicurezza
                //string json = JsonConvert.SerializeObject(list, Formatting.Indented);

                // Cloniamo gli oggetti per non modificare quelli in memoria
                var cloneList = DBitems.Select(x => new MioArchivio
                {
                    ID = x.ID,
                    Nota = EncryptString(x.Nota),
                    Valore = EncryptString(x.Valore),
                    Categoria = EncryptString(x.Categoria)
                }).ToList();
                string json = JsonConvert.SerializeObject(cloneList, Formatting.Indented);

                File.WriteAllText(_mioarchivioFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il salvataggio dei dati:\n" + ex.Message,
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadData2()
        {
            try
            {
                if (File.Exists(_mioarchivioFilePath))
                {
                    string json = File.ReadAllText(_mioarchivioFilePath);
                    var list = JsonConvert.DeserializeObject<List<MioArchivio>>(json);

                    if (list != null)
                    {
                        //DBitems = new BindingList<MioArchivio>(list);
                        // decrittografa Categoria
                        foreach (var item in list)
                        {
                            item.Valore = DecryptString(item.Valore);
                            item.Nota = DecryptString(item.Nota);
                            item.Categoria = DecryptString(item.Categoria);
                        }
                        DBitems = new BindingList<MioArchivio>(list);
                    }
                    else
                        DBitems = new BindingList<MioArchivio>();
                }
                else
                {
                    DBitems = new BindingList<MioArchivio>();
                }

                dgv2.DataSource = DBitems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il caricamento dei dati:\n" + ex.Message,
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                items = new BindingList<ToDoItem>();
                dgv2.DataSource = DBitems;
            }
        }
        */

        // SaveData2: serializza e poi cripta l'intero JSON
        private void SaveData2()
        {
            try
            {
                // (se vuoi cifrare anche i singoli campi non è necessario se cripti l'intero JSON)
                var cloneList = DBitems.Select(x => new MioArchivio
                {
                    ID = x.ID,
                    Nota = x.Nota,       // mantieni in chiaro qui: verrà cifrato insieme al JSON
                    Valore = x.Valore,
                    Categoria = x.Categoria
                }).ToList();

                string json = JsonConvert.SerializeObject(cloneList, Formatting.Indented);

                // Cripta tutta la stringa JSON (EncryptString deve restituire stringa base64 o simile)
                string encryptedJson = EncryptString(json);

                File.WriteAllText(_mioarchivioFilePath, encryptedJson);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il salvataggio dei dati:\n" + ex.Message,
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // LoadData2: legge, decripta, poi deserializza
        private void LoadData2()
        {
            try
            {
                if (File.Exists(_mioarchivioFilePath))
                {
                    string encryptedJson = File.ReadAllText(_mioarchivioFilePath);
                    string json = DecryptString(encryptedJson);

                    var list = JsonConvert.DeserializeObject<List<MioArchivio>>(json) ?? new List<MioArchivio>();

                    // se vuoi, decodifica/trasforma campi qui (non necessario se non li hai crittati singolarmente)
                    DBitems = new BindingList<MioArchivio>(list);
                }
                else
                {
                    DBitems = new BindingList<MioArchivio>();
                }

                dgv2.DataSource = DBitems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il caricamento dei dati:\n" + ex.Message,
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DBitems = new BindingList<MioArchivio>();
                dgv2.DataSource = DBitems;
            }
        }

  
        #endregion JSON_TODOLIST

        #region REFRESH_DGV
        private void RefreshGrid2()
        {
            DBitemsBinding.DataSource = DBitems;
            dgv2.DataSource = DBitemsBinding;

            AdjustStatoColumnWidth2();
        }

        private void AdjustStatoColumnWidth2()
        {
            if (DBitems.Count == 0)
                return;

            int maxWidth = 0;
            using (Graphics g = dgv2.CreateGraphics())
            {
                foreach (var item in DBitems)
                {

                    string text = item.Valore ?? "";
                    int w = (int)g.MeasureString(text, dgv2.Font).Width;
                    if (w > maxWidth) maxWidth = w;
                }
            }

            int maxWidth2 = 0;
            using (Graphics g = dgv2.CreateGraphics())
            {
                foreach (var item in DBitems)
                {

                    string text = item.Categoria ?? "";
                    int w = (int)g.MeasureString(text, dgv2.Font).Width;
                    if (w > maxWidth2) maxWidth2 = w;
                }
            }

            // Aggiungi un po’ di padding (ad esempio 20px)
            dgv2.Columns["Categoria"].Width = maxWidth2 + 40;

            // Aggiungi un po’ di padding (ad esempio 20px)
            dgv2.Columns["Valore"].Width = maxWidth + 40;


            // Nota si espande
            dgv2.Columns["Nota"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }
        #endregion REFRESH_DGV

        private void ClearInput2()
        {
            txtNota2.Clear();
            txtValore.Clear();
        }

        #region CRYTION
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
        #endregion CRYTION
        #endregion f()

        #region EVENTI
        private void dgv2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            if (dgv2.Rows.Count == 0)
                return; // nessuna riga → nessun menu

            var hit = dgv2.HitTest(e.X, e.Y);

            // Mostra menu solo se clic su una cella (non header)
            if (hit.Type == DataGridViewHitTestType.Cell && hit.RowIndex >= 0)
            {
                // Seleziona solo la riga cliccata (senza CurrentCell!)
                dgv2.ClearSelection();
                dgv2.Rows[hit.RowIndex].Selected = true;

                // Mostra menu nella posizione cliccata
                dgvContextMenu2.Show(dgv2 , new Point(e.X, e.Y));
            }
        }

        private void CancellaNota2_Click(object sender, EventArgs e)
        {
            if (dgv2.SelectedRows.Count == 0)
                return;

            var selectedRow = dgv2.SelectedRows[0];
            if (selectedRow == null)
                return;

            var boundItem = selectedRow.DataBoundItem as MioArchivio;
            if (boundItem == null)
                return;

            MioArchivio selectedItem = boundItem;

            var confirm = MessageBox.Show(
                $"Vuoi cancellare la nota:\n\n{selectedItem.Nota}?",
                "Conferma cancellazione",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                // 🔹 Rimuove l'oggetto dalla BindingList
                DBitems.Remove(selectedItem);

                // 🔹 Il DataGridView si aggiorna automaticamente
                //    grazie al BindingList

                // 🔹 Salva il file JSON (se non usi il salvataggio automatico)
                SaveData2();
            }
        }
        private void dgv2_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv.Rows.Count)
                return;

            var row = dgv2.Rows[e.RowIndex];
            if (row.DataBoundItem is MioArchivio updatedItem)
            {
                var item = DBitems.FirstOrDefault(x => x.ID == updatedItem.ID);
                if (item != null)
                {
                    item.Nota = updatedItem.Nota;
                    item.Valore = updatedItem.Valore;
                    item.Categoria = updatedItem.Categoria;

                    SaveData2();
                }
            }
        }

        private void btnSalvaDB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNota2.Text))
            {
                MessageBox.Show("Inserisci una informazione prima di salvare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtValore.Text))
            {
                MessageBox.Show("Inserisci un valore prima di salvare.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCategoria2.SelectedIndex == -1)
            {
                MessageBox.Show("Seleziona una categoria. Se non esiste aggiungila.", "Attenzione", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            nextId2++;

            var item = new MioArchivio
            {
                ID = nextId2,
                Nota = txtNota2.Text.Trim(),
                Valore = txtValore.Text.Trim(),
                Categoria = cmbCategoria2.SelectedItem?.ToString() ?? null
            };

           DBitems.Add(item);
            SaveData2();
            RefreshGrid2();
            ClearInput2();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            string input = InputBox.Show("Inserisci una nuova categoria:", "Nuova Categoria");
            if (!string.IsNullOrEmpty(input))
            {
                // Controlla se la categoria esiste già
                if (!categorie2.Contains(input))
                {
                    // Aggiungi il nuovo item
                    cmbCategoria2.Items.Add(input);

                    // Riordina alfabeticamente ignorando maiuscole/minuscole
                    List<string> sorted = cmbCategoria2.Items.Cast<string>()
                        .OrderBy(x => x, StringComparer.CurrentCultureIgnoreCase)
                        .ToList();

                    cmbCategoria2.Items.Clear();
                    cmbCategoria2.Items.AddRange(sorted.ToArray());

                    // Seleziona automaticamente l’item appena inserito
                    cmbCategoria2.SelectedItem = sorted.FirstOrDefault(x =>
                        x.Equals(input, StringComparison.CurrentCultureIgnoreCase));

                    // Salva nel JSON aggiornando la lista 'categorie'
                    categorie2 = sorted;
                    SaveCategorie2();

                    // Aggiorna anche la colonna Categoria della griglia
                    UpdateComboBoxCategorie2();


                }
                else
                {
                    // Se già esiste, selezionalo semplicemente
                    cmbCategoria2.SelectedItem = input;

                    MessageBox.Show("Categoria già presente.",
                                    "Attenzione",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }

            }
        }

        private void dgv2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (DBitems == null || DBitems.Count == 0)
                return;

            string colName = dgv2.Columns[e.ColumnIndex].DataPropertyName;
            if (string.IsNullOrEmpty(colName))
                return;

            // Inverti il verso se clicco sulla stessa colonna
            if (lastSortedColumn == colName)
                sortAscending2 = !sortAscending2;
            else
                sortAscending2 = true;

            lastSortedColumn2 = colName;

            // Crea una nuova lista ordinata
            List<MioArchivio> sorted;
            switch (colName)
            {
                case "Categoria":
                    sorted = sortAscending2
                        ? DBitems.OrderBy(x => x.Categoria).ToList()
                        : DBitems.OrderByDescending(x => x.Categoria).ToList();
                    break;

                case "Nota":
                    sorted = sortAscending2
                        ? DBitems.OrderBy(x => x.Nota).ToList()
                        : DBitems.OrderByDescending(x => x.Nota).ToList();
                    break;

                case "Valore":
                    sorted = sortAscending2
                        ? DBitems.OrderBy(x => x.Valore).ToList()
                        : DBitems.OrderByDescending(x => x.Valore).ToList();
                    break;


                default:
                    sorted = DBitems.ToList();
                    break;
            }

            // 🔹 Ricrea la BindingList e ricollega la griglia
            DBitems = new BindingList<MioArchivio >(sorted);
            dgv2.DataSource = DBitems;

            // 🔹 Rimuovi selezioni e focus
            dgv2.ClearSelection();

            // 🔹 Eventuale salvataggio automatico
            SaveData2();

        }



        #endregion EVENTI

        #endregion MIO_ARCHIVIO

        #region MEMO

        #region f()

        /*
        private void LoadMemo()
        {
            try
            {
                if (File.Exists(_memoFilePath))
                {
                    string json = File.ReadAllText(_memoFilePath);
                    var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    if (data != null && data.ContainsKey("Note"))
                        txtMemo.Text = data["Note"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il caricamento del memo:\n" + ex.Message,
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SaveMemo()
        {
            try
            {
                var memoText = txtMemo.Text.Trim();
                string json = JsonConvert.SerializeObject(new { Note = memoText }, Formatting.Indented);
                File.WriteAllText(_memoFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il salvataggio del memo:\n" + ex.Message,
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        */

        private void LoadMemo()
        {
            try
            {
                if (File.Exists(_memoFilePath))
                {
                    string json = File.ReadAllText(_memoFilePath);
                    var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

                    if (data != null && data.ContainsKey("Note"))
                    {
                        // Decifra il valore della nota
                        txtMemo.Text = DecryptString(data["Note"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il caricamento del memo:\n" + ex.Message,
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SaveMemo()
        {
            try
            {
                // Cifra solo il valore della nota
                var data = new Dictionary<string, string>
                {
                    ["Note"] = EncryptString(txtMemo.Text)
                };

                string json = JsonConvert.SerializeObject(data, Formatting.Indented);

                File.WriteAllText(_memoFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore durante il salvataggio del memo:\n" + ex.Message,
                    "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion f()
        private void btnSalvaMemo_Click(object sender, EventArgs e)
        {
            SaveMemo();
        }
        #endregion MEMO

     
    }

    public static class InputBox
    {
        public static string Show(string prompt, string title = "")
        {
            Form form = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label lbl = new Label() { Left = 10, Top = 10, Text = prompt, AutoSize = true };
            TextBox txt = new TextBox() { Left = 10, Top = 40, Width = 360 };
            Button btnOk = new Button() { Text = "OK", Left = 200, Width = 80, Top = 70, DialogResult = DialogResult.OK };
            Button btnCancel = new Button() { Text = "Cancel", Left = 290, Width = 80, Top = 70, DialogResult = DialogResult.Cancel };

            form.Controls.Add(lbl);
            form.Controls.Add(txt);
            form.Controls.Add(btnOk);
            form.Controls.Add(btnCancel);

            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
                if (!string.IsNullOrEmpty(txt.Text.Trim()))
                    return txt.Text.Trim();
                else
                    return null;
            else
                return null;
        }
    }
}

