using Hakaton1.Infrastructure;
using Hakaton1.models;
using Hakaton1.pages;
using Hakaton1.pages.Admin;
using Hakaton1.pages.Checkpoint;
using Hakaton1.pages.OperatorLiftTruck;
using Hakaton1.pages.OrderMan;
using Hakaton1.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Hakaton1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if((int)Settings.Default["UserId"] != 0)
            {
                try
                {
                    List<Users> users = MyEntity.Execute<Users>("SELECT * FROM \"Hackaton\".users");
                    Users user = users.Where(s => s.idUser == (int)Settings.Default["UserId"]).FirstOrDefault();
                    if (user != null)
                    {
                        switch (user.idRole)
                        {
                            //warehouse manager
                            case 1:
                                {
                                    //frmNavigate.Content = new WarehouseManagerPage();
                                    break;
                                }
                            //администратор
                            case 3:
                                {
                                    App.Current.Resources["idRole"] = user.idRole;
                                    frmNavigate.Content = new PageAdmin();
                                    break;
                                }
                            //сотрудник кпп
                            case 4:
                                {
                                    frmNavigate.Content = new CheckPoint();
                                    break;
                                }
                            //оператор погрузчика
                            case 5:
                                {
                                    frmNavigate.Content = new GridPage();
                                    break;
                                }
                            //экономист
                            case 6:
                                {
                                    frmNavigate.Content = new OrderManPage();
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection error " + ex.ToString());
                }
            }
            else
            {
                frmNavigate.Content = new AuthorizationPage();
            }
        }
        private void btn_collapse_Click(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private void btn_Full_hd_Click(object sender, RoutedEventArgs e)
        {
             App.Current.MainWindow.WindowState = WindowState.Maximized;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        

        private void dtg_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MessageBox.Show("lb yf[w");
        }
    }
}
