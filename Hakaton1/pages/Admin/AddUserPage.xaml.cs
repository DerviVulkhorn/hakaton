using Hakaton1.Infrastructure;
using Hakaton1.models;
using Hakaton1.options;
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

namespace Hakaton1.pages.Admin
{
    /// <summary>
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        public AddUserPage()
        {
            InitializeComponent();
            List<Roles> roles = MyEntity.Execute<Roles>("SELECT id, name_role FROM \"Hackaton\".roles");
            cmbRole.ItemsSource = roles;
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (txbLastName.Text != "" && txbFirstName.Text != "" && txbPatronymic.Text != "" && txbLogin.Text != "" && txbPassword.Text != "" && cmbRole.SelectedItem != null)
            {
                bool valid = validate(txbLogin.Text, txbPassword.Text);
                if (valid == true)
                {
                    HashPassword hash = new HashPassword();
                    string hashedPassword = hash.hashingPassword(txbPassword.Text);
                    string insertUsers = string.Format("INSERT INTO \"Hackaton\".users (last_name, first_name, patronymic, login, \"password\", id_role) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', {5})", txbLastName.Text, txbFirstName.Text, txbPatronymic.Text, txbLogin.Text, hashedPassword, (cmbRole.SelectedItem as Roles).idRole);
                    try
                    {
                        MyEntity.Execute(insertUsers);
                        MessageBox.Show("Пользователь успешно создан!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!");
            }
        }

        public bool validate(string login, string password)
        {
            Regex regexPas = new Regex(@"^(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$");
            Match matchPas = regexPas.Match(password);
            if (!matchPas.Success)
            {
                MessageBox.Show("Пароль должен быть не менее 8 символов,\nбуквы в верхнем и нижнем регистрах и цифры,\nбуквы в английской и/или русской раскладке!");
                return false;
            }
            if (login.Length > 32 || login.Length < 6)
            {
                MessageBox.Show("Логин не может быть больше 32 символов\nи не меньше 6 символов!");
                return false;
            }
            return true;
        }
    }
}
