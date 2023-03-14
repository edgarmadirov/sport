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
using SportShop.Pages;
namespace SportShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cfg.MainFrame = MainFrame;
            if (!cfg.IsAuth)
            {
                Auth.Content = "Войти";
            }
        }

        private void Assortiment_Click(object sender, RoutedEventArgs e)
        {
            cfg.MainFrame.Navigate(new AssortimentsPage());
        }

        private void OpenCards_Click(object sender, RoutedEventArgs e)
        {
            cfg.MainFrame.Navigate(new ShopingCartPage());
        }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
