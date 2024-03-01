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
using System.Windows.Shapes;

namespace WpfPP
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        LombardEntities1 entities = new LombardEntities1();

        public Authorization()
        {
            InitializeComponent();
        }

        private void text_login_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (text_login.Text != "")
            {
                text_1.Text = "";
            }
            else
            {
                text_1.Text = "login";
            }
        }

        private void button_in_Click(object sender, RoutedEventArgs e)
        {
            var Login = text_login.Text; //TextBox содержит логин
            var Password = text_password.Text;//TextBox содержит пароль

            if(entities.Admin.Any(u => u.login == Login && u.password == Password))
            {
                MainWindow_Admin mainWindow_Admin = new MainWindow_Admin();
                mainWindow_Admin.Show();
                Close();
            }
            else
            {
                if (entities.User.Any(u => u.login == Login && u.password == Password))
                {
                    MainWindow window_ = new MainWindow();
                    window_.Show();
                    Close();
                }
                else
                {
                    Message message = new Message();
                    message.Show();
                }
            }
        }

        private void button_register_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Close();
        }

        private void text_password_PasswordChanged(object sender, TextChangedEventArgs e)
        {
            if (text_password.Text != null)
            {
                text_2.Text = "";
            }
            else
            {
                text_2.Text = "password";
            }

            if (text_password.Text.Length < 8)
            {
                error_pass_lenght.Visibility = Visibility.Visible;
                button_in.IsEnabled = false;
            }
            else
            {
                button_in.IsEnabled = true;
                error_pass_lenght.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
