using Hakaton1.Infrastructure;
using Hakaton1.models;
using Hakaton1.options;
using Hakaton1.pages.Admin;
using Hakaton1.pages.Checkpoint;
using Hakaton1.pages.OperatorLiftTruck;
using Hakaton1.pages.OrderMan;
using Hakaton1.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Hakaton1.pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
            txbPass.Visibility = Visibility.Collapsed;
            gbError.Visibility = Visibility.Hidden;
        }

        private void txbPass_TextChanged(object sender, TextChangedEventArgs e)
        {
            psbPass.Password = txbPass.Text;
        }

        private void chbShow_Click(object sender, RoutedEventArgs e)
        {
            if (chbShow.IsChecked == true)
            {
                txbPass.Text = psbPass.Password;
                txbPass.Visibility = Visibility.Visible;
                psbPass.Visibility = Visibility.Collapsed;
            }
            else if (chbShow.IsChecked == false)
            {
                psbPass.Password = txbPass.Text;
                psbPass.Visibility = Visibility.Visible;
                txbPass.Visibility = Visibility.Collapsed;
            }
        }

        public void remember(Users user)
        {
            if (chbRemember.IsChecked == true)
            {
                Settings.Default["UserId"] = user.idUser;
                Settings.Default.Save();
            }
            else if (chbRemember.IsChecked == false)
            {
                Settings.Default["UserId"] = 0;
                Settings.Default.Save();
            }
        }

        public bool validate(string login, string password)
        {
            if (login == "" || password == "")
            {
                txbError.Text = string.Format("Не все поля заполнены!");
                gbError.Visibility = Visibility.Visible;
                return false;
            }
            Regex regexPas = new Regex(@"^(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$");
            Match matchPas = regexPas.Match(password);
            if (!matchPas.Success)
            {
                txbError.Text = string.Format("Пароль должен быть не менее 8 символов,\nбуквы в верхнем и нижнем регистрах и цифры,\nбуквы в английской и/или русской раскладке!");
                gbError.Visibility = Visibility.Visible;
                return false;
            }
            if (login.Length > 32 || login.Length < 6)
            {
                txbError.Text = string.Format("Логин не может быть больше 32 символов\nи не меньше 6 символов!");
                gbError.Visibility = Visibility.Visible;
                return false;
            }
            return true;
        }

        public void clearingFields()
        {
            gbError.Visibility = Visibility.Visible;
            txbLogin.Text = null;
            txbPass.Text = null;
            psbPass.Password = null;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            bool valid = validate(txbLogin.Text, psbPass.Password);
            if(valid == true)
            {
                HashPassword hash = new HashPassword();
                string hashedPassword = hash.hashingPassword(psbPass.Password);
                try
                {
                    List<Users> users = MyEntity.Execute<Users>("SELECT * FROM \"Hackaton\".users");
                    Users user = users.Where(s => s.login == txbLogin.Text && hashedPassword == s.password).FirstOrDefault();
                    if (user != null)
                    {
                        switch (user.idRole)
                        {
                            //warehouse manager
                            case 1:
                                {
                                    //remember(user);
                                    //Page page = new WarehouseManagerPage();
                                    //NavigationService.GetNavigationService(this).Navigate(page);
                                    break;
                                }
                            //администратор
                            case 3:
                                {
                                    remember(user);
                                    App.Current.Resources["idRole"] = user.idRole;
                                    Page page = new PageAdmin();
                                    NavigationService.GetNavigationService(this).Navigate(page);
                                    break;
                                }
                            //сотрудник кпп
                            case 4:
                                {
                                    remember(user);
                                    Page page = new CheckPoint();
                                    NavigationService.GetNavigationService(this).Navigate(page);
                                    break;
                                }
                            //оператор погрузчика
                            case 5:
                                {
                                    remember(user);
                                    Page page = new GridPage();
                                    NavigationService.GetNavigationService(this).Navigate(page);
                                    break;
                                }
                            //экономист
                            case 6:
                                {
                                    remember(user);
                                    Page page = new OrderManPage();
                                    NavigationService.GetNavigationService(this).Navigate(page);
                                    break;
                                }
                            default:
                                {
                                    txbError.Text = string.Format("Отказано в доступе.\nРоль не подходит для этого приложения!");
                                    clearingFields();
                                    break;
                                }
                        }
                    }
                    else
                    {
                        txbError.Text = string.Format("Пользователь не найден!");
                        clearingFields();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Connection error "+ ex.ToString());
                }
            }
        }

        private void btnCheat_Click(object sender, RoutedEventArgs e)
        {
            List<Users> users = new List<Users>();
            Users users1 = new Users();
            users1.idRole = 3;
            users1.firstName = "test";
            users1.lastName = "test";
            users1.patronymic = "test";
            users1.idUser = 99;
            users1.login = "test";
            users.Add(users1);
            Page page = new PageAdmin();
            NavigationService.GetNavigationService(this).Navigate(page);
        }
    }
}
