using np_project.ViewModels.Base;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using np_project.Views;

namespace np_project.ViewModels
{
    internal class LoginViewModel : ViewModelBase
    {
        private string _dni;
        public string Dni
        {
            get => _dni;
            set => SetProperty(ref _dni, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand { get; }

        private readonly Action _onLoginSuccess;

        public LoginViewModel(Action onLoginSuccess = null)
        {
            _onLoginSuccess = onLoginSuccess;
            LoginCommand = new RelayCommand(_ => DoLogin());
        }

        private void DoLogin()
        {
            Debug.WriteLine($"Intento de Login con DNI: {Dni}, Password: {Password}");

            if (Dni == "123" && Password == "123")
            {
                _onLoginSuccess?.Invoke();
            } else
            {
                MessageBox.Show("Credenciales Incorrectas");
            }
        }
    }
}
