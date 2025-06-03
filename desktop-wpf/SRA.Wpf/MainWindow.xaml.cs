using System.Windows;
using SRA.Wpf.Services;
using SRA.Wpf.Views;

namespace SRA.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly ApiService _apiService;

        public MainWindow(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private void AbrirReservas_Click(object sender, RoutedEventArgs e)
        {
            new ReservasPendientesWindow(_apiService).ShowDialog();
        }

        private void AbrirFranjas_Click(object sender, RoutedEventArgs e)
        {
            new FranjasWindow(_apiService).ShowDialog();
        }

        private void AbrirDiasNoLectivos_Click(object sender, RoutedEventArgs e)
        {
            new DiasNoLectivosWindow(_apiService).ShowDialog();
        }

        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
