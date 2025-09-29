using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using np_project.Data;
using np_project.Services;
using np_project.ViewModels;
using np_project.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace np_project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Services { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();


            // Conexión a DB
            services.AddDbContext<ProjectDbContext>(options => 
                options.UseSqlServer(@"Server=localhost;Database=db_np;Trusted_Connection=true;TrustServerCertificate=True;"));

            // Servicios
            services.AddScoped<AuthService>();

            services.AddSingleton<INavigationService, NavigationService>();

            // ViewModels ventana
            services.AddTransient<LoginViewModel>();
            services.AddTransient<MainWindowViewModel>();

            // ViewModels internos
            services.AddTransient<HomeViewModel>();
            services.AddTransient<UsersViewModel>();
            services.AddTransient<OrdersViewModel>();


            Services = services.BuildServiceProvider();

            // Lanzar ventada de Login
            var loginWindow = new Views.LoginWindow
            {
                DataContext = Services.GetRequiredService<LoginViewModel>()
            };

            loginWindow.Show();
        }
    }

}
