using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.Security.Principal;
using System.IO;

namespace Calcola_Data_File
{
    public partial class DataFile : Form
    {
        // Variabili Globali
        private string selectedFolderPath = ""; // Variabile per memorizzare il percorso della cartella selezionata
        //private int totalFilesFound = 0;      // Variabile per memorizzare il numero totale di file trovati con filtro
        //private int totalFilesWithoutFilter = 0; // Variabile per memorizzare il numero totale di file senza filtro
        int maxPathLength = 255;                // Lunghezza massima del percorso


        public DataFile()
        {
            InitializeComponent();
        }

        private void Verifica_Click(object sender, EventArgs e)
        {
            {
                if (selectedFolderPath == "")
                {
                    // Mostra avviso se non è stato selezionato alcun percorso
                    MessageBox.Show("Seleziona un percorso valido!");
                }
                else
                {
                    // Percorso della directory da esaminare
                    string directoryPath = selectedFolderPath;

                    // Data di riferimento per il filtro (esempio: 1 gennaio 2023)
                    DateTime dateThreshold = DateTime.Value;

                    // Ottieni tutte le cartelle nella directory selezionata (incluso le sotto-cartelle)
                    var directories = Directory.GetDirectories(selectedFolderPath, "*", System.IO.SearchOption.AllDirectories).ToList();

                    // Ottieni tutti i file nella directory selezionata
                    var allFiles = Directory.GetFiles(selectedFolderPath, "*.*", System.IO.SearchOption.AllDirectories)
                        .Select(f => new FileInfo(f))
                        .ToList();

                    // Conta il numero totale di file e cartelle
                    int totalDirectories = directories.Count;
                    int totalFilesWithoutFilter = allFiles.Count;

                    // Filtra i file più vecchi di dateThreshold
                    var filteredFiles = allFiles.Where(fi => fi.LastWriteTime < dateThreshold).ToList();
                    int totalFilesFound = filteredFiles.Count;

                    // Pulisci il DataGridView prima di aggiungere nuovi elementi
                    dataGridViewFiles.Rows.Clear();
                    dataGridViewFiles.Columns.Clear();

                    // Aggiungi colonne al DataGridView
                    dataGridViewFiles.Columns.Add("FileName", "Nome File");
                    dataGridViewFiles.Columns.Add("LastModified", "Ultima Modifica");
                    dataGridViewFiles.Columns.Add("FileSizeMB", "Dimensione (MB)");  // Colonna per la dimensione del file in MB
                    dataGridViewFiles.Columns.Add("FileFormat", "Formato");          // Colonna per il formato del file
                    dataGridViewFiles.Columns.Add("FilePath", "Percorso Completo");

                    // Inserisci i file filtrati nel DataGridView
                    foreach (var file in filteredFiles)
                    {
                        // Calcola la dimensione del file in MB
                        double fileSizeMB = Math.Round(file.Length / 1024.0 / 1024.0, 2);

                        // Ottieni il formato del file (estensione)
                        string fileFormat = file.Extension.ToLower();

                        // Aggiungi i dati al DataGridView
                        dataGridViewFiles.Rows.Add(file.Name, file.LastWriteTime.ToString(), fileSizeMB, fileFormat, file.FullName);
                    }

                    // Mostra il numero totale di file, cartelle e file filtrati in una TextBox, Label o MessageBox
                    textBoxFileTrovati.Text = ($"Trovate {totalDirectories} cartelle totali e {totalFilesWithoutFilter} file totali.\n" +
                                    $"Filtrati {totalFilesFound} file più vecchi del {dateThreshold.ToShortDateString()}.");
                }
            }
        }

        private void SelectPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                // Imposta il titolo del dialog
                folderDialog.Description = "Seleziona la cartella da esaminare";

                // Mostra il dialog e controlla se l'utente ha selezionato una cartella
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // Salva il percorso della cartella selezionata nella variabile
                    selectedFolderPath = folderDialog.SelectedPath;

