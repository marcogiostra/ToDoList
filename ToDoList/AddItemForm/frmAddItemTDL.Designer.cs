namespace RicettarioMG.Form.FormAddItem
{
    partial class frmAddItemTDL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddItemTDL));
            this.btnAnnulla = new DevExpress.XtraEditors.SimpleButton();
            this.btnSalvaChiudi = new DevExpress.XtraEditors.SimpleButton();
            this.txtNew = new DevExpress.XtraEditors.TextEdit();
            this.Label1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.txtNew.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAnnulla
            // 
            this.btnAnnulla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnnulla.Location = new System.Drawing.Point(240, 45);
            this.btnAnnulla.Name = "btnAnnulla";
            this.btnAnnulla.Size = new System.Drawing.Size(82, 23);
            this.btnAnnulla.TabIndex = 6;
            this.btnAnnulla.Text = "Annulla";
            this.btnAnnulla.Click += new System.EventHandler(this.btnAnnulla_Click);
            // 
            // btnSalvaChiudi
            // 
            this.btnSalvaChiudi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalvaChiudi.Location = new System.Drawing.Point(329, 45);
            this.btnSalvaChiudi.Name = "btnSalvaChiudi";
            this.btnSalvaChiudi.Size = new System.Drawing.Size(82, 23);
            this.btnSalvaChiudi.TabIndex = 7;
            this.btnSalvaChiudi.Text = "Salva e chiudi";
            this.btnSalvaChiudi.Click += new System.EventHandler(this.btnSalvaChiudi_Click);
            // 
            // txtNew
            // 
            this.txtNew.Location = new System.Drawing.Point(115, 15);
            this.txtNew.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNew.Name = "txtNew";
            this.txtNew.Size = new System.Drawing.Size(297, 20);
            this.txtNew.TabIndex = 5;
            this.txtNew.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNew_KeyDown);
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(20, 17);
            this.Label1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(56, 13);
            this.Label1.TabIndex = 4;
            this.Label1.Text = "Nuova cosa";
            // 
            // frmAddItemTDL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 81);
            this.Controls.Add(this.btnAnnulla);
            this.Controls.Add(this.btnSalvaChiudi);
            this.Controls.Add(this.txtNew);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("frmAddItemTDL.IconOptions.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddItemTDL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Aggiungi una nuova cosa da fare";
            this.Load += new System.EventHandler(this.frmAddItemCategoria_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtNew.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnAnnulla;
        private DevExpress.XtraEditors.SimpleButton btnSalvaChiudi;
        private DevExpress.XtraEditors.TextEdit txtNew;
        private DevExpress.XtraEditors.LabelControl Label1;
    }
}