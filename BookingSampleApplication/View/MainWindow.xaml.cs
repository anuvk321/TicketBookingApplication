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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainViewVM;
        public MainWindow()
        {
            InitializeComponent();
            mainViewVM = new MainWindowViewModel();
            this.DataContext = mainViewVM;
        }

        private void LoginControl_LoginClick(string value)
        {
            LoadMainWindow();
        }

        private void LoadMainWindow()
        {
            SimpleLoginControl.Visibility = System.Windows.Visibility.Collapsed;
            MainControl.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
