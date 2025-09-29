using Microsoft.Extensions.DependencyInjection;
using np_project.ViewModels;
using np_project.ViewModels.Base;
using System;
using System.Linq;
using System.Windows;

namespace np_project.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void OpenWindow<TViewModel>() where TViewModel : class
        {
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();

            // Nombre esperado de la View (ej: MainWindowViewModel -> MainWindow)
            var viewTypeName = typeof(TViewModel).Name.Replace("ViewModel", "");

            // Buscar dentro del assembly actual un tipo que coincida con el nombre
            var assembly = typeof(TViewModel).Assembly;
            var viewType = assembly.GetTypes()
                .FirstOrDefault(t => t.Name == viewTypeName && typeof(Window).IsAssignableFrom(t));

            if (viewType == null)
                throw new InvalidOperationException(
                    $"No se encontró una ventana llamada {viewTypeName} asociada a {typeof(TViewModel).Name}");

            var window = (Window)Activator.CreateInstance(viewType);
            window.DataContext = viewModel;
            window.Show();
        }

        public void CloseWindow<TViewModel>() where TViewModel : class
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.DataContext is TViewModel)
                {
                    window.Close();
                    break;
                }
            }
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            var vm = _serviceProvider.GetRequiredService<TViewModel>();

            var mainWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext is MainWindowViewModel);

            if (mainWindow?.DataContext is MainWindowViewModel mainVM)
            {
                mainVM.CurrentViewModel = vm as ViewModelBase;
            } else
            {
                throw new InvalidOperationException("No se encontró un MainWindow abierto con MainWindowViewModel");
            }
        }
    }
}
