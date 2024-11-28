namespace Calcola_Data_File
{
    partial class AD_DC
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
            dataGridView1 = new DataGridView();
            CercaUtenti = new Button();
            textBoxLDAP = new TextBox();
            PSW = new TextBox();
            Utente = new TextBox();
            CSV = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(1111, 745);
            dataGridView1.TabIndex = 0;
            // 
            // CercaUtenti
            // 
            CercaUtenti.Location = new Point(891, 781);
            CercaUtenti.Name = "CercaUtenti";
            CercaUtenti.Size = new Size(113, 23);
            CercaUtenti.TabIndex = 1;
            CercaUtenti.Text = "Cerca Utenti";
            CercaUtenti.UseVisualStyleBackColor = true;
            CercaUtenti.Click += button1_Click;
            // 
            // textBoxLDAP
            // 
            textBoxLDAP.HideSelection = false;
            textBoxLDAP.Location = new Point(428, 783);
            textBoxLDAP.Name = "textBoxLDAP";
            textBoxLDAP.Size = new Size(457, 23);
            textBoxLDAP.TabIndex = 7;
            textBoxLDAP.Text = "LDAP://192.168.178.170";
            // 
            // PSW
            // 
            PSW.HideSelection = false;
            PSW.Location = new Point(220, 783);
            PSW.Name = "PSW";
            PSW.Size = new Size(202, 23);
            PSW.TabIndex = 8;
            PSW.Text = "Tecnodata1!";
            PSW.UseSystemPasswordChar = true;
            PSW.TextChanged += textBox1_TextChanged;
            // 
            // Utente
            // 
            Utente.HideSelection = false;
            Utente.Location = new Point(12, 782);
            Utente.Name = "Utente";
            Utente.Size = new Size(202, 23);
            Utente.TabIndex = 9;
            Utente.Text = "Administrator";
            Utente.TextChanged += textBox2_TextChanged;
            // 
            // CSV
            // 
            CSV.Location = new Point(1010, 781);
            CSV.Name = "CSV";
            CSV.Size = new Size(113, 23);
            CSV.TabIndex = 10;
            CSV.Text = "Esporta CSV";
            CSV.UseVisualStyleBackColor = true;
            CSV.Click += CSV_Click;
            // 
            // AD_DC
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1135, 816);
            Controls.Add(CSV);
            Controls.Add(Utente);
            Controls.Add(PSW);
            Controls.Add(textBoxLDAP);
            Controls.Add(CercaUtenti);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "AD_DC";
            Text = "AD_DC";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button CercaUtenti;
        private TextBox textBoxLDAP;
        private TextBox PSW;
        private TextBox Utente;
        private Button CSV;
    }
}