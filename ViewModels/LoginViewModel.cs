using np_project.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using np_project.Data.Context;
using np_project.Helpers;
using System.Windows.Controls;
using System.Windows;
using np_project.Views;

namespace np_project.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private long _dni;
        public long Dni
        {
            get => _dni;
            set
            {
                _dni = value;
                OnPropertyChanged(nameof(Dni));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand LoginCommand { get; }
        private readonly AuthService _authService;

        public LoginViewModel()
        {
            _authService = new AuthService(new ProjectDbContext());
            LoginCommand = new RelayCommand(async (param) => await LoginAsync(param));
        }

        private async Task LoginAsync(object parameter)
        {
            
            
            if (parameter is PasswordBox passwordBox)
            {
                Password = passwordBox?.Password;
            }

            IsBusy = true;

            try
            {
                var user = await _authService.LoginAsync(Dni, Password);
                var mainWindow = new MainWindow();
                mainWindow.Show();

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is Login)
                    {
                        window.Close();
                        break;
                    }
                }
            }
            catch (ArgumentException ex)
            {
                ErrorMessage = ex.Message;
            } finally
            {
                Password = string.Empty;
                IsBusy = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
