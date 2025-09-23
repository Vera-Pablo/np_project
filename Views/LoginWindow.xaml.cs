using System;
using System.Collections.Generic;
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
using np_project.ViewModels;

namespace np_project.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            var vm = new LoginViewModel(OnLoginSuccess);
            DataContext = vm;
        }

        private void OnLoginSuccess()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var main = new MainWindow();
                main.Show();
                this.Close();
            });
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm && sender is PasswordBox pb)
            {
                vm.Password = pb.Password;
            }
        }
    }
}
