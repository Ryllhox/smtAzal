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
    /// Логика взаимодействия для UserProfilePage.xaml
    /// </summary>
    public partial class UserProfilePage : Page
    {
        private readonly int _userId;
        private readonly ApplicationDbContext _context;

        public UserProfilePage(int userId)
        {
            InitializeComponent();
            _userId = userId;
            _context = new ApplicationDbContext();

            // Загрузка данных пользователя
            LoadUserData();
        }

        private void LoadUserData()
        {
            // Находим пользователя по Id
            var user = _context.Users.FirstOrDefault(u => u.Id == _userId);

            // Проверяем, найден ли пользователь
            if (user != null)
            {
                // Заполняем данные пользователя на странице
                txtUsername.Text = user.Login;
                txtPassword.Password= user.Password;
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
                txtEmail.Text = user.Email;
                txtPhone.Text = user.Phone;
                txtAddress.Text = user.Address;
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
        }
        private void UpdateUser_Click(object sender, RoutedEventArgs e)
        {
            var _currentUser = _context.Users.FirstOrDefault(u => u.Id == _userId);

            _currentUser.Login = txtUsername.Text;
            _currentUser.Password = txtPassword.Password;
            _currentUser.FirstName = txtFirstName.Text;
            _currentUser.LastName = txtLastName.Text;
            _currentUser.Email = txtEmail.Text;
            _currentUser.Phone = txtPhone.Text;
            _currentUser.Address = txtAddress.Text;

            _context.SaveChanges();
            MessageBox.Show("Данные о пользователе успешно обновлены!");
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HomePage());
        }
    }
}
