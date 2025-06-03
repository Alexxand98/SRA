using System;
using System.Windows;
using SRA.Wpf.Models;
using SRA.Wpf.Services;

namespace SRA.Wpf.Views
{
    public partial class DiasNoLectivosWindow : Window
    {
        private readonly ApiService _apiService;

        public DiasNoLectivosWindow(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            CargarDias();
        }

        private async void CargarDias()
        {
            var dias = await _apiService.GetDiasNoLectivosAsync();
            DiasGrid.ItemsSource = dias;
        }

        private async void Agregar_Click(object sender, RoutedEventArgs e)
        {
            if (FechaPicker.SelectedDate == null || string.IsNullOrWhiteSpace(MotivoTextBox.Text))
            {
                MessageBox.Show("Selecciona fecha y escribe motivo.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dto = new CreateDiaNoLectivoDTO
            {
                Fecha = FechaPicker.SelectedDate.Value,
                Motivo = MotivoTextBox.Text.Trim()
            };

            bool ok = await _apiService.CrearDiaNoLectivoAsync(dto);
            if (ok)
            {
                CargarDias();
                FechaPicker.SelectedDate = null;
                MotivoTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Error al agregar día no lectivo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (DiasGrid.SelectedItem is DiaNoLectivoDTO dia)
            {
                var confirm = MessageBox.Show("¿Eliminar día no lectivo?", "Confirmar", MessageBoxButton.YesNo);
                if (confirm == MessageBoxResult.Yes)
                {
                    bool ok = await _apiService.EliminarDiaNoLectivoAsync(dia.Id);
                    if (ok)
                        CargarDias();
                    else
                        MessageBox.Show("Error al eliminar día no lectivo.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Selecciona un día para eliminar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