                    // Mostra il percorso selezionato in una MessageBox o su un controllo, ad esempio una Label
                    //MessageBox.Show($"Cartella selezionata: {selectedFolderPath}");
                }
            }
            textBoxPath.Text = selectedFolderPath;
        }

        private void btnMoveToRecycleBin_Click(object sender, EventArgs e)
        {
            Verifica_Click(sender, e);

            if (string.IsNullOrEmpty(selectedFolderPath))
            {
                MessageBox.Show("Seleziona un percorso valido!");
                return;
            }
            // Data da utilizzare come riferimento
            DateTime dateThreshold = DateTime.Value; // ad esempio, tutti i file più vecchi del 1 gennaio 2023

            // Ottieni tutti i file nella directory selezionata
            var allFiles = Directory.GetFiles(selectedFolderPath, "*.*", System.IO.SearchOption.AllDirectories)
                .Select(f => new FileInfo(f))
                .ToList();

            // Filtra i file per la data (più vecchi di dateThreshold)
            var filteredFiles = allFiles.Where(fi => fi.LastWriteTime < dateThreshold).ToList();

            // Se non ci sono file da spostare, informa l'utente
            if (filteredFiles.Count == 0)
            {
                MessageBox.Show("Non ci sono file più vecchi della data specificata.");
                return;
            }

            // Messaggio di conferma prima di spostare i file nel Cestino
            var confirmationResult = MessageBox.Show($"Sei sicuro di voler spostare {filteredFiles.Count} file nel Cestino?",
                                                      "Conferma Cancellazione",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

            if (confirmationResult == DialogResult.Yes)
            {
                // Sposta i file filtrati nel Cestino
                foreach (var file in filteredFiles)
                {
                    try
                    {
                        FileSystem.DeleteFile(file.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Errore nel trasferire {file.FullName} nel Cestino: {ex.Message}");
                    }
                }
                MessageBox.Show($"Spostati {filteredFiles.Count} file nel Cestino.");
            }
            else
            {

            }
            // Aggiorna la ListBox e la TextBox dopo lo spostamento
            Verifica_Click(sender, e); // Rieffettua la ricerca per aggiornare l'elenco

        }

        private void esportaCsv_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.Title = "Salva come CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Apri un file StreamWriter per scrivere nel file CSV
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        // Scrivi l'intestazione delle colonne nel file CSV
                        var header = string.Join(",", dataGridViewFiles.Columns.Cast<DataGridViewColumn>().Select(col => col.HeaderText));
                        writer.WriteLine(header);

                        // Scrivi ogni riga del DataGridView nel file CSV
                        foreach (DataGridViewRow row in dataGridViewFiles.Rows)
                        {
                            // Verifica che la riga non sia una riga vuota (come l'ultima riga di inserimento)
                            if (!row.IsNewRow)
                            {
                                var cells = row.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value?.ToString());
                                var line = string.Join(",", cells);
                                writer.WriteLine(line);
                            }
                        }
                    }

                    MessageBox.Show("Esportazione completata!");
                }
            }

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btnElencaPercorsiFile_Click(object sender, EventArgs e)
        {
            if (selectedFolderPath == "")
            {
                // Mostra avviso se non è stato selezionato alcun percorso
                MessageBox.Show("Seleziona un percorso valido!");
            }
            else
            {
                // Percorso di partenza (fai scegliere all'utente tramite una TextBox o FolderBrowserDialog)
                string directoryPath = selectedFolderPath;

                // Pulisci il DataGridView prima di aggiungere nuovi elementi
                dataGridViewFiles.Rows.Clear();
                dataGridViewFiles.Columns.Clear();

                // Aggiungi colonne al DataGridView (una per il percorso completo e una per il numero di caratteri)
                dataGridViewFiles.Columns.Add("FilePath", "Percorso Completo");
                dataGridViewFiles.Columns.Add("CharacterCount", "Numero di Caratteri");

                // Chiamata alla funzione che elenca i percorsi lunghi e li visualizza nel DataGridView
                ElencaPercorsiLunghi(directoryPath, maxPathLength);
            }
        }

        private void ElencaPercorsiLunghi(string directoryPath, int maxPathLength)
        {
            try
            {
                // Ottieni tutti i file e le directory nel percorso specificato
                var allFiles = Directory.GetFiles(directoryPath, "*.*", System.IO.SearchOption.AllDirectories).Select(f => new FileInfo(f));

                var allDirectories = Directory.GetDirectories(directoryPath, "*", System.IO.SearchOption.AllDirectories);

                // Verifica i percorsi dei file
                foreach (var file in allFiles)
                {
                    string filePath = file.FullName;
                    if (filePath.Length > maxPathLength)
                    {
                        // Aggiungi il percorso del file e il numero di caratteri al DataGridView
                        dataGridViewFiles.Rows.Add(filePath, filePath.Length);
                    }
                }

                // Verifica i percorsi delle directory
                foreach (var dir in allDirectories)
                {
                    if (dir.Length > maxPathLength)
                    {
                        // Aggiungi il percorso della directory e il numero di caratteri al DataGridView
                        dataGridViewFiles.Rows.Add(dir, dir.Length);
                    }
                }

            }

            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show($"Accesso negato: {e.Message}");
            }
            catch (PathTooLongException e)
            {
                MessageBox.Show($"Percorso troppo lungo: {e.Message}");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Errore: {e.Message}");
            }
        }

        private void btnElencaPercorsi_Click(object sender, EventArgs e)
        {
            if (selectedFolderPath == "")
            {
                // Mostra avviso se non è stato selezionato alcun percorso
                MessageBox.Show("Seleziona un percorso valido!");
            }
            else
            {
                // Percorso di partenza (puoi modificarlo o farlo scegliere all'utente tramite una TextBox o FolderBrowserDialog)
                string directoryPath = selectedFolderPath;

                // Pulisci il DataGridView prima di aggiungere nuovi elementi
                dataGridViewFiles.Rows.Clear();
                dataGridViewFiles.Columns.Clear();

                // Aggiungi colonne al DataGridView (una per il percorso completo e una per il numero di caratteri)
                dataGridViewFiles.Columns.Add("DirectoryPath", "Percorso Completo della Directory");
                dataGridViewFiles.Columns.Add("CharacterCount", "Numero di Caratteri");

                // Chiamata alla funzione che elenca le directory con percorsi lunghi e li visualizza nel DataGridView
                ElencaPercorsiDirectoryLunghi(directoryPath, maxPathLength);
            }
        }

        private void ElencaPercorsiDirectoryLunghi(string directoryPath, int maxPathLength)
        {
            try
            {
                // Ottieni tutte le directory nel percorso specificato, comprese le sotto-cartelle
                var allDirectories = Directory.GetDirectories(directoryPath, "*", System.IO.SearchOption.AllDirectories);

                // Conta il numero totale di cartelle trovate
                int totalDirectories = allDirectories.Length;

                // Conta le directory con percorso più lungo di maxPathLength
                int directoriesExceedingLength = 0;

                // Verifica i percorsi delle directory
                foreach (var dir in allDirectories)
                {
                    if (dir.Length > maxPathLength)
                    {
                        // Aumenta il contatore per le directory che superano il limite
                        directoriesExceedingLength++;

                        // Aggiungi il percorso della directory e il numero di caratteri al DataGridView
                        dataGridViewFiles.Rows.Add(dir, dir.Length);
                    }
                }

                // Mostra il numero totale di directory trovate in una TextBox o Label
                textBoxPercorsiCartella.Text = $"Trovate {totalDirectories} cartelle totali. Maggiori di {maxPathLength} sono {directoriesExceedingLength}.";

            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show($"Accesso negato: {e.Message}");
            }
            catch (PathTooLongException e)
            {
                MessageBox.Show($"Percorso troppo lungo: {e.Message}");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Errore: {e.Message}");
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            maxPathLength = (int)NumeroCaratteri.Value;
        }

        private void VerificaPermessi_Click(object sender, EventArgs e)
        {

            if (selectedFolderPath == "")
            {
                // Mostra avviso se non è stato selezionato alcun percorso
                MessageBox.Show("Seleziona un percorso valido!");

            }
            else
            {
                // Pulisci il DataGridView prima di aggiungere nuovi elementi
                dataGridViewFiles.Rows.Clear();
                dataGridViewFiles.Columns.Clear();

                // Imposta le colonne del DataGridView
                dataGridViewFiles.Columns.Add("Percorso", "Percorso");
                dataGridViewFiles.Columns.Add("UtenteGruppo", "Utente/Grupo");
                dataGridViewFiles.Columns.Add("TipoAccesso", "Tipo di Accesso");
                dataGridViewFiles.Columns.Add("Permessi", "Permessi");
                dataGridViewFiles.Columns.Add("Ereditato", "Ereditato");

                // Inserisci qui il percorso che vuoi esplorare
                string rootPath = selectedFolderPath;

                if (Directory.Exists(rootPath))
                {
                    // Elenca i permessi e caricali nel DataGridView
                    ListPermissions(rootPath);
                }
                else
                {
                    MessageBox.Show($"Il percorso {rootPath} non esiste.");
             
                }

            }

        }
        // Funzione per elencare i permessi
        private void ListPermissions(string path)
        {
            try
            {
                var directoryInfo = new DirectoryInfo(path);
                var security = directoryInfo.GetAccessControl();

                foreach (FileSystemAccessRule rule in security.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount)))
                {
                    dataGridViewFiles.Rows.Add(
                        path,
                        rule.IdentityReference.Value,
                        rule.AccessControlType,
                        rule.FileSystemRights,
                        rule.IsInherited
                    );
                }

                foreach (var dir in Directory.GetDirectories(path))
                {
                    ListPermissions(dir);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show($"Errore di accesso a {path}: {e.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Si è verificato un errore: {ex.Message}");
            }
        }


    }

}


