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

            if (!File.Exists(scriptPath))
            {
                Logi.Text = "❌ Nie znaleziono pliku clean-system.ps1";
                return;
            }

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

            try
            {
                using (Process process = new Process { StartInfo = psi })
                {
                    process.Start();

                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();

                    process.WaitForExit();

                    if (!string.IsNullOrWhiteSpace(output))
                        Logi.Text = output;
                    else if (!string.IsNullOrWhiteSpace(error))
                        Logi.Text = "⚠️ Błąd:\n" + error;
                    else
                        Logi.Text = "✅ Czyszczenie zakończone.";
                }
            }
            catch (Exception ex)
            {
                Logi.Text = "❌ Wystąpił błąd: " + ex.Message;
            }
        }

    }
}