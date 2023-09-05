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
    /// Логика взаимодействия для CreatePass.xaml
    /// </summary>
    public partial class CreatePass : Page
    {
        List<ViewCreatePass> listPathWarehouse = MyEntity.Execute<ViewCreatePass>("select * from \"Hackaton\".view_list_create_pass");
        List<Order> listOrder = MyEntity.Execute<Order>("select * from \"Hackaton\".order");
        List<StatusOrder> listStatusOrder = MyEntity.Execute<StatusOrder>("select * from \"Hackaton\".status_order");
        List<Companies> listCompanyInfo = MyEntity.Execute<Companies>("select * from \"Hackaton\".companies");
        public CreatePass()
        {
            InitializeComponent();
            dgrdListCheckPoint.ItemsSource = listPathWarehouse;
            cmbCompanies.ItemsSource = listCompanyInfo;
        }


        public void sorting()
        {
            if (txbSearch.Text != "")
            {
                string search = (txbSearch.Text).ToLower();
                if (cmbCompanies.SelectedItem != null)
                {
                    string company = (cmbCompanies.SelectedItem as Companies).nameCompany;
                    if (txbFio.Text != "")
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
                        dgrdListCheckPoint.ItemsSource = listPathWarehouse.Where(s => s.FIO.ToLower().Contains(fio));
                    }
                    else
                    {
                        dgrdListCheckPoint.ItemsSource = listPathWarehouse;
                    }
                }
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (dgrdListCheckPoint.SelectedItem != null)
            {
                int idOrderStatus = (dgrdListCheckPoint.SelectedItem as ViewCreatePass).idOrderStatus;
                int newIdStatusOrder = listStatusOrder.Max(p => p.idStatusOrder) + 1;
                int idOrder = listStatusOrder.Where(p => p.idStatusOrder == idOrderStatus).FirstOrDefault().idOrder;
                var n = listOrder.Where(p => p.numberPass != "");
                int id = n.Max(p => p.idOrder);
                string numberPass = listOrder.Where(p => p.idOrder == id).FirstOrDefault().numberPass;
                long newNumberPass = (long.Parse(numberPass) + 1);

                try
                {
                    string sqlUpdate = string.Format("UPDATE \"Hackaton\".\"order\" SET number_pass={0} WHERE id={1}", newNumberPass, idOrder);
                    string sqlInsert = string.Format("INSERT INTO \"Hackaton\".status_order (id,id_status, id_order, date_start, date_end) VALUES({0},{1}, {2}, '{3}', '{4}')", newIdStatusOrder, 2, idOrder, (DateTime.Now).ToString("yyyy-MM-dd HH:MM:ss"), "-infinity");
                    MyEntity.Execute(sqlUpdate);
                    MyEntity.Execute(sqlInsert);

                    MessageBox.Show("Создание прошло успешно!");
                    dgrdListCheckPoint.ItemsSource = null;
                    listPathWarehouse = MyEntity.Execute<ViewCreatePass>("select * from \"Hackaton\".view_list_create_pass");
                    dgrdListCheckPoint.ItemsSource = listPathWarehouse;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Не выбран элемент из списка");
            }
        }

        private void cmbCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sorting();
        }



        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cmbCompanies.SelectedItem = null;
            txbFio.Text = "";
            txbSearch.Text = "";
            dgrdListCheckPoint.ItemsSource = listPathWarehouse;
        }

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            sorting();
        }

        private void txbFio_TextChanged(object sender, TextChangedEventArgs e)
        {
            sorting();
        }
    }
}
