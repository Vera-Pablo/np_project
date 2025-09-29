using np_project.Helpers;
using np_project.Models;
using np_project.Services;
using np_project.ViewModels.Base;
using np_project.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace np_project.ViewModels
{
    internal class UsersViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set 
            {
                if (SetProperty(ref _selectedUser, value))
                {

                    (DeleteUserCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();
                    (ActiveUserCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();
                    (OpenEditUserCommand as AsyncRelayCommand)?.RaiseCanExecuteChanged();

                }
            }

        }
        public ObservableCollection<User> Users { get; set; } = new();
        public ICommand LoadUsersCommand { get; }
        public ICommand CreateUserCommand { get; }
        public ICommand UpdateUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand ActiveUserCommand { get; }
        public ICommand OpenCreateUserCommand { get; }
        public ICommand OpenEditUserCommand { get; }
        
        public UsersViewModel(IUserService userService)
        {
            _userService = userService;

            LoadUsersCommand = new AsyncRelayCommand(async _ => await LoadUsers());
            CreateUserCommand = new AsyncRelayCommand(async _ => await CreateUser());
            UpdateUserCommand = new AsyncRelayCommand(async _ => await UpdateUser(), _ => SelectedUser != null);
            DeleteUserCommand = new AsyncRelayCommand(async _ => await DeleteUser(), _ => SelectedUser != null);
            ActiveUserCommand = new AsyncRelayCommand(async _ => await ActiveUser(), _ => SelectedUser != null);
            OpenCreateUserCommand = new AsyncRelayCommand(async _ => await OpenCreateUser());
            OpenEditUserCommand = new AsyncRelayCommand(async _ => await OpenEditUser(), _ => SelectedUser != null);

            _ = LoadUsers();
        }

        private async Task LoadUsers()
        {
            Users.Clear();
            var list = await _userService.GetAllAsync();
            foreach(var user in list)
            {
                Users.Add(user);
            }
        }

        private async Task CreateUser()
        {
            var newUser = new User { Dni = 123, Name = "Nuevo", Password = "test", Role = "vendedor"};
            await _userService.CreateAsync(newUser);
            await LoadUsers();
        }

        private async Task UpdateUser()
        {
            if (SelectedUser != null)
            {
                await _userService.UpdateAsync(SelectedUser);
                await LoadUsers();
            }
        }

        private async Task DeleteUser()
        {
            if (SelectedUser != null)
            {
                await _userService.DeleteAsync(SelectedUser.Id);
                await LoadUsers();
            }
        }
        private async Task ActiveUser()
        {
            if (SelectedUser != null)
            {
                await _userService.ActiveAsync(SelectedUser.Id);
                await LoadUsers();
            }
        }

        private async Task OpenCreateUser()
        {
            var newUser = new User();
            var window = new UserFormWindow(newUser);

            if (window.ShowDialog() == true)
            {
                await _userService.CreateAsync(newUser);
                await LoadUsers();
            }
        }

        private async Task OpenEditUser()
        {
            var userCopy = new User
            {
                Id = SelectedUser.Id,
                Dni = SelectedUser.Dni,
                Name = SelectedUser.Name,
                Password = SelectedUser.Password,
                Role = SelectedUser.Role
            };

            var window = new UserFormWindow(userCopy);

            if (window.ShowDialog() == true)
            {
                SelectedUser.Dni = userCopy.Dni;
                SelectedUser.Name = userCopy.Name;
                SelectedUser.Role = userCopy.Role;
                SelectedUser.Password = userCopy.Password;

                await _userService.UpdateAsync(userCopy);
                await LoadUsers();
            }
        }
    }
}
