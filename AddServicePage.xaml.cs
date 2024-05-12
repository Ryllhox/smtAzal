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
    /// Логика взаимодействия для AddServicePage.xaml
    /// </summary>
    public partial class AddServicePage : Page
    {
        private readonly ApplicationDbContext _context;

        public AddServicePage(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
            LoadServiceTypes();
        }

        private void LoadServiceTypes()
        {
            var serviceTypes = _context.ServiceTypes.ToList();
            cmbServiceTypes.ItemsSource = serviceTypes;
            cmbServiceTypes.DisplayMemberPath = "Name"; // Отображать свойство Name объекта ServiceType
            cmbServiceTypes.SelectedValuePath = "Id"; // Использовать свойство Id в качестве выбранного значения
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string description = txtDescription.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            var selectedServiceType = cmbServiceTypes.SelectedItem as ServiceType;

            if (selectedServiceType != null)
            {
                var newService = new Service
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    ServiceTypeId = selectedServiceType.Id
                };

                _context.Services.Add(newService);
                _context.SaveChanges();

                MessageBox.Show("Еще одна причина потреблять!");
                NavigationService.Navigate(new AdminPage());
            }
            else
            {
                MessageBox.Show("а тип услуги ты дома не забыл?");
            }
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPage());
        }
    }
}
