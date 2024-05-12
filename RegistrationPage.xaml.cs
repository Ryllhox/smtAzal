using smtAzal.Domain;
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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private ApplicationDbContext _context;
        private LoginPage _loginPage;

        public RegistrationPage(LoginPage loginPage)
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            _loginPage = loginPage;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword) || string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(address))
            {
                txtErrorMessage.Visibility = Visibility.Visible;
                txtErrorMessage.Text = "Пж заполните все поля умоляю";
                return;
            }

            if (password != confirmPassword)
            {
                txtErrorMessage.Visibility = Visibility.Visible;
                txtErrorMessage.Text = "пароль не спички стоп чо";
                return;
            }

            if (_context.Users.Any(u => u.Login == login))
            {
                txtErrorMessage.Visibility = Visibility.Visible;
                txtErrorMessage.Text = "юзер с такой душой уже существует !!";
                return;
            }

            var newUser = new User
            {
                Login = login,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                Address = address,
                RoleId = 2 // assuming 2 is the role ID for regular user
                // You can add other properties here (discount, salon, etc.) if needed
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            MessageBox.Show("вы успешно вступили в клуб!");

            NavigationService.Navigate(_loginPage);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}
