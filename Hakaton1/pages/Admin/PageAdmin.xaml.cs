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

namespace Hakaton1.pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для PageAdmin.xaml
    /// </summary>
    public partial class PageAdmin : Page
    {
        public PageAdmin()
        {
            InitializeComponent();
            frmNavigateAdmin.Content = new othersPages.imagePages();
        }

        private void btnWorkWarehourse_Click(object sender, RoutedEventArgs e)
        {
            frmNavigateAdmin.Content = new WorkWarehouse();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            frmNavigateAdmin.Content = new Admin.AddUserPage();
        }

        private void btnListOrders_Click(object sender, RoutedEventArgs e)
        {
            frmNavigateAdmin.Content = new PageListOrders();
        }

        private void btnExitAutorization_Click(object sender, RoutedEventArgs e)
        {
            frmNavigateAdmin.Content = new AuthorizationPage();
        }

        private void btnCreateOrders_Click(object sender, RoutedEventArgs e)
        {
            frmNavigateAdmin.Content =new OrderMan.OrderManPage();
        }

        private void btnWorkCheckpoint_Click(object sender, RoutedEventArgs e)
        {
            frmNavigateAdmin.Content = new Checkpoint.CheckPoint();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default["UserId"] = 0;
            Settings.Default.Save();
            Page page = new AuthorizationPage();
            NavigationService.GetNavigationService(this).Navigate(page);
        }

        private void btnLiftTruck_Click(object sender, RoutedEventArgs e)
        {
            frmNavigateAdmin.Content = new OperatorLiftTruck.GridPage();
        }
    }
}
