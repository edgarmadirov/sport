using SportShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SportShop
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private string _code = string.Empty;

        public AuthWindow()
        {
            InitializeComponent();
            GenerateCode();

        }
        private string GenerateCode()
        {
            Random random= new Random();
            List <string> chars = new List<string>() {"1","2","3","5","4","6","7","8","9", "a","b","c","d","f","h","g","p","m","j"};
            string result = string.Empty;
            for(int i = 0; i < 6; i++)
            {
                result += chars[random.Next(chars.Count)];
            }
            Code.Text = "Капча: " + result;
            return _code = result;
        }
        private void Gost_Click(object sender, RoutedEventArgs e)
        {
            cfg.AuthUser = null;
            cfg.IsAuth = false;
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            List <User> users = App.db.Users.ToList();
            User u = users.FirstOrDefault(el => el.UserLogin == Login.Text && el.UserPassword == Password.Text);
            if (CodeInput.Text == _code)
            {
                if (u != null)
                {
               
                    MessageBox.Show($"Добро пожаловать, {u.UserName}.\nВаша роль: {u.Role.RoleName}");
                    cfg.AuthUser = u;
                    cfg.IsAuth = true;
                    MainWindow window = new MainWindow();
                    window.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Такого Пользователя не существует!");
                }
            }
            else
            {
                MessageBox.Show("Вы не правильно ввели капчу, блокировка приложения на 10 сек.");
                Thread.Sleep(10000);
                GenerateCode();
            }
        }
    }
}
