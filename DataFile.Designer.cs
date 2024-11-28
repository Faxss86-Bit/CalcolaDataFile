namespace Calcola_Data_File
{
    partial class DataFile
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
            DateTime = new DateTimePicker();
            Verifica = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            SelectPath = new Button();
            textBoxPath = new TextBox();
            textBoxFileTrovati = new TextBox();
            btnMoveToRecycleBin = new Button();
            esportaCsv = new Button();
            dataGridViewFiles = new DataGridView();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabControl2 = new TabControl();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            textBoxPercorsiCartella = new TextBox();
            btnElencaPercorsiFile = new Button();
            NumeroCaratteri = new NumericUpDown();
            btnElencaPercorsi = new Button();
            tabPage2 = new TabPage();
            VerificaPermessi = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewFiles).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabControl2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumeroCaratteri).BeginInit();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // DateTime
            // 
            DateTime.Location = new Point(177, 6);
            DateTime.Name = "DateTime";
            DateTime.Size = new Size(200, 23);
            DateTime.TabIndex = 0;
            // 
            // Verifica
            // 
            Verifica.Location = new Point(3, 6);
            Verifica.Name = "Verifica";
            Verifica.Size = new Size(158, 23);
            Verifica.TabIndex = 1;
            Verifica.Text = "Verifica File Più Vecchi Di";
            Verifica.UseVisualStyleBackColor = true;
            Verifica.Click += Verifica_Click;
            // 
            // SelectPath
            // 
            SelectPath.Location = new Point(20, 17);
            SelectPath.Name = "SelectPath";
            SelectPath.Size = new Size(75, 23);
            SelectPath.TabIndex = 4;
            SelectPath.Text = "Select Path";
            SelectPath.UseVisualStyleBackColor = true;
            SelectPath.Click += SelectPath_Click;
            // 
            // textBoxPath
            // 
            textBoxPath.Enabled = false;
            textBoxPath.HideSelection = false;
            textBoxPath.Location = new Point(112, 18);
            textBoxPath.Name = "textBoxPath";
            textBoxPath.Size = new Size(699, 23);
            textBoxPath.TabIndex = 5;
            // 
            // textBoxFileTrovati
            // 
            textBoxFileTrovati.Enabled = false;
            textBoxFileTrovati.HideSelection = false;
            textBoxFileTrovati.Location = new Point(3, 35);
            textBoxFileTrovati.Name = "textBoxFileTrovati";
            textBoxFileTrovati.Size = new Size(607, 23);
            textBoxFileTrovati.TabIndex = 6;
            // 
            // btnMoveToRecycleBin
            // 
            btnMoveToRecycleBin.Location = new Point(478, 71);
            btnMoveToRecycleBin.Name = "btnMoveToRecycleBin";
            btnMoveToRecycleBin.Size = new Size(132, 30);
            btnMoveToRecycleBin.TabIndex = 7;
            btnMoveToRecycleBin.Text = "Canccella Files Vecchi";
            btnMoveToRecycleBin.UseVisualStyleBackColor = true;
            btnMoveToRecycleBin.Click += btnMoveToRecycleBin_Click;
            // 
            // esportaCsv
            // 
            esportaCsv.Location = new Point(653, 661);
            esportaCsv.Name = "esportaCsv";
            esportaCsv.Size = new Size(158, 23);
            esportaCsv.TabIndex = 8;
            esportaCsv.Text = "Esporta csv";
            esportaCsv.UseVisualStyleBackColor = true;
            esportaCsv.Click += esportaCsv_Click;
            // 
            // dataGridViewFiles
            // 
            dataGridViewFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewFiles.Location = new Point(20, 58);
            dataGridViewFiles.Name = "dataGridViewFiles";
            dataGridViewFiles.Size = new Size(791, 488);
            dataGridViewFiles.TabIndex = 9;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(0, 1);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(844, 719);
            tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tabControl2);
            tabPage1.Controls.Add(esportaCsv);
            tabPage1.Controls.Add(dataGridViewFiles);
            tabPage1.Controls.Add(SelectPath);
            tabPage1.Controls.Add(textBoxPath);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(836, 691);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Calcolo Data Files";
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(tabPage3);
            tabControl2.Controls.Add(tabPage4);
            tabControl2.Controls.Add(tabPage2);
            tabControl2.Location = new Point(20, 552);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(625, 136);
            tabControl2.TabIndex = 13;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(Verifica);
            tabPage3.Controls.Add(DateTime);
            tabPage3.Controls.Add(textBoxFileTrovati);
            tabPage3.Controls.Add(btnMoveToRecycleBin);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(617, 108);
            tabPage3.TabIndex = 0;
            tabPage3.Text = "Data File";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(textBoxPercorsiCartella);
            tabPage4.Controls.Add(btnElencaPercorsiFile);
            tabPage4.Controls.Add(NumeroCaratteri);
            tabPage4.Controls.Add(btnElencaPercorsi);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(617, 108);
            tabPage4.TabIndex = 1;
            tabPage4.Text = "Lunghezza Cartelle";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // textBoxPercorsiCartella
            // 
            textBoxPercorsiCartella.Enabled = false;
            textBoxPercorsiCartella.HideSelection = false;
            textBoxPercorsiCartella.Location = new Point(6, 64);
            textBoxPercorsiCartella.Name = "textBoxPercorsiCartella";
            textBoxPercorsiCartella.Size = new Size(607, 23);
            textBoxPercorsiCartella.TabIndex = 8;
            // 
            // btnElencaPercorsiFile
            // 
            btnElencaPercorsiFile.Location = new Point(6, 6);
            btnElencaPercorsiFile.Name = "btnElencaPercorsiFile";
            btnElencaPercorsiFile.Size = new Size(158, 23);
            btnElencaPercorsiFile.TabIndex = 10;
            btnElencaPercorsiFile.Text = "Elenca Percosi Di File > Di";
            btnElencaPercorsiFile.UseVisualStyleBackColor = true;
            btnElencaPercorsiFile.Click += btnElencaPercorsiFile_Click;
            // 
            // NumeroCaratteri
            // 
            NumeroCaratteri.Location = new Point(183, 21);
            NumeroCaratteri.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
            NumeroCaratteri.Name = "NumeroCaratteri";
            NumeroCaratteri.Size = new Size(120, 23);
            NumeroCaratteri.TabIndex = 12;
            NumeroCaratteri.Value = new decimal(new int[] { 254, 0, 0, 0 });
            NumeroCaratteri.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // btnElencaPercorsi
            // 
            btnElencaPercorsi.Location = new Point(6, 35);
            btnElencaPercorsi.Name = "btnElencaPercorsi";
            btnElencaPercorsi.Size = new Size(158, 23);
            btnElencaPercorsi.TabIndex = 11;
            btnElencaPercorsi.Text = "Elenca Percorsi Cartella > Di";
            btnElencaPercorsi.UseVisualStyleBackColor = true;
            btnElencaPercorsi.Click += btnElencaPercorsi_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(VerificaPermessi);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(617, 108);
            tabPage2.TabIndex = 2;
            tabPage2.Text = "Permessi Cartelle";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // VerificaPermessi
            // 
            VerificaPermessi.Location = new Point(15, 17);
            VerificaPermessi.Name = "VerificaPermessi";
            VerificaPermessi.Size = new Size(182, 23);
            VerificaPermessi.TabIndex = 0;
            VerificaPermessi.Text = "Calcola Permessi Cartelle";
            VerificaPermessi.UseVisualStyleBackColor = true;
            VerificaPermessi.Click += VerificaPermessi_Click;
            // 
            // DataFile
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(840, 721);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "DataFile";
            Text = "DataFile";
            ((System.ComponentModel.ISupportInitialize)dataGridViewFiles).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabControl2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumeroCaratteri).EndInit();
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DateTimePicker DateTime;
        private Button Verifica;
        private FolderBrowserDialog folderBrowserDialog1;
        private Button SelectPath;
        private TextBox textBoxPath;
        private TextBox textBoxFileTrovati;
        private Button btnMoveToRecycleBin;
        private Button esportaCsv;
        private DataGridView dataGridViewFiles;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button btnElencaPercorsiFile;
        private Button btnElencaPercorsi;
        private NumericUpDown NumeroCaratteri;
        private TabControl tabControl2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TextBox textBoxPercorsiCartella;
        private TabPage tabPage2;
        private Button VerificaPermessi;
    }
}