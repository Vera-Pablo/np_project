using np_project.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using np_project.Views;
using System.Windows.Input;
using np_project.Services;

namespace np_project.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public ICommand ShowHomeCommand { get; }
        public ICommand ShowUsersCommand { get; }
        public ICommand ShowOrdersCommand { get; }

        private readonly INavigationService _navigation;

        public MainWindowViewModel(INavigationService navigation)
        {
            _navigation = navigation;

            CurrentViewModel = new HomeViewModel();

            ShowHomeCommand = new RelayCommand(_ => _navigation.NavigateTo<HomeViewModel>());
            ShowUsersCommand = new RelayCommand(_ => _navigation.NavigateTo<UsersViewModel>());
            ShowOrdersCommand = new RelayCommand(_ => _navigation.NavigateTo<OrdersViewModel>());

        }
    }
}
