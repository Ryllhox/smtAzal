using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using smtAzal.Domain;
using smtAzal.Utilities;
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

namespace smtAzal
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private ApplicationDbContext _context;

        public LoginPage()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Password;

            var user = _context.Users.Include(u => u.Role)
                                      .FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user != null)
            {
                if (user.Role.Name == "Администратор")
                {
                    MessageBox.Show("0101");
                    NavigationService.Navigate(new AdminPage());
                }
                else
                {
                    MessageBox.Show("Приветствуем, пользователь!");
                    CurrentUser.SetUserId(user.Id);
                    NavigationService.Navigate(new HomePage());
                }
            }
            else
            {
                txtErrorMessage.Visibility = Visibility.Visible;
                txtErrorMessage.Text = "Неправильный логин или пароль";
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage(this));
        }

    }
}
