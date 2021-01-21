using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            if(Text.Length <= 1 || csv?.Count == 0)
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
            if ((bool)!openFileDialog.ShowDialog()) return;

                csv = new List<string>();

            var contents = File.ReadAllText(openFileDialog.FileName).Split('\n');

            if(contents.Length <= 1)
            {
                MessageBox.Show("CSV non valido o vuoto!", "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            foreach(string s in contents)
            {
                var sarray = s.Split(",");

                csv.Add(sarray[1]);

            }
        }
    }
}
