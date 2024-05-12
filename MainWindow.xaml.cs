using smtAzal.Domain;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new LoginPage();
        }

        // Метод для загрузки страницы в основной фрейм
        public void LoadPage(Page page)
        {
            MainFrame.NavigationService.Navigate(page);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // При загрузке главного окна показываем страницу входа
            LoadPage(new LoginPage());
        }
    }
}