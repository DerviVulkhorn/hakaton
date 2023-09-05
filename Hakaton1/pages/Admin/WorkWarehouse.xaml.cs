using Hakaton1.Infrastructure;
using Hakaton1.models;
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
using WPF_hueta.model;

namespace Hakaton1.pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для WorkWarehouse.xaml
    /// </summary>
    public partial class WorkWarehouse : Page
    {

        public List<ViewPathWarehouse> view_path_in_warehouse_list = new List<ViewPathWarehouse>();
        List<Status> listStatusInfo = MyEntity.Execute<Status>("select * from \"Hackaton\".status");
        public WorkWarehouse()
        {
            InitializeComponent();
            string sql = "select * FROM \"Hackaton\".view_to_warehouse;";
            view_path_in_warehouse_list = MyEntity.Execute<ViewPathWarehouse>(sql);
            dtgPathToWearehouse.ItemsSource = view_path_in_warehouse_list;

           


            Vse(view_path_in_warehouse_list);
            sql = "select * FROM \"Hackaton\".product;";
            List<Product> products_list = MyEntity.Execute<Product>(sql);


            List<string> listStatus = new List<string>();
            listStatus.Add("Все");
            foreach (var r in listStatusInfo)
            {
                listStatus.Add(r.nameStatus.ToString());
            }
            cmbStatus.ItemsSource = listStatus;





        }

        private void btnChangeState_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStatus.SelectedItem == null)
            {
                dtgPathToWearehouse.ItemsSource = view_path_in_warehouse_list;
            }
            else
            {
                string status = cmbStatus.SelectedItem.ToString();
                if (status == "В пути на склад")
                {
                    dgtxTime.Visibility = Visibility.Visible;
                }
                else
                {
                    dgtxTime.Visibility = Visibility.Hidden;
                }
                if (status == "Все")
                {
                    dtgPathToWearehouse.ItemsSource = view_path_in_warehouse_list;
                }
                else
                {
                    dtgPathToWearehouse.ItemsSource = view_path_in_warehouse_list.Where(p => p.name_status == status).ToList();
                }

            }
        }

        private void cmbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<ViewPathWarehouse> view_path_in_warehouse_list = new List<ViewPathWarehouse>();
            string combob = (sender as ComboBox).SelectedItem.ToString();
            if (combob == "Все")
            {
                Vse(view_path_in_warehouse_list);
            }
            else
            {


            }
        }
        public List<ViewPathWarehouse> Vse(List<ViewPathWarehouse> view_path_in_warehouse_list)
        {
            string sql = "select * FROM \"Hackaton\".view_to_warehouse;";
            view_path_in_warehouse_list = MyEntity.Execute<ViewPathWarehouse>(sql);
            dtgPathToWearehouse.ItemsSource = view_path_in_warehouse_list;

            return view_path_in_warehouse_list;
        }
    }
}
