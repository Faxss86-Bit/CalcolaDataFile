namespace Calcola_Data_File
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Crea un'istanza del secondo form
            DataFile DataFile = new DataFile();

            // Mostra il secondo form
            DataFile.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Crea un'istanza del secondo form
            AD_DC AD_DC = new AD_DC();

            // Mostra il secondo form
            AD_DC.Show();
        }
    }
}
