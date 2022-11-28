using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CapstoneQueryApplication
{
    /// <summary>
    /// Interaction logic for ItemWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public MainWindow.User ThisUser {get; set;}

        public bool NeedsUpdate { get; set; }

        public UserWindow(MainWindow.User user)
        {
            InitializeComponent();
            ThisUser = user;

            Title = "User ID: " + user.id.ToString();
            txtUsername.Text = user.username;

            NeedsUpdate = false;
        }

        public UserWindow()
        {
            InitializeComponent();
            Title = "New User";
            ThisUser = new MainWindow.User(0, "", "");
            btnUpdate.Content = "Add User";
            txtUsername.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThisUser.username = txtUsername.Text;
            if (txtPassword.Text != "")
            ThisUser.password = txtPassword.Text;
            NeedsUpdate = true;
            Close();
        }

        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnUpdate.IsEnabled = txtPassword.Text != "";
        }
    }
}
