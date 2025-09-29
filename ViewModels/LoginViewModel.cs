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
using np_project.Services;
using np_project.Helpers;

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

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoginCommand { get; }

        private readonly AuthService _authService;
        private readonly SessionService _sessionService;
        private readonly INavigationService _navigationService;

        public LoginViewModel(AuthService authService, INavigationService navigationService, SessionService sessionService)
        {
            _sessionService = sessionService;
            _authService = authService;
            _navigationService = navigationService;
            LoginCommand = new AsyncRelayCommand(async _ => await DoLoginAsync());
        }

        private async Task DoLoginAsync()
        {
            try
            {
                var user = await _authService.LoginAsync(long.Parse(Dni), Password);
                _sessionService.CurrentUser = user;
                _navigationService.OpenWindow<MainWindowViewModel>();
                _navigationService.CloseWindow<LoginViewModel>();
            } catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
