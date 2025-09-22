using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace np_project.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private string _welcomeMessage;
        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged(nameof(WelcomeMessage));
            }
        }

        public HomeViewModel()
        {
            WelcomeMessage = "¡Bienvenido al panel de administración!";
        }
    }
}
