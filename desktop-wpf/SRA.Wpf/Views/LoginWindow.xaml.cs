using System.Windows;
using SRA.Wpf.Services;

namespace SRA.Wpf.Views
{
    public partial class LoginWindow : Window
    {
        private readonly ApiService _apiService = new();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Introduce correo y contraseña.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool loginSuccess = await _apiService.LoginAsync(email, password);
            if (loginSuccess)
            {
                var mainWindow = new MainWindow(_apiService);
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
