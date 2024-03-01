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
using System.Windows.Controls.Primitives;
using Microsoft.Win32;

namespace WpfPP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Contract_window_admin : Window
    {
        private int title = 0;
        private int price = 0;

        LombardEntities1 entities = new LombardEntities1();
        public Contract_window_admin()
        {
            InitializeComponent();
            if (App.is_first == true)
            {
                Authorization authorization = new Authorization();
                authorization.Show();
                Close();
                App.is_first = false;
            }

            foreach (var entity in entities.Dogovor)
                main_list.Items.Add(entity);
            foreach (var cat in entities.Client)
                Combo_Client.Items.Add(cat);
            foreach (var type in entities.Sotrudnik)
                Combo_Employee.Items.Add(type);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (poisk.Text != null)
            {
                Text_Searc.Text = "";
            }
            else
            {
                Text_Searc.Text = "  Search a product";
            }

            if (string.IsNullOrEmpty(poisk.Text.Trim()) == false)
            {
                using (LombardEntities1 entities = new LombardEntities1())
                {
                    main_list.Items.Clear();
                    foreach (var items in entities.Dogovor)
                    {
                        if (items.date_dogovor.ToString().StartsWith(poisk.Text.ToLower()) ||
                            items.date_srokzaloga.ToString().StartsWith(poisk.Text.ToLower()) ||
                            items.procent.ToString().StartsWith(poisk.Text.ToLower()))
                            main_list.Items.Add(items);
                    }
                }
            }
            else if (poisk.Text.Trim() == "")
            {
                main_list.Items.Clear();
                foreach (var item in entities.Dogovor)
                    main_list.Items.Add(item);
            }
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_product_Click(object sender, RoutedEventArgs e)
        {  
            main_list.SelectedIndex = -1;
            Combo_Client.SelectedIndex = -1;
            Combo_Employee.SelectedIndex = -1;

            text_date_dogovor.SelectedDate = DateTime.Now;
            text_date_srokzaloga.SelectedDate = DateTime.Now;
            text_Percent.Text = "";

            main_list.SelectedIndex = -1;
        }

        private void main_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selected = main_list.SelectedItem as Dogovor;
                if(selected != null)
                {
                    Title.Content = selected.date_dogovor;
                    text_date_dogovor.SelectedDate = selected.date_dogovor;
                    text_date_srokzaloga.SelectedDate = selected.date_srokzaloga;
                    text_Percent.Text = selected.procent;

                    Combo_Client.SelectedItem = (from client in entities.Client where client.id_client == selected.Id_Client select client).Single<Client>();
                    Combo_Employee.SelectedItem = (from employee in entities.Sotrudnik where employee.Id_Sotrudnik == selected.Id_Sotrudnik select employee).Single<Sotrudnik>();
                }

            }
            catch
            {
                Message message = new Message();
                message.Show();
            }
        }

        private void clear_product_Click(object sender, RoutedEventArgs e)
        {
            Title.Content = "Title";

            text_date_dogovor.SelectedDate = DateTime.Now;
            text_date_srokzaloga.SelectedDate = DateTime.Now;
            text_Percent.Text = "";

            main_list.SelectedIndex = -1;
            Combo_Client.SelectedIndex = -1;
            Combo_Employee.SelectedIndex = -1;
        }

        private void deelte_product_Click(object sender, RoutedEventArgs e)
        {
            var rezult = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rezult == MessageBoxResult.No)
                return;
            var delete = main_list.SelectedItem as Dogovor;
            try
            {
                var ext = (from structur in entities.Tovar where structur.Id_dogovor == delete.Id_Dogovor select structur).First();
                Message message = new Message();
                message.Main.Content = "There are no objects to be deleted!";
                message.label.Content = "Error";
                message.Show();
            }
            catch
            {
                if (delete != null)
                {
                    entities.Dogovor.Remove(delete);
                    entities.SaveChanges();

                    text_date_dogovor.SelectedDate = DateTime.Now;
                    text_date_srokzaloga.SelectedDate = DateTime.Now;
                    text_Percent.Clear();

                    Combo_Client.Items.Clear();
                    Combo_Employee.Items.Clear();

                    main_list.Items.Remove(delete);
                }
                else
                {
                    Message message = new Message();
                    message.Main.Content = "There are no objects to be deleted!";
                    message.label.Content = "Error";
                    message.Show();
                }
            }
        }

        private void save_product_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var save = main_list.SelectedItem as Dogovor;
                if (text_Percent.Text == "" || Combo_Client.SelectedIndex == -1 || Combo_Employee.SelectedIndex  == -1 || text_date_dogovor.SelectedDate == null || text_date_srokzaloga.SelectedDate == null)
                {
                    Message message = new Message();

                    message.label.Content = "Mistake";
                    message.Show();
                }
                else
                {
                    if (save == null)
                    {
                        save = new Dogovor();
                        entities.Dogovor.Add(save);
                        main_list.Items.Add(save);
                    }

                    save.Id_Client = (Combo_Client.SelectedItem as Client).id_client;
                    save.Id_Sotrudnik = (Combo_Employee.SelectedItem as Sotrudnik).Id_Sotrudnik;
                    save.date_dogovor = text_date_dogovor.SelectedDate;
                    save.date_srokzaloga = text_date_srokzaloga.SelectedDate;
                    save.procent = text_Percent.Text;
                    entities.SaveChanges();
                    main_list.Items.Refresh();
                }
            }
            catch
            {
                Message message = new Message();
                message.label.Content = "Select an image!";
                message.Show();
            }
        }

       

        private void title_product_Click(object sender, RoutedEventArgs e)
        {
            price = 0;

            border_price.Visibility = Visibility.Hidden;

            title++;
            if(title % 2 == 1)
            {
                border_title.Visibility = Visibility.Visible;
                main_list.Items.SortDescriptions.Clear();
                main_list.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("date_dogovor", System.ComponentModel.ListSortDirection.Ascending));
            }
            else
            {
                border_title.Visibility = Visibility.Hidden;
                main_list.Items.SortDescriptions.Clear();
            }
        }
        

        private void price_product_Click(object sender, RoutedEventArgs e)
        {
            title = 0;

            border_title.Visibility = Visibility.Hidden;

            price++;
            if (price % 2 == 1)
            {
                border_price.Visibility = Visibility.Visible;
                main_list.Items.SortDescriptions.Clear();
                main_list.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("date_srokzaloga", System.ComponentModel.ListSortDirection.Ascending));
            }
            else
            {
                border_price.Visibility = Visibility.Hidden;
                main_list.Items.SortDescriptions.Clear();
            }
        }

        private void HamburgerMenuItem_Selected_1(object sender, RoutedEventArgs e)
        {
            Contract_window authorization = new Contract_window();
            authorization.Show();
            Close();
        }

        private void Home_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = Home;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Home";
        }

        private void Home_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Сontract_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = Profile;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Contract";
        }

        private void Сontract_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Settings_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = Settings;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Сlientele";
        }

        private void Settings_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Exit_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = Exit;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Exit";
        }

        private void Exit_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            Client_window_admin window1 = new Client_window_admin();
            window1.Show();
            Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            Close();
        }

        private void Сontract_Click(object sender, RoutedEventArgs e)
        {
            Contract_window_admin contract_Window = new Contract_window_admin();
            contract_Window.Show();
            Close();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow_Admin window = new MainWindow_Admin();
            window.Show();
            Close();
        }

        private void text_Percent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Client_window_admin client_Window = new Client_window_admin();
            client_Window.Show();
            Close();
        }

        private void category_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = category;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Category";
        }

        private void category_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void category_Click(object sender, RoutedEventArgs e)
        {
            Category_window_admin category_Window_Admin = new Category_window_admin();
            category_Window_Admin.Show();
            Close();
        }

        private void sotrudnik_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = sotrudnik;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Employee";
        }

        private void sotrudnik_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void sotrudnik_Click(object sender, RoutedEventArgs e)
        {
            Sotrudnik_window_admin sotrudnik_Window_Admin = new Sotrudnik_window_admin();
            sotrudnik_Window_Admin.Show();
            Close();
        }

        private void users_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = users;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Users";
        }

        private void users_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void users_Click(object sender, RoutedEventArgs e)
        {
            User_window_admin user_Window_Admin = new User_window_admin();
            user_Window_Admin.Show();
            Close();
        }
    }
}
