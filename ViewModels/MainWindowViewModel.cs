using np_project.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using np_project.Views;
using System.Windows.Input;

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

        public ICommand NavigateToHomeCommand { get; }
        public ICommand NavigateToUsersCommand { get; }
        public ICommand NavigateToOrdersCommand { get; }

        public MainWindowViewModel()
        {
            NavigateToHomeCommand = new RelayCommand(_ => CurrentViewModel = new HomeViewModel());
            NavigateToUsersCommand = new RelayCommand(_ => CurrentViewModel = new UsersViewModel());
            NavigateToOrdersCommand = new RelayCommand(_ => CurrentViewModel = new OrdersViewModel());

            // Vista inicial
            CurrentViewModel = new HomeViewModel();
        }
    }
}
