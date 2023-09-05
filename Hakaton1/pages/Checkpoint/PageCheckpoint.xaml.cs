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

namespace Hakaton1.pages.Checkpoint
{
    /// <summary>
    /// Логика взаимодействия для PageCheckpoint.xaml
    /// </summary>
    public partial class PageCheckpoint : Page
    {
        List<ViewListCheckpoint> listPathWarehouse = MyEntity.Execute<ViewListCheckpoint>("select * from \"Hackaton\".view_list_checkpoint");
        public PageCheckpoint()
        {
            InitializeComponent();
            dgrdListCheckPoint.ItemsSource = listPathWarehouse;
            List<Companies> listCompanies = MyEntity.Execute<Companies>("select * from \"Hackaton\".companies"); 
            cmbCompanies.ItemsSource = listCompanies;
        }

        private void btnChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            if(dgrdListCheckPoint.SelectedItem != null)
            {
                int idOrderStatus = (dgrdListCheckPoint.SelectedItem as ViewListCheckpoint).idOrderStatus;
                StatusOrder statusOrder = MyEntity.Execute<StatusOrder>(string.Format("select * from \"Hackaton\".status_order st where st.id = {0}", idOrderStatus)).FirstOrDefault();
                List<StatusOrder> statusOrders = MyEntity.Execute<StatusOrder>("select * from \"Hackaton\".status_order");
                string updateOrderStatus = string.Format("UPDATE \"Hackaton\".status_order SET date_end='{0}' WHERE id={1}", (DateTime.Now).ToString("yyyy.MM.dd hh:mm:ss"), idOrderStatus);
                int maxId = statusOrders.Max(s => s.idStatusOrder) + 1;
                string insertOrderStatus = string.Format("INSERT INTO \"Hackaton\".status_order (id, id_status, id_order, date_start, date_end) VALUES({0}, {1}, {2}, '{3}', '{4}')", maxId, 1, statusOrder.idOrder, (DateTime.Now).ToString("yyyy-MM-dd HH:MM:ss"), "-infinity");
                try
                {
                    MyEntity.Execute(updateOrderStatus);
                    MyEntity.Execute(insertOrderStatus);
                    dgrdListCheckPoint.ItemsSource = null;
                    listPathWarehouse = MyEntity.Execute<ViewListCheckpoint>("select * from \"Hackaton\".view_list_checkpoint");
                    dgrdListCheckPoint.ItemsSource = listPathWarehouse;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cmbCompanies.SelectedItem = null;
            txbSearch.Text = "";
            dgrdListCheckPoint.ItemsSource = null;
            listPathWarehouse = MyEntity.Execute<ViewListCheckpoint>("select * from \"Hackaton\".view_list_checkpoint");
            dgrdListCheckPoint.ItemsSource = listPathWarehouse;
        }

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            sorting();
        }

        private void cmbCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sorting();
        }

        public void sorting()
        {
            if (txbSearch.Text != "")
            {
                string search = (txbSearch.Text).ToLower();
                if (cmbCompanies.SelectedItem != null)
                {
                    string company = (cmbCompanies.SelectedItem as Companies).nameCompany;
                    if(txbFio.Text != "")
                    {
                        string fio = (txbFio.Text).ToLower();
                        dgrdListCheckPoint.ItemsSource = listPathWarehouse.Where(r => r.numberCar.ToLower().Contains(search) && r.nameCompany == company && r.FIO.ToLower().Contains(fio));
                    }
                    else
                    {
                        dgrdListCheckPoint.ItemsSource = listPathWarehouse.Where(r => r.numberCar.ToLower().Contains(search) && r.nameCompany == company);
                    }              
                }
                else
                {
                    if (txbFio.Text != "")
                    {
                        string fio = (txbFio.Text).ToLower();
                        dgrdListCheckPoint.ItemsSource = listPathWarehouse.Where(r => r.numberCar.ToLower().Contains(search) && r.FIO.ToLower().Contains(fio));
                    }
                    else
                    {
                        dgrdListCheckPoint.ItemsSource = listPathWarehouse.Where(r => r.numberCar.ToLower().Contains(search));
                    }
                }
            }
            else
            {
                if (cmbCompanies.SelectedItem != null)
                {
                    string company = (cmbCompanies.SelectedItem as Companies).nameCompany;
                    if (txbFio.Text != "")
                    {
                        string fio = (txbFio.Text).ToLower();
                        dgrdListCheckPoint.ItemsSource = listPathWarehouse.Where(r => r.nameCompany == company && r.FIO.ToLower().Contains(fio));
                    }
                    else
                    {
                        dgrdListCheckPoint.ItemsSource = listPathWarehouse.Where(r => r.nameCompany == company);
                    }
                }
                else
                {
                    if (txbFio.Text != "")
                    {
                        string fio = (txbFio.Text).ToLower();
                        dgrdListCheckPoint.ItemsSource = listPathWarehouse.Where(s=>s.FIO.ToLower().Contains(fio));
                    }
                    else
                    {
                        dgrdListCheckPoint.ItemsSource = listPathWarehouse;
                    }
                }
            }
        }

        private void txbFio_TextChanged(object sender, TextChangedEventArgs e)
        {
            sorting();
        }
    }
}
