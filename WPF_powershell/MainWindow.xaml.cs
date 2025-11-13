using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System;
using System.Diagnostics;
using System.IO;
using System.Data.Odbc;


namespace WPF_powershell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Czysc(object sender, RoutedEventArgs e)
        {
            // Ścieżka do skryptu PowerShell
            string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "czysc.ps1");
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logi.txt");
            // Przygotowanie procesu PowerShell
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-ExecutionPolicy Bypass -NoProfile -File \"{scriptPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };


            using (Process process = new Process { StartInfo = psi })
            {
                process.Start();
                // Nie zatrzymuj działania aplikacji - czekaj asynchronicznie
                Logi.Text += "Czyszczenie rozpoczęte...\n";
                await process.WaitForExitAsync();
                if (File.Exists(logPath))
                {
                    Logi.Text += File.ReadAllText(logPath);
                }
            }
        }

        private async void skanuj(object sender, RoutedEventArgs e)
        {

            if (OD.Text == "" || DO.Text == "")
            {
                Logi.Text += "Najpierw podaj zakres\n";
                return;
            }
            if (!int.TryParse(OD.Text, out _) || !int.TryParse(DO.Text, out _))
            {
                Logi.Text += "Błąd danych - wprowadź tylko cyfry\n";
                return;
            }
            if(SciezkaSkanu.Text == "")
            {
                Logi.Text += "Podaj ścieżkę skanowania\n";
                return;
            }
            // Sprawdzenie czy ścieżka istnieje
            if (!Directory.Exists(SciezkaSkanu.Text))
            {
                Logi.Text += "Podana ścieżka nie istnieje\n";
                return;
            }

            // Ścieżka do skryptu PowerShell
            string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "skanuj.ps1");
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logi_scan.txt");
            // Przygotowanie procesu PowerShell

            List<string> dane = new List<string>();
            dane.Add(OD.Text);
            dane.Add(DO.Text);
            dane.Add(SciezkaSkanu.Text);

            string dane2 = string.Join(",", dane);



            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-ExecutionPolicy Bypass -File \"{scriptPath}\" -apps \"{dane2}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,

            };

            using (Process process = new Process { StartInfo = psi })
            {
                process.Start();
                // Nie zatrzymuj działania aplikacji - czekaj asynchronicznie
                Logi.Text += "Skanowanie rozpoczęte...\n";
                await process.WaitForExitAsync();
                if (File.Exists(logPath))
                {
                    Logi.Text += "Pliki spełniające kryteria: \n";
                    Logi.Text += File.ReadAllText(logPath);
                }
            }



        }

        private async void pobierz(object sender, RoutedEventArgs e)
        {
            string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pobierz.ps1");
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logi_pobierz.txt");

            List<string> aplikacje = new List<string>();

            foreach (var app in ListaApek.SelectedItems)
            {
                ListBoxItem item = app as ListBoxItem;
                if (item != null)
                {
                    string nazwa = item.Content.ToString().ToLower().Trim();
                    aplikacje.Add(nazwa);
                }
            }

            if (aplikacje.Count == 0)
            {
                Logi.Text += "Nie wybrano aplikacji.\n";
                return;
            }

            // Zamieniamy listę na string rozdzielony przecinkami
            string appsParam = string.Join(",", aplikacje);

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-ExecutionPolicy Bypass -NoProfile -File \"{scriptPath}\" -apps \"{appsParam}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = psi })
            {
                process.Start();
                Logi.Text += "Pobieranie rozpoczęte...\n";
                await process.WaitForExitAsync();
                if (File.Exists(logPath))
                {
                    Logi.Text += File.ReadAllText(logPath);
                }
            }
        }

    }
}
