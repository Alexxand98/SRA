using System;
using System.Windows;
using SRA.Wpf.Models;
using SRA.Wpf.Services;

namespace SRA.Wpf.Views
{
    public partial class FranjasWindow : Window
    {
        private readonly ApiService _apiService;

        public FranjasWindow(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            CargarFranjas();
        }

        private async void CargarFranjas()
        {
            var franjas = await _apiService.GetFranjasAsync();
            FranjasGrid.ItemsSource = franjas;
        }

        private async void Agregar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(InicioTextBox.Text) || string.IsNullOrWhiteSpace(FinTextBox.Text))
            {
                MessageBox.Show("Introduce hora inicio y fin.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dto = new CreateFranjaHorariaDTO
            {
                HoraInicio = InicioTextBox.Text.Trim(),
                HoraFin = FinTextBox.Text.Trim()
            };

            bool ok = await _apiService.CrearFranjaAsync(dto);
            if (ok)
            {
                CargarFranjas();
                InicioTextBox.Clear();
                FinTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Error al agregar franja.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (FranjasGrid.SelectedItem is FranjaHorariaDTO franja)
            {
                var confirm = MessageBox.Show("¿Eliminar franja seleccionada?", "Confirmar", MessageBoxButton.YesNo);
                if (confirm == MessageBoxResult.Yes)
                {
                    bool ok = await _apiService.EliminarFranjaAsync(franja.Id);
                    if (ok)
                        CargarFranjas();
                    else
                        MessageBox.Show("Error al eliminar franja.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Selecciona una franja para eliminar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
