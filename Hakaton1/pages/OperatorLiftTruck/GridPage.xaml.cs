using Hakaton1.Infrastructure;
using Hakaton1.models;
using Hakaton1.models.Views;
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

namespace Hakaton1.pages.OperatorLiftTruck
{
    /// <summary>
    /// Логика взаимодействия для GridPage.xaml
    /// </summary>
    public partial class GridPage : Page
    {
        public List<Product> products = MyEntity.Execute<Product>("select  * from \"Hackaton\".product p");
        public List<ViewCoordinates> view_s = MyEntity.Execute<ViewCoordinates>("select  * from \"Hackaton\".view_coordins vc ");
        public GridPage()
        {
            InitializeComponent();
            cbItems.ItemsSource = products;
            cbItems.Visibility = Visibility.Collapsed;
            lblInArea.Visibility = Visibility.Collapsed;
            lblInStreat.Visibility = Visibility.Collapsed;

            if (App.Current.Resources["idRole"] != null)
            {
                btnBack.Visibility = Visibility.Collapsed;
            }
        }

        private void cbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            decimal allWeight = 0;
            string type_wieght = "";
            txbInfoSklads.Text = "";
            txbInfoStreet.Text = "";
            int Length = Int32.Parse(txbLenghtArea.Text);
            int With = Int32.Parse(txbWithArea.Text);
            List<string> checkCoordinats = new List<string>();
            string[,] arr = new string[Length, With];
            List<string> values = new List<string>();
            // заполнение values
            string selectItem = (cbItems.SelectedItem as Product).nameProduct;

            foreach (var item in stp.Children)
            {
                if (item.GetType().Name == "StackPanel")
                {

                    foreach (var value in (item as StackPanel).Children)
                    {
                        if (value.GetType().Name == "Button")
                        {
                            (value as Button).Background = new SolidColorBrush(Colors.White);

                        }
                    }

                }
            }
            foreach (var item in stg.Children)
            {
                if (item.GetType().Name == "StackPanel")
                {

                    foreach (var value in (item as StackPanel).Children)
                    {
                        if (value.GetType().Name == "Button")
                        {
                            (value as Button).Background = new SolidColorBrush(Colors.AntiqueWhite);

                        }
                    }

                }
            }
            foreach (var coord in view_s)
            {
                if (coord.name_product == selectItem)
                {
                    checkCoordinats.Add(coord.x + ";" + coord.y);
                }
            }

            foreach (var item in stp.Children)
            {
                if (item.GetType().Name == "StackPanel")
                {

                    foreach (var value in (item as StackPanel).Children)
                    {
                        if (value.GetType().Name == "Button")
                        {
                            values.Add((value as Button).Content.ToString());
                            if (cbItems.SelectedItem != null)
                            {

                                foreach (var cor in checkCoordinats)
                                {
                                    string[] place = (value as Button).Content.ToString().Split('.');
                                    if (place[0] == "A")
                                    {
                                        lblInArea.Visibility = Visibility.Visible;
                                        lblInArea.Content = "На складе:";
                                        lblInArea.Foreground = new SolidColorBrush(Colors.DarkRed);
                                        if (place[1] == cor)
                                        {
                                            string cordinate = (value as Button).Content.ToString();

                                            string[] arrcor = place[1].Split(';');
                                            (value as Button).Background = new SolidColorBrush(Colors.LightGreen);
                                            string wieght = view_s.Where(s => s.x == arrcor[0] && s.y == arrcor[1]).FirstOrDefault().wieth.ToString();
                                            type_wieght = view_s.Where(s => s.x == arrcor[0] && s.y == arrcor[1]).FirstOrDefault().name_type_weight;


                                            txbInfoSklads.Text += cordinate + " : " + wieght + " " + type_wieght + "\n";
                                            allWeight += decimal.Parse(wieght);
                                        }
                                    }


                                }



                            }

                        }
                    }

                }
            }
            //////////////////////SREEEETS/////////////////
            ///


            foreach (var item in stg.Children)
            {
                if (item.GetType().Name == "StackPanel")
                {

                    foreach (var value in (item as StackPanel).Children)
                    {
                        if (value.GetType().Name == "Button")
                        {
                            values.Add((value as Button).Content.ToString());
                            if (cbItems.SelectedItem != null)
                            {

                                foreach (var cor in checkCoordinats)
                                {
                                    string[] place = (value as Button).Content.ToString().Split('.');
                                    if (place[0] == "S")
                                    {
                                        lblInStreat.Visibility = Visibility.Visible;
                                        lblInStreat.Content = "УЛИЦА:";
                                        lblInStreat.Foreground = new SolidColorBrush(Colors.DarkRed);
                                        if (place[1] == cor)
                                        {
                                            string cordinate = (value as Button).Content.ToString();

                                            string[] arrcor = place[1].Split(';');
                                            (value as Button).Background = new SolidColorBrush(Colors.Yellow);
                                            string wieght = view_s.Where(s => s.x == arrcor[0] && s.y == arrcor[1]).FirstOrDefault().wieth.ToString();
                                            type_wieght = view_s.Where(s => s.x == arrcor[0] && s.y == arrcor[1]).FirstOrDefault().name_type_weight;


                                            txbInfoStreet.Text += cordinate + " : " + wieght + " " + type_wieght + "\n";
                                            allWeight += decimal.Parse(wieght);
                                        }
                                    }


                                }



                            }

                        }
                    }

                }
            }
            // заполнение массива
            //int g= 0;
            btnCreate.Visibility = Visibility.Collapsed;
            finalInfo.Visibility = Visibility.Visible;
            finalInfo.Content = allWeight.ToString() + " " + type_wieght;
            finalInfo.Foreground = new SolidColorBrush(Colors.DarkRed);
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            int Length = Int32.Parse(txbLenghtArea.Text);
            int With = Int32.Parse(txbWithArea.Text);

            int LengthStreet = int.Parse(txbWith.Text);
            int WithStreet = int.Parse(txbLenghtStreet.Text);

            int X = 0;
            int Y = 0;
            stp.Children.Clear();



            for (int i = 0; i < Length; i++)
            {
                stp.Children.Add(new StackPanel { Orientation = Orientation.Horizontal });

            }
            for (int g = 0; g < LengthStreet; g++)
            {
                stg.Children.Add(new StackPanel { Orientation = Orientation.Horizontal });
            }
            foreach (var child in stp.Children)
            {
                X++;
                if (child.GetType().Name == "StackPanel")
                {
                    for (int f = 0; f < With; f++)
                    {
                        Y = f;
                        //Где A - Ангар(aria)
                        (child as StackPanel).Children.Add(new Button
                        {
                            Content = String.Format($"A.{X};{Y + 1}"),
                            Margin = new Thickness(5),
                            Height = 40,
                            Width = 40,
                            Background = new SolidColorBrush(Colors.AntiqueWhite),
                            Foreground = new SolidColorBrush(Colors.Black)
                            
                        });

                    }
                }
            }
            foreach (var child in stg.Children)
            {
                X++;
                if (child.GetType().Name == "StackPanel")
                {
                    for (int f = 0; f < WithStreet; f++)
                    {
                        Y = f;
                        //Где S - улица(street)
                        (child as StackPanel).Children.Add(new Button { Content = String.Format($"S.{Y + 1};{X}"), Margin = new Thickness(5), Height = 40, Width = 40 , Foreground = new SolidColorBrush(Colors.Black) });

                    }
                }
            }
            cbItems.Visibility = Visibility.Visible;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new Uri("pages/Admin/PageAdmin.xaml", UriKind.RelativeOrAbsolute));
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
