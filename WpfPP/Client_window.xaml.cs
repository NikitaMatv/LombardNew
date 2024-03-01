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
    public partial class Client_window : Window
    {
        private int surname = 0;
        private int name = 0;

        LombardEntities1 entities = new LombardEntities1();
        public Client_window()
        {
            InitializeComponent();

            foreach (var entity in entities.Client)
                main_list.Items.Add(entity);
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
                    foreach (var items in entities.Client)
                    {
                        if (items.Surname.ToString().StartsWith(poisk.Text.ToLower()) ||
                            items.Name.ToString().StartsWith(poisk.Text.ToLower()) ||
                            items.phone.ToString().StartsWith(poisk.Text.ToLower()) ||
                            items.paspoport_number.ToString().StartsWith(poisk.Text.ToLower()))
                            main_list.Items.Add(items);
                    }
                }
            }
            else if (poisk.Text.Trim() == "")
            {
                main_list.Items.Clear();
                foreach (var item in entities.Client)
                    main_list.Items.Add(item);
            }
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_product_Click(object sender, RoutedEventArgs e)
        {
            Title.Content = "Client";
            main_list.SelectedIndex = -1;
            text_Name.Text = "";
            text_Surname.Text = "";
            text_number.Text = "";
            text_series.Text = "";
            text_phone.Text = "";
        }

        private void main_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selected = main_list.SelectedItem as Client;
                if(selected != null)
                {
                    Title.Content = selected.Surname;
                    text_Name.Text = selected.Name;
                    text_Surname.Text = selected.Surname;
                    text_phone.Text = selected.phone;
                    text_number.Text = selected.paspoport_number.ToString();
                    text_series.Text = selected.pasport_seria.ToString();
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
            Title.Content = "Client";

            text_Surname.Text = "";
            text_number.Text = "";
            text_phone.Text = "";
            text_series.Text = "";
            text_Name.Text = "";

            main_list.SelectedIndex = -1;
        }

        private void deelte_product_Click(object sender, RoutedEventArgs e)
        {
            var rezult = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rezult == MessageBoxResult.No)
                return;
            var delete = main_list.SelectedItem as Client;
            try
            {
                var ext = (from structur in entities.Dogovor where structur.Id_Client == delete.id_client select structur).First();
                Message message = new Message();
                message.Main.Content = "There are no objects to be deleted!";
                message.label.Content = "Error";
                message.Show();
            }
            catch
            {
                if (delete != null)
                {
                    entities.Client.Remove(delete);
                    entities.SaveChanges();

                    text_Name.Clear();
                    text_number.Clear();
                    text_phone.Clear();
                    text_phone.Clear();
                    text_series.Clear();
                    text_Surname.Clear();

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
                var save = main_list.SelectedItem as Client;
                if (text_Name.Text == "")
                {
                    Message message = new Message();
                    message.label.Content = "Mistake";
                    message.Show();
                }
                else
                {
                    if (save == null)
                    {
                        save = new Client();
                        entities.Client.Add(save);
                        main_list.Items.Add(save);
                    }
                    save.Surname = text_Surname.Text;
                    save.Name = text_Name.Text;
                    save.phone = text_phone.Text;
                    save.paspoport_number = Convert.ToInt32(text_number.Text);
                    save.pasport_seria = Convert.ToInt32(text_series.Text);

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
            // null
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            Close();
        }
        private void Сontract_Click(object sender, RoutedEventArgs e)
        {
            Contract_window contract_Window = new Contract_window();
            contract_Window.Show();
            Close();
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
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

        private void surname_client_Click(object sender, RoutedEventArgs e)
        {
            name = 0;

            border_price.Visibility = Visibility.Hidden;

            surname++;
            if (surname % 2 == 1)
            {
                border_title.Visibility = Visibility.Visible;
                main_list.Items.SortDescriptions.Clear();
                main_list.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Surname", System.ComponentModel.ListSortDirection.Ascending));
            }
            else
            {
                border_title.Visibility = Visibility.Hidden;
                main_list.Items.SortDescriptions.Clear();
            }
        }

        private void name_client_Click(object sender, RoutedEventArgs e)
        {
            surname = 0;

            border_title.Visibility = Visibility.Hidden;

            name++;
            if (name % 2 == 1)
            {
                border_price.Visibility = Visibility.Visible;
                main_list.Items.SortDescriptions.Clear();
                main_list.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name", System.ComponentModel.ListSortDirection.Ascending));
            }
            else
            {
                border_price.Visibility = Visibility.Hidden;
                main_list.Items.SortDescriptions.Clear();
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
