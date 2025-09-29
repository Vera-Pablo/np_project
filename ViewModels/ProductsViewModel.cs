using np_project.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace np_project.ViewModels
{
    public class ProductsViewModel : ViewModelBase
    {
        private string _title = "Bienvenido a productos";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}
