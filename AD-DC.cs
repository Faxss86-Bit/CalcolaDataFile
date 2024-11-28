using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;

namespace Calcola_Data_File
{
    public partial class AD_DC : Form
    {
        public AD_DC()
        {
            InitializeComponent();
        }

        // Parametri di connessione LDAP
        private string ldapPath = ""; // Usa "LDAP://" se LDAPS non funziona
        private string adUsername = "";   // Inserisci il nome utente
        private string adPassword = "";   // Inserisci la password
        private string adDomain = "";      // Inserisci il dominio

        private void button1_Click(object sender, EventArgs e)
        {
            // Inizializzare la DataGridView
            InitializeDataGridView();
            // Recuperare e popolare i dati
            PopulateDataGridView();
        }

        // Funzione per inizializzare la DataGridView
        private void InitializeDataGridView()
        {
            // Pulisci il DataGridView prima di aggiungere nuovi elementi
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // Aggiungi le colonne alla DataGridView
            dataGridView1.Columns.Add("Username", "Username");
            dataGridView1.Columns.Add("DisplayName", "Display Name");
            dataGridView1.Columns.Add("Groups", "Groups");
            dataGridView1.Columns.Add("CanChangePassword", "Can Change Password");
            dataGridView1.Columns.Add("LastPasswordChange", "Last Password Change");
            dataGridView1.Columns.Add("IsActive", "Is Active");
        }

        private void PopulateDataGridView()
        {
            ldapPath = textBoxLDAP.Text;
            adUsername = Utente.Text;
            adPassword = PSW.Text;


            try
            {
                using (DirectoryEntry directoryEntry = new DirectoryEntry(ldapPath, adUsername, adPassword))
                {
                    using (DirectorySearcher searcher = new DirectorySearcher(directoryEntry))
                    {
                        searcher.Filter = "(objectClass=user)";
                        searcher.PropertiesToLoad.Add("samAccountName");
                        searcher.PropertiesToLoad.Add("displayName");
                        searcher.PropertiesToLoad.Add("userAccountControl");
                        searcher.PropertiesToLoad.Add("pwdLastSet");
                        searcher.PropertiesToLoad.Add("memberOf");

                        SearchResultCollection results = searcher.FindAll();

                        foreach (SearchResult result in results)
                        {
                            string username = result.Properties.Contains("samAccountName") ? result.Properties["samAccountName"][0].ToString() : "N/A";
                            string displayName = result.Properties.Contains("displayName") ? result.Properties["displayName"][0].ToString() : "N/A";
                            string domainUser = GetDomainUser(username);
                            string groups = GetUserGroups(result); // Ottieni i gruppi
                            bool canChangePassword = CanChangePassword(result);
                            DateTime? lastPasswordChange = GetLastPasswordSet(result);
                            bool isActive = IsAccountActive(result);

                            dataGridView1.Rows.Add(
                                domainUser,
                                displayName,
                                groups,
                                canChangePassword ? "Yes" : "No",
                                lastPasswordChange.HasValue ? lastPasswordChange.Value.ToString() : "N/A",
                                isActive ? "Yes" : "No"
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il popolamento della DataGridView: {ex.Message}");
            }
        }


        // Funzione per ottenere il Domain User
        private string GetDomainUser(string username)
        {
            return $"{username}"; // Formato: DOMAIN\username
                                  // O in alternativa per il formato email: $"{username}@{adDomain}";
        }


        private string GetUserGroups(SearchResult result)
        {
            List<string> groupNames = new List<string>();

            // Controlla i gruppi associati all'utente
            if (result.Properties.Contains("memberOf"))
            {
                foreach (var group in result.Properties["memberOf"])
                {
                    // Parsing del nome del gruppo
                    string groupName = ParseGroupName(group.ToString());
                    groupNames.Add(groupName);
                }
            }

            // Aggiungi "Domain Users" se non è già presente
            if (!groupNames.Contains("Domain Users"))
            {
                groupNames.Add("Domain Users");
            }

            return groupNames.Count > 0 ? string.Join("; ", groupNames) : "N/A";
        }

        // Funzione per estrarre solo il nome del gruppo dal DN
        private string ParseGroupName(string distinguishedName)
        {
            // Suddivide il DN in parti
            var parts = distinguishedName.Split(',');

            if (parts.Length > 0)
            {
                // Ritorna il valore del CN, che è il nome del gruppo
                var match = System.Text.RegularExpressions.Regex.Match(distinguishedName, @"CN=([^,]+)");
                if (match.Success)
                {
                    return match.Groups[1].Value.Trim(); // Rimuove eventuali spazi bianchi
                }
            }
            return distinguishedName; // Ritorna il DN originale se non riesce a parse
        }




        // Funzione per determinare se l'utente può cambiare la password
        private bool CanChangePassword(SearchResult result)
        {
            try
            {
                const int ADS_UF_PASSWD_CANT_CHANGE = 0x0040;
                if (result.Properties.Contains("userAccountControl"))
                {
                    int uac = (int)result.Properties["userAccountControl"][0];
                    return (uac & ADS_UF_PASSWD_CANT_CHANGE) == 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nel determinare se l'utente può cambiare la password: {ex.Message}");
            }
            return false;
        }

        // Funzione per ottenere la data dell'ultimo cambio password
        private DateTime? GetLastPasswordSet(SearchResult result)
        {
            try
            {
                if (result.Properties.Contains("pwdLastSet"))
                {
                    long pwdLastSet = Convert.ToInt64(result.Properties["pwdLastSet"][0]);
                    if (pwdLastSet > 0)
                    {
                        return DateTime.FromFileTime(pwdLastSet);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nel recuperare l'ultima data di cambio password: {ex.Message}");
            }
            return null;
        }

        // Funzione per determinare se l'account è attivo o disattivo
        private bool IsAccountActive(SearchResult result)
        {
            try
            {
                const int ADS_UF_ACCOUNTDISABLE = 0x0002;
                if (result.Properties.Contains("userAccountControl"))
                {
                    int uac = Convert.ToInt32(result.Properties["userAccountControl"][0]);
                    return (uac & ADS_UF_ACCOUNTDISABLE) == 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore nel determinare se l'account è attivo o disattivo: {ex.Message}");
            }
            return false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void CSV_Click(object sender, EventArgs e)
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
                            var header = string.Join(",", dataGridView1.Columns.Cast<DataGridViewColumn>().Select(col => col.HeaderText));
                            writer.WriteLine(header);

                            // Scrivi ogni riga del DataGridView nel file CSV
                            foreach (DataGridViewRow row in dataGridView1.Rows)
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
    }
}