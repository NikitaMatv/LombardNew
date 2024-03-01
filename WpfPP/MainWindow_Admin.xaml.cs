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
    public partial class MainWindow_Admin : Window
    {
        private int title = 0;
        private int status = 0;
        private int price = 0;
        private int contract = 0;

        LombardEntities1 entities = new LombardEntities1();
        public MainWindow_Admin()
        {
            InitializeComponent();
            if (App.is_first == true)
            {
                Authorization authorization = new Authorization();
                authorization.Show();
                Close();
                App.is_first = false;
            }
            foreach (var entity in entities.Tovar)
                main_list.Items.Add(entity);
            foreach (var cat in entities.Category)
                Combo_Category.Items.Add(cat);
            foreach (var type in entities.Dogovor)
                Combo_Dogovor.Items.Add(type);
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
                    foreach (var items in entities.Tovar)
                    {
                        if (items.Name_Tovar.ToLower().StartsWith(poisk.Text.ToLower()) ||
                            items.status.ToLower().StartsWith(poisk.Text.ToLower()) ||
                            items.price.ToLower().StartsWith(poisk.Text.ToLower()) ||
                            items.Id_dogovor.ToString().StartsWith(poisk.Text.ToLower()))
                            main_list.Items.Add(items);
                    }
                }
            }
            else if (poisk.Text.Trim() == "")
            {
                main_list.Items.Clear();
                foreach (var item in entities.Tovar)
                    main_list.Items.Add(item);
            }
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_product_Click(object sender, RoutedEventArgs e)
        {
            text_name.Text = "";
            text_price.Text = "";
            text_status.Text = "";
            image.Source = null;
            main_list.SelectedIndex = -1;
            Combo_Category.SelectedIndex = -1;
            Combo_Dogovor.SelectedIndex = -1;

            main_list.SelectedIndex = -1;
        }

        private void main_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selected = main_list.SelectedItem as Tovar;
                if(selected != null)
                {
                    Title.Content = selected.Name_Tovar;
                    text_name.Text = selected.Name_Tovar;
                    text_price.Text = selected.price;
                    text_status.Text = selected.status;

                    if (selected.Img != null)
                    {
                        image.Source = new BitmapImage(new Uri(selected.Img));
                    }
                    if (selected.Img == null)
                    {
                        image.Source = new BitmapImage(new Uri("/img/messagebox.jpg", UriKind.Relative));
                    }

                    Combo_Category.SelectedItem = (from Name in entities.Category where Name.Id_Category == selected.Id_Category select Name).Single<Category>();
                    Combo_Dogovor.SelectedItem = (from vid in entities.Dogovor where vid.Id_Dogovor == selected.Id_dogovor select vid).Single<Dogovor>();
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
            text_name.Text = "";
            text_price.Text = "";
            text_status.Text = "";
            image.Source = null;
            main_list.SelectedIndex = -1;
            Combo_Category.SelectedIndex = -1;
            Combo_Dogovor.SelectedIndex = -1;
        }

        private void deelte_product_Click(object sender, RoutedEventArgs e)
        {
            var rezult = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rezult == MessageBoxResult.No)
                return;
            var delete = main_list.SelectedItem as Tovar;
            if (delete != null)
            {
                entities.Tovar.Remove(delete);
                entities.SaveChanges();

                image.Source = new BitmapImage(new Uri("11111.jpg", UriKind.Relative));
                text_name.Clear();
                text_price.Clear();
                text_status.Clear();

                Combo_Category.Items.Clear();
                Combo_Dogovor.Items.Clear();

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

        private void save_product_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var save = main_list.SelectedItem as Tovar;
                if (text_name.Text == "" || text_price.Text == "" || text_status.Text == "" || Combo_Category.SelectedIndex == -1 || Combo_Dogovor.SelectedIndex  == -1)
                {
                    Message message = new Message();

                    message.label.Content = "Mistake";
                    message.Show();
                }
                else
                {
                    if (save == null)
                    {
                        save = new Tovar();
                        entities.Tovar.Add(save);
                        main_list.Items.Add(save);
                    }
                    save.Name_Tovar = text_name.Text;
                    save.price = text_price.Text;
                    save.status = text_status.Text;
                    save.Id_Category = (Combo_Category.SelectedItem as Category).Id_Category;
                    save.Id_dogovor = (Combo_Dogovor.SelectedItem as Dogovor).Id_Dogovor;
                    if (image.Source == null)
                    {
                        save.Img = null;
                    }
                    if (image.Source != null)
                    {
                        save.Img = image.Source.ToString();
                    }
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

        private void img_product_Copy_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                image.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void title_product_Click(object sender, RoutedEventArgs e)
        {
            status = 0;
            price = 0;
            contract = 0;

            border_price.Visibility = Visibility.Hidden;
            border_contact.Visibility = Visibility.Hidden;
            border_status.Visibility = Visibility.Hidden;

            title++;
            if(title % 2 == 1)
            {
                border_title.Visibility = Visibility.Visible;
                main_list.Items.SortDescriptions.Clear();
                main_list.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Name_Tovar", System.ComponentModel.ListSortDirection.Ascending));
            }
            else
            {
                border_title.Visibility = Visibility.Hidden;
                main_list.Items.SortDescriptions.Clear();
            }
        }

        private void status_product_Click(object sender, RoutedEventArgs e)
        {
            title = 0;
            price = 0;
            contract = 0;

            border_price.Visibility = Visibility.Hidden;
            border_title.Visibility = Visibility.Hidden;
            border_contact.Visibility = Visibility.Hidden;

            status++;
            if (status % 2 == 1)
            {
                border_status.Visibility = Visibility.Visible;
                main_list.Items.SortDescriptions.Clear();
                main_list.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("status", System.ComponentModel.ListSortDirection.Ascending));
            }
            else
            {
                border_status.Visibility = Visibility.Hidden;
                main_list.Items.SortDescriptions.Clear();
            }
        }

        private void price_product_Click(object sender, RoutedEventArgs e)
        {
            title = 0;
            status = 0;
            contract = 0;

            border_contact.Visibility = Visibility.Hidden;
            border_title.Visibility = Visibility.Hidden;
            border_status.Visibility = Visibility.Hidden;

            price++;
            if (price % 2 == 1)
            {
                border_price.Visibility = Visibility.Visible;
                main_list.Items.SortDescriptions.Clear();
                main_list.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("price", System.ComponentModel.ListSortDirection.Ascending));
            }
            else
            {
                border_price.Visibility = Visibility.Hidden;
                main_list.Items.SortDescriptions.Clear();
            }
        }

        private void contract_product_Click(object sender, RoutedEventArgs e)
        {
            title = 0;
            status = 0;
            price = 0;

            border_price.Visibility = Visibility.Hidden;
            border_title.Visibility = Visibility.Hidden;
            border_status.Visibility = Visibility.Hidden;

            contract++;
            if (contract % 2 == 1)
            {
                border_contact.Visibility = Visibility.Visible;
                main_list.Items.SortDescriptions.Clear();
                main_list.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("Id_dogovor", System.ComponentModel.ListSortDirection.Ascending));
            }
            else
            {
                border_contact.Visibility = Visibility.Hidden;
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
            Contract_window_admin window1 = new Contract_window_admin();
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

        private void Home_Click(object sender, RoutedEventArgs e)
        {

        }

        private void text_price_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void text_price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
