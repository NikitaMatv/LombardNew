﻿using System;
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
    /// Логика взаимодействия для Message.xaml
    /// </summary>
    public partial class Message : Window
    {
        public Message()
        {
            InitializeComponent();
            label.Content = "Check the correctness of the entered\ndata.";
        }

        private void but_ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void X_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
