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

namespace BookingSampleApplication
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public delegate void NotifyLoginClick(string value);
        public event NotifyLoginClick LoginClick;

        LoginControlViewModel loginControlViewModel;
 
        public LoginControl()
        {
            InitializeComponent();
            loginControlViewModel = new LoginControlViewModel();
            this.DataContext = loginControlViewModel;
            loginControlViewModel.LoginClick += LoginControlViewModel_LoginClick;       
        }

        private void LoginControlViewModel_LoginClick(string value)
        {
            this.DataContext = null;
            if (LoginClick != null)
            {
                LoginClick("");
            }
        }
    }
}
