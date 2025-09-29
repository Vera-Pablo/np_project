using np_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace np_project.Views
{
    public partial class UserFormWindow : Window
    {
        public User User { get; private set; }
        public UserFormWindow(User user)
        {
            InitializeComponent();
            User = user;
            DataContext = User;
        }

        public void Submit_Click(object sender, RoutedEventArgs e)
        {
            User.Password = PasswordBox.Password;
            DialogResult = true;
            Close();
        }
    }
}
