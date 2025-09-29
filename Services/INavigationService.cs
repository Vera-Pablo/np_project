using np_project.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace np_project.Services
{
    public interface INavigationService
    {
        void OpenWindow<TViewModel>() where TViewModel : class;
        void CloseWindow<TViewModel>() where TViewModel: class;


        void NavigateTo<TVieModel>() where TVieModel : ViewModelBase;
    }
}
