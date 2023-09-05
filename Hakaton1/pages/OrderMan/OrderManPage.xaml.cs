using Hakaton1.Infrastructure;
using Hakaton1.models;
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

namespace Hakaton1.pages.OrderMan
{
    /// <summary>
    /// Логика взаимодействия для OrderManPage.xaml
    /// </summary>
    public partial class OrderManPage : Page
    {
        List<ProductsOut> productsOut = new List<ProductsOut>();
        public OrderManPage()
        {
            InitializeComponent();
            List<ModelCar> models = MyEntity.Execute<ModelCar>("SELECT * FROM \"Hackaton\".model_car");
            cmbModelAuto.ItemsSource = models;
            List<Product> products = MyEntity.Execute<Product>("SELECT * FROM \"Hackaton\".product");
            cmbProduct.ItemsSource = products;
            List<TypeWeight> types = MyEntity.Execute<TypeWeight>("SELECT * FROM \"Hackaton\".type_weight");
            cmbTypeWeight.ItemsSource = types;
            List<Companies> companies = MyEntity.Execute<Companies>("SELECT * FROM \"Hackaton\".companies");
            cmbCompany.ItemsSource = companies;
            dtpDateStart.SelectedDate = DateTime.Now;
            dgrdProducts.ItemsSource = productsOut;

            if (App.Current.Resources["idRole"] != null)
            {
                btnBack.Visibility = Visibility.Collapsed;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txbFirstName.Text != "" && txbLastName.Text != "" && txbPatronymic.Text != "" && txbTonnage.Text != "" && productsOut.Count > 0 &&
                txbNumberCar.Text != "" && cmbCompany.SelectedItem != null && cmbModelAuto.SelectedItem != null && dtpDateEnd.SelectedDate != null && dtpDateStart.SelectedDate != null)
            {
                if (dtpDateStart.SelectedDate > dtpDateEnd.SelectedDate.Value)
                {
                    MessageBox.Show("Дата начала не может быть больше начала конца!");
                }
                else
                {
                    decimal allWeight = 0;
                    foreach (var prod in productsOut)
                    {
                        if(prod.nameType == "Т")
                        {
                            allWeight += prod.weight;
                        }
                        if(prod.nameType == "КГ")
                        {
                            decimal w = prod.weight / 1000;
                            allWeight += w;
                        }
                    }
                    if(decimal.Parse(txbTonnage.Text) < allWeight)
                    {
                        MessageBox.Show("Вес заказа не может превышать грузоподъемность авто!");
                    }
                    else
                    {
                        try
                        {
                            List<Users> users = MyEntity.Execute<Users>("SELECT * FROM \"Hackaton\".users");
                            List<Car> cars = MyEntity.Execute<Car>("SELECT * FROM \"Hackaton\".car");
                            List<CarUser> carUsers = MyEntity.Execute<CarUser>("SELECT * FROM \"Hackaton\".car_user");

                            Car car = cars.Where(s => s.number == txbNumberCar.Text).FirstOrDefault();
                            //если нет машины с таким номером, то ее добавит
                            if (car == null)
                            {
                                string insertCars = string.Format("INSERT INTO \"Hackaton\".car (number_car, id_model_car) VALUES('{0}', {1})", txbNumberCar.Text, (cmbModelAuto.SelectedItem as ModelCar).idModel);
                                MyEntity.Execute(insertCars);
                                //обновление данных
                                cars = MyEntity.Execute<Car>("SELECT * FROM \"Hackaton\".car");
                                car = cars.Where(s => s.number == txbNumberCar.Text).FirstOrDefault();
                                MessageBox.Show("Новый авто успешно добавлен!");
                            }

                            Users user = users.Where(s => s.firstName == txbFirstName.Text && txbLastName.Text == s.lastName && txbPatronymic.Text == s.patronymic).FirstOrDefault();
                            //если нет такого водителя, то его добавит
                            if (user == null)
                            {
                                string insertUsers = string.Format("INSERT INTO \"Hackaton\".users (last_name, first_name, patronymic, id_role) VALUES('{0}', '{1}', '{2}', {3})", txbLastName.Text, txbFirstName.Text, txbPatronymic.Text, 2);
                                MyEntity.Execute(insertUsers);
                                users = MyEntity.Execute<Users>("SELECT * FROM \"Hackaton\".users");
                                user = users.Where(s => s.firstName == txbFirstName.Text && txbLastName.Text == s.lastName && txbPatronymic.Text == s.patronymic).FirstOrDefault();
                                MessageBox.Show("Новый водитель успешно добавлен!");
                            }

                            CarUser carUser = carUsers.Where(s => s.idCar == car.idCar && s.idUser == user.idUser).FirstOrDefault();
                            //если нет связи водителя с машиной, то ее добавит
                            if (carUser == null)
                            {
                                string insertCarUser = string.Format("INSERT INTO \"Hackaton\".car_user (id_car, id_user) VALUES({0}, {1})", car.idCar, user.idUser);
                                MyEntity.Execute(insertCarUser);
                                carUsers = MyEntity.Execute<CarUser>("SELECT * FROM \"Hackaton\".car_user");
                                carUser = carUsers.Where(s => s.idCar == car.idCar && s.idUser == user.idUser).FirstOrDefault();
                                MessageBox.Show("Связь водителя с авто успешно добавлена!");
                            }

                            string insertOrders = string.Format("INSERT INTO \"Hackaton\".\"order\" (number_pass, id_company, id_car_user, date_start, date_end, is_finished) VALUES('{0}', {1}, {2}, '{3}', '{4}', {5})",
                                null, (cmbCompany.SelectedItem as Companies).idCompany, carUser.idCarUser, (dtpDateStart.SelectedDate.Value).ToString("yyyy.MM.dd hh:mm:ss"), (dtpDateEnd.SelectedDate.Value).ToString("yyyy.MM.dd hh:mm:ss"), false);
                            MyEntity.Execute(insertOrders);

                            List<Order> orders = MyEntity.Execute<Order>("SELECT * FROM \"Hackaton\".order");

                            foreach (var prod in productsOut)
                            {
                                string insertOrderProducts = string.Format("INSERT INTO \"Hackaton\".order_products (id_product, id_order, weight, id_type_weight) VALUES({0}, {1}, {2}, {3})", prod.idProduct, orders.Max(s => s.idOrder), prod.weight, prod.idType);
                                MyEntity.Execute(insertOrderProducts);
                                MessageBox.Show("Товар добавлен в заказ!");
                            }
                            List<StatusOrder> statusOrders = MyEntity.Execute<StatusOrder>("select * from \"Hackaton\".status_order");
                            int maxId = statusOrders.Max(s => s.idStatusOrder) + 1;
                            string insertOrderStatus = string.Format("INSERT INTO \"Hackaton\".status_order (id, id_status, id_order, date_start, date_end) VALUES({0}, {1}, {2}, '{3}', '{4}')", maxId, 8, orders.Max(s=>s.idOrder), (DateTime.Now).ToString("yyyy-MM-dd HH:MM:ss"), "-infinity");
                            MyEntity.Execute(insertOrderStatus);
                            MessageBox.Show("Заказ успешно добавлен!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }                  
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default["UserId"] = 0;
            Settings.Default.Save();
            Page page = new AuthorizationPage();
            NavigationService.GetNavigationService(this).Navigate(page);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dgrdProducts.SelectedItem != null)
            {
                int idProduct = (dgrdProducts.SelectedItem as ProductsOut).idProduct;
                var selectProduct = productsOut.Find(p => p.idProduct == idProduct);
                productsOut.Remove(selectProduct);
                dgrdProducts.ItemsSource = null;
                dgrdProducts.ItemsSource = productsOut;
            }
            else
            {
                MessageBox.Show("В таблице не выбран товар!");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cmbTypeWeight.SelectedItem != null && cmbProduct.SelectedItem != null && txbWeight.Text != "")
            {
                Product selectProduct = (cmbProduct.SelectedItem as Product);
                TypeWeight typeWeight = (cmbTypeWeight.SelectedItem as TypeWeight);
                var findProduct = productsOut.Find(po => po.idProduct == selectProduct.idProduct);
                if (findProduct == null)
                {
                    productsOut.Add(new ProductsOut() { idProduct = selectProduct.idProduct, nameProduct = selectProduct.nameProduct, weight = decimal.Parse(txbWeight.Text), idType = typeWeight.idType, nameType = typeWeight.nameType });
                    cmbTypeWeight.SelectedItem = null;
                    cmbProduct.SelectedItem = null;
                    txbWeight.Text = "";
                }
                else
                {
                    MessageBox.Show("Этот товар уже выбран!");
                }
                dgrdProducts.ItemsSource = null;
                dgrdProducts.ItemsSource = productsOut;
            }
            else
            {
                MessageBox.Show("Не выбран товар\nили тип ед. изм.\nили не введен вес");
            }
        }
    }
}
