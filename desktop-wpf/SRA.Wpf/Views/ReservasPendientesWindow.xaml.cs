using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SRA.Wpf.Models;
using SRA.Wpf.Services;

namespace SRA.Wpf.Views
{
    public partial class ReservasPendientesWindow : Window
    {
        private readonly ApiService _apiService;
        private List<ReservaDTO> _reservas;

        public ReservasPendientesWindow(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            CargarReservasPendientes();
        }

        private async void CargarReservasPendientes()
        {
            _reservas = await _apiService.GetReservasPendientesAsync();
            ReservasGrid.ItemsSource = _reservas;
        }

        private async void Aprobar_Click(object sender, RoutedEventArgs e)
        {
            if (ReservasGrid.SelectedItem is ReservaDTO reserva)
            {
                bool ok = await _apiService.CambiarEstadoReservaAsync(reserva.Id, "Aprobada");
                if (ok) CargarReservasPendientes();
                else MessageBox.Show("Error al aprobar la reserva.");
            }
        }

        private async void Rechazar_Click(object sender, RoutedEventArgs e)
        {
            if (ReservasGrid.SelectedItem is ReservaDTO reserva)
            {
                bool ok = await _apiService.CambiarEstadoReservaAsync(reserva.Id, "Rechazada");
                if (ok) CargarReservasPendientes();
                else MessageBox.Show("Error al rechazar la reserva.");
            }
        }
    }
}
