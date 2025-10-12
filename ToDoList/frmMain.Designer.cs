namespace ToDoList
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.TC = new System.Windows.Forms.TabControl();
            this.tpToDoList = new System.Windows.Forms.TabPage();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkScadenza = new System.Windows.Forms.CheckBox();
            this.btnSAlva = new System.Windows.Forms.Button();
            this.dtpScadenza = new System.Windows.Forms.DateTimePicker();
            this.cmbStato = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNotaToDo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tpMemo = new System.Windows.Forms.TabPage();
            this.txtMemo = new System.Windows.Forms.TextBox();
            this.btnSalvaMemo = new DevExpress.XtraEditors.SimpleButton();
            this.tpArchivio = new System.Windows.Forms.TabPage();
            this.dgv2 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtValore = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCategoria2 = new System.Windows.Forms.ComboBox();
            this.btnSalvaDB = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNota2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TC.SuspendLayout();
            this.tpToDoList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel1.SuspendLayout();
            this.tpMemo.SuspendLayout();
            this.tpArchivio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TC
            // 
            this.TC.Controls.Add(this.tpToDoList);
            this.TC.Controls.Add(this.tpMemo);
            this.TC.Controls.Add(this.tpArchivio);
            this.TC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TC.Location = new System.Drawing.Point(0, 0);
            this.TC.Margin = new System.Windows.Forms.Padding(4);
            this.TC.Name = "TC";
            this.TC.SelectedIndex = 0;
            this.TC.Size = new System.Drawing.Size(916, 513);
            this.TC.TabIndex = 0;
            // 
            // tpToDoList
            // 
            this.tpToDoList.Controls.Add(this.dgv);
            this.tpToDoList.Controls.Add(this.panel1);
            this.tpToDoList.Location = new System.Drawing.Point(4, 27);
            this.tpToDoList.Margin = new System.Windows.Forms.Padding(4);
            this.tpToDoList.Name = "tpToDoList";
            this.tpToDoList.Padding = new System.Windows.Forms.Padding(4);
            this.tpToDoList.Size = new System.Drawing.Size(908, 482);
            this.tpToDoList.TabIndex = 0;
            this.tpToDoList.Text = "To do list               ";
            this.tpToDoList.UseVisualStyleBackColor = true;
            // 
            // dgv
            // 
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(4, 85);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(906, 390);
            this.dgv.TabIndex = 1;
            this.dgv.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_ColumnHeaderMouseClick);
            this.dgv.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_RowValidated);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightYellow;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cmbCategoria);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.chkScadenza);
            this.panel1.Controls.Add(this.btnSAlva);
            this.panel1.Controls.Add(this.dtpScadenza);
            this.panel1.Controls.Add(this.cmbStato);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtNotaToDo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 76);
            this.panel1.TabIndex = 0;
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Location = new System.Drawing.Point(85, 38);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(240, 26);
            this.cmbCategoria.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(331, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "Stato";
            // 
            // chkScadenza
            // 
            this.chkScadenza.AutoSize = true;
            this.chkScadenza.Location = new System.Drawing.Point(621, 41);
            this.chkScadenza.Name = "chkScadenza";
            this.chkScadenza.Size = new System.Drawing.Size(86, 22);
            this.chkScadenza.TabIndex = 7;
            this.chkScadenza.Text = "Data fine";
            this.chkScadenza.UseVisualStyleBackColor = true;
            // 
            // btnSAlva
            // 
            this.btnSAlva.Location = new System.Drawing.Point(824, 38);
            this.btnSAlva.Name = "btnSAlva";
            this.btnSAlva.Size = new System.Drawing.Size(60, 27);
            this.btnSAlva.TabIndex = 6;
            this.btnSAlva.Text = "Salva";
            this.btnSAlva.UseVisualStyleBackColor = true;
            this.btnSAlva.Click += new System.EventHandler(this.btnSAlva_Click);
            // 
            // dtpScadenza
            // 
            this.dtpScadenza.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpScadenza.Location = new System.Drawing.Point(708, 39);
            this.dtpScadenza.Name = "dtpScadenza";
            this.dtpScadenza.Size = new System.Drawing.Size(112, 26);
            this.dtpScadenza.TabIndex = 5;
            this.dtpScadenza.Value = new System.DateTime(2025, 10, 11, 0, 0, 0, 0);
            // 
            // cmbStato
            // 
            this.cmbStato.FormattingEnabled = true;
            this.cmbStato.Location = new System.Drawing.Point(376, 38);
            this.cmbStato.Name = "cmbStato";
            this.cmbStato.Size = new System.Drawing.Size(240, 26);
            this.cmbStato.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Categoria";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtNotaToDo
            // 
            this.txtNotaToDo.Location = new System.Drawing.Point(85, 8);
            this.txtNotaToDo.Name = "txtNotaToDo";
            this.txtNotaToDo.Size = new System.Drawing.Size(800, 26);
            this.txtNotaToDo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Da fare";
            // 
            // tpMemo
            // 
            this.tpMemo.Controls.Add(this.txtMemo);
            this.tpMemo.Controls.Add(this.btnSalvaMemo);
            this.tpMemo.Location = new System.Drawing.Point(4, 27);
            this.tpMemo.Margin = new System.Windows.Forms.Padding(4);
            this.tpMemo.Name = "tpMemo";
            this.tpMemo.Padding = new System.Windows.Forms.Padding(4);
            this.tpMemo.Size = new System.Drawing.Size(908, 482);
            this.tpMemo.TabIndex = 1;
            this.tpMemo.Text = "Memo               ";
            this.tpMemo.UseVisualStyleBackColor = true;
            // 
            // txtMemo
            // 
            this.txtMemo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMemo.Location = new System.Drawing.Point(3, 37);
            this.txtMemo.Multiline = true;
            this.txtMemo.Name = "txtMemo";
            this.txtMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMemo.Size = new System.Drawing.Size(905, 449);
            this.txtMemo.TabIndex = 1;
            // 
            // btnSalvaMemo
            // 
            this.btnSalvaMemo.Location = new System.Drawing.Point(8, 8);
            this.btnSalvaMemo.Name = "btnSalvaMemo";
            this.btnSalvaMemo.Size = new System.Drawing.Size(75, 23);
            this.btnSalvaMemo.TabIndex = 0;
            this.btnSalvaMemo.Text = "Salva Memo";
            this.btnSalvaMemo.Click += new System.EventHandler(this.btnSalvaMemo_Click);
            // 
            // tpArchivio
            // 
            this.tpArchivio.Controls.Add(this.dgv2);
            this.tpArchivio.Controls.Add(this.panel2);
            this.tpArchivio.Location = new System.Drawing.Point(4, 27);
            this.tpArchivio.Margin = new System.Windows.Forms.Padding(4);
            this.tpArchivio.Name = "tpArchivio";
            this.tpArchivio.Padding = new System.Windows.Forms.Padding(4);
            this.tpArchivio.Size = new System.Drawing.Size(908, 482);
            this.tpArchivio.TabIndex = 2;
            this.tpArchivio.Text = "Mio archivio               ";
            this.tpArchivio.UseVisualStyleBackColor = true;
            // 
            // dgv2
            // 
            this.dgv2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv2.Location = new System.Drawing.Point(0, 86);
            this.dgv2.Name = "dgv2";
            this.dgv2.Size = new System.Drawing.Size(906, 390);
            this.dgv2.TabIndex = 2;
            this.dgv2.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv2_ColumnHeaderMouseClick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtValore);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cmbCategoria2);
            this.panel2.Controls.Add(this.btnSalvaDB);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtNota2);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(900, 76);
            this.panel2.TabIndex = 1;
            // 
            // txtValore
            // 
            this.txtValore.Location = new System.Drawing.Point(415, 39);
            this.txtValore.Name = "txtValore";
            this.txtValore.Size = new System.Drawing.Size(403, 26);
            this.txtValore.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(361, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "Valore";
            // 
            // cmbCategoria2
            // 
            this.cmbCategoria2.FormattingEnabled = true;
            this.cmbCategoria2.Location = new System.Drawing.Point(103, 40);
            this.cmbCategoria2.Name = "cmbCategoria2";
            this.cmbCategoria2.Size = new System.Drawing.Size(240, 26);
            this.cmbCategoria2.TabIndex = 9;
            // 
            // btnSalvaDB
            // 
            this.btnSalvaDB.Location = new System.Drawing.Point(824, 38);
            this.btnSalvaDB.Name = "btnSalvaDB";
            this.btnSalvaDB.Size = new System.Drawing.Size(60, 27);
            this.btnSalvaDB.TabIndex = 6;
            this.btnSalvaDB.Text = "Salva";
            this.btnSalvaDB.UseVisualStyleBackColor = true;
            this.btnSalvaDB.Click += new System.EventHandler(this.btnSalvaDB_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "Categoria";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // txtNota2
            // 
            this.txtNota2.Location = new System.Drawing.Point(103, 8);
            this.txtNota2.Name = "txtNota2";
            this.txtNota2.Size = new System.Drawing.Size(782, 26);
            this.txtNota2.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 18);
            this.label6.TabIndex = 0;
            this.label6.Text = "Informazione";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 513);
            this.Controls.Add(this.TC);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMain";
            this.Text = "To do list ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.TC.ResumeLayout(false);
            this.tpToDoList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tpMemo.ResumeLayout(false);
            this.tpMemo.PerformLayout();
            this.tpArchivio.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TC;
        private System.Windows.Forms.TabPage tpToDoList;
        private System.Windows.Forms.TabPage tpMemo;
        private System.Windows.Forms.TabPage tpArchivio;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpScadenza;
        private System.Windows.Forms.ComboBox cmbStato;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNotaToDo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSAlva;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.CheckBox chkScadenza;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgv2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtValore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCategoria2;
        private System.Windows.Forms.Button btnSalvaDB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNota2;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.SimpleButton btnSalvaMemo;
        private System.Windows.Forms.TextBox txtMemo;
    }
}