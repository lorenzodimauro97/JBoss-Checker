using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace JBoss_Checker
{
    public partial class MainWindow
    {
        List<string> csv;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OutputBox.Text = string.Empty;
            //MessageBox.Show(InputText.Text);
            string[] Text = InputText.Text.Split("\r\n");

            if (Text.Length <= 1 || csv?.Count == 0)
            {
                MessageBox.Show("CSV Non caricato o lista server non valida!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (string s in Text)
            {
                if (s == string.Empty || !s.Substring(0, 1).Contains("p")) continue;
                if (!csv.Any(x => x.Contains(s))) OutputBox.Text += $"{s}\n";
            }
        }

        private void CSVImportButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files |*.csv";
            if ((bool)!openFileDialog.ShowDialog()) return;

            csv = new List<string>();

            var contents = System.Array.Empty<string>();

            try
            {
                contents = File.ReadAllText(openFileDialog.FileName).Split('\n');
            }

            catch (IOException)
            {
                MessageBox.Show("Eccezione I/O! Impossibile aprire il CSV, verificare che non sia aperto in un altro programma!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (contents.Length <= 1)
            {
                MessageBox.Show("CSV non valido o vuoto!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (string s in contents)
            {
                var sarray = s.Split(",");

                if (sarray.Length < 2) continue;

                csv.Add(sarray[1]);
            }

            ServerLabel.Content = $"Server caricati in memoria: {contents.Length}";
        }
    }
}
