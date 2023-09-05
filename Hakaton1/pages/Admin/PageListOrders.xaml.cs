using Hakaton1.Infrastructure;
using Hakaton1.models;
using Hakaton1.models.Views;
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
    /// Логика взаимодействия для PageListOrders.xaml
    /// </summary>
    public partial class PageListOrders : Page
    {
        List<ViewOrderList> listOrdersInfo = MyEntity.Execute<ViewOrderList>("select * from \"Hackaton\".view_list_orders");
        List<ContractViewInfo> conInfo = new List<ContractViewInfo>();

        List<Companies> listCompanyInfo = MyEntity.Execute<Companies>("select * from \"Hackaton\".companies");
        public PageListOrders()
        {
            InitializeComponent();

            foreach(var order in listOrdersInfo)
            {
                conInfo.Add(new ContractViewInfo(order));
            }

            dgrdListOrders.ItemsSource = conInfo;

            List<string> listStatus = new List<string>();
            listStatus.Add("Все");
            listStatus.Add("Готов");
            listStatus.Add("Выполняется");
            cmbStatus.ItemsSource = listStatus;

            List<string> listCompany = new List<string>();
            listCompany.Add("Все");
            foreach (var r in listCompanyInfo)
            {
                listCompany.Add(r.nameCompany);
            }
            cmbCompany.ItemsSource = listCompany;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            dgrdListOrders.ItemsSource = listOrdersInfo;
            cmbCompany.SelectedItem = null;
            cmbStatus.SelectedItem = null;
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FiltersOrder();
        }

        private void cmbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FiltersOrder();
        }

        private void FiltersOrder()
        {
            if (cmbStatus.SelectedItem == null)
            {
                if (cmbCompany.SelectedItem == null)
                {
                    dgrdListOrders.ItemsSource = conInfo;
                }
                else
                {
                    string type = cmbCompany.SelectedItem.ToString();
                    if (type == "Все")
                    {
                        dgrdListOrders.ItemsSource = conInfo;
                    }
                    else
                    {
                        dgrdListOrders.ItemsSource = conInfo.Where(p => p.name_company == type);
                    }
                }
            }
            else
            {
                string status = cmbStatus.SelectedItem.ToString();
                if (cmbCompany.SelectedItem == null)
                {
                    if (status == "Все")
                    {
                        dgrdListOrders.ItemsSource = conInfo;
                    }
                    else
                    {
                        dgrdListOrders.ItemsSource = conInfo.Where(p => p.finished == status);
                    }
                }
                else
                {
                    string type = cmbCompany.SelectedItem.ToString();
                    if (status == "Все")
                    {
                        if (type == "Все")
                        {
                            dgrdListOrders.ItemsSource = conInfo;
                        }
                        else
                        {
                            dgrdListOrders.ItemsSource = conInfo.Where(p => p.name_company == type);
                        }
                    }
                    else
                    {
                        if (type == "Все")
                        {
                            dgrdListOrders.ItemsSource = conInfo.Where(p => p.finished == status);
                        }
                        else
                        {
                            dgrdListOrders.ItemsSource = conInfo.Where(p => p.name_company == type && p.finished == status);
                        }
                    }

                }
            }

        }

        private void cmbDone_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
