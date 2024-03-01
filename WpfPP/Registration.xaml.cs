using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {

        LombardEntities1 entities = new LombardEntities1();
        public Registration()
        {

            InitializeComponent();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
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

        private void button_register_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = new User();
                if (text_login.Text == "" || text_password.Text == "" || text_repeat_password.Text == "")
                {
                    Message message = new Message();
                    message.Show();
                }
                else
                {
                    if (user != null)
                    {
                        user = new User();
                        entities.User.Add(user);
                    }
                    user.login = text_login.Text;
                    user.password = text_repeat_password.Text;
                    entities.SaveChanges();
                }
                Authorization authorization = new Authorization();
                authorization.Show();
                Close();
            }
            catch
            {
                Message message = new Message();
                message.Show();
            }
            
        }

        private void text_password_PasswordChanged(object sender, RoutedEventArgs e)
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
                button_register.IsEnabled = false;
            }
            else
            {
                button_register.IsEnabled = true;
                error_pass_lenght.Visibility = Visibility.Hidden;
            }
        }

        private void text_repeat_password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (text_repeat_password.Text != "")
            {
                text_3.Text = "";
            }
            else
            {
                text_3.Text = "repeat password";
            }

            if (text_password.Text != text_repeat_password.Text)
            {
                button_register.IsEnabled = false;
                error_pass.Visibility = Visibility.Visible;
            }
            else
            {
                button_register.IsEnabled = true;
                error_pass.Visibility = Visibility.Hidden;
                return;
            }
        }

        private void button_in_Click_1(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }
    }
}
