using Hakaton1.Properties;
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

namespace Hakaton1.pages.Checkpoint
{
    /// <summary>
    /// Логика взаимодействия для CheckPoint.xaml
    /// </summary>
    public partial class CheckPoint : Page
    {
        public CheckPoint()
        {
            InitializeComponent();
            frmCheck.Content = new PageCheckpoint();
            frmCreate.Content = new CreatePass();

            if (App.Current.Resources["idRole"] != null)
            {
                btnBack.Visibility = Visibility.Collapsed;
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default["UserId"] = 0;
            Settings.Default.Save();
            Page page = new AuthorizationPage();
            NavigationService.GetNavigationService(this).Navigate(page);
        }
    }
}
