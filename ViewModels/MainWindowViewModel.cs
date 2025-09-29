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
        public ICommand ShowProductsCommand { get; }
        public ICommand ShowSuppliersCommand { get; }
        public ICommand ShowSalesCommand { get; }

        private readonly INavigationService _navigation;
        private readonly SessionService _session;

        public bool IsAdmin => _session.CurrentUser.Role == "Administrador";
        public bool IsManager => _session.CurrentUser.Role == "Encargado";
        public MainWindowViewModel(INavigationService navigation, SessionService session)
        {
            _session = session;
            _navigation = navigation;

            CurrentViewModel = new HomeViewModel();

            ShowHomeCommand = new RelayCommand(_ => _navigation.NavigateTo<HomeViewModel>());
            ShowOrdersCommand = new RelayCommand(_ => _navigation.NavigateTo<OrdersViewModel>());
            ShowSalesCommand = new RelayCommand(_ => navigation.NavigateTo<SalesViewModel>());

            if (IsManager)
            {
                ShowSuppliersCommand = new RelayCommand(_ => navigation.NavigateTo<SuppliersViewModel>());
                ShowProductsCommand = new RelayCommand(_ => navigation.NavigateTo<ProductsViewModel>());
            }

            if(IsAdmin)
            {
                ShowUsersCommand = new RelayCommand(_ => _navigation.NavigateTo<UsersViewModel>());
                ShowSuppliersCommand = new RelayCommand(_ => navigation.NavigateTo<SuppliersViewModel>());
                ShowProductsCommand = new RelayCommand(_ => navigation.NavigateTo<ProductsViewModel>());
                ShowSalesCommand = new RelayCommand(_ => navigation.NavigateTo<SalesViewModel>());
            }
        }
    }
}
