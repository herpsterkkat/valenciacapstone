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
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CapstoneQueryApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBConnection databaseConnection = DBConnection.Instance();

        public class ShopProduct
        {
            public int id { get; set; }
            public string type { get; set; }
            public string category { get; set; }
            public string size { get; set; }
            public string name { get; set; }

            public ShopProduct(int id, string type, string category, string size, string name)
            {
                this.id = id;
                this.type = type;
                this.category = category;
                this.size = size;
                this.name = name;
            }
        }

        public class User
        {
            public int id { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public User(int id, string username, string password)
            {
                this.id = id;
                this.username = username;
                this.password = password;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();

            loginWindow.ShowDialog();

            // Pass login info.
            databaseConnection.Server = "clobundance.shop";
            databaseConnection.DatabaseName = "capstone";
            databaseConnection.UserName = loginWindow.Username;
            databaseConnection.Password = loginWindow.Password;

            // Get table listing for product categories.
            if (databaseConnection.IsConnect())
            {
                string query = "SHOW TABLES";
                var cmd = new MySqlCommand(query, databaseConnection.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Get a table name.
                    string tableName = reader.GetString(0);

                    // Only add relevant tables.
                    if (tableName.StartsWith("men") || tableName.StartsWith("women"))
                        lstProductCategories.Items.Add(tableName);
                }
                databaseConnection.Close();
            }
            else
            {
                MessageBox.Show("Failed to connect to server.");
                Environment.Exit(0);
            }


            // Create a new list of users.
            List<User> users = new List<User>();

            // Get table listing for user data.
            if (databaseConnection.IsConnect())
            {
                string query = $"SELECT * FROM `users`";
                var cmd = new MySqlCommand(query, databaseConnection.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Add selected item to list.
                    users.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }
                databaseConnection.Close();

                // Set the new list as the table source.
                userData.ItemsSource = users;
            }
            else
            {
                MessageBox.Show("Failed to connect to server.");
            }
        }

        private void lstProductCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAddProduct.IsEnabled = true;

            UpdateTable();
        }

        private void UpdateTable()
        {
            // Create a new list of products.
            List<ShopProduct> products = new List<ShopProduct>();

            // Get table for selected product category.
            if (databaseConnection.IsConnect())
            {
                string query = $"SELECT * FROM `{lstProductCategories.SelectedItem}`";
                var cmd = new MySqlCommand(query, databaseConnection.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Add selected item to list.
                    products.Add(new ShopProduct(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
                }
                databaseConnection.Close();

                // Set the new list as the table source.
                productData.ItemsSource = products;
            }
            else
            {
                MessageBox.Show("Failed to connect to server.");
            }
        }

        private void UpdateUserTable()
        {
            // Create a new list of users.
            List<User> users = new List<User>();

            // Get table for users.
            if (databaseConnection.IsConnect())
            {
                string query = $"SELECT * FROM `users`";
                var cmd = new MySqlCommand(query, databaseConnection.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Add selected item to list.
                    users.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }
                databaseConnection.Close();

                // Set the new list as the table source.
                userData.ItemsSource = users;
            }
            else
            {
                MessageBox.Show("Failed to connect to server.");
            }
        }

        private void productData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ItemWindow itemWindow = null;
            if (productData.SelectedItem is ShopProduct shopProduct)
                itemWindow = new ItemWindow(shopProduct);

            if (itemWindow != null)
            {
                itemWindow.ShowDialog();

                if (itemWindow.NeedsUpdate)
                {
                    // Update the product
                    if (databaseConnection.IsConnect())
                    {
                        string query = $"UPDATE `{lstProductCategories.SelectedItem}` SET type = '{itemWindow.ThisProduct.type}', category = '{itemWindow.ThisProduct.category}', size = '{itemWindow.ThisProduct.size}', name = '{itemWindow.ThisProduct.name}' WHERE id = '{itemWindow.ThisProduct.id}';";
                        //query = query.Replace("'", "''");
                        var cmd = new MySqlCommand(query, databaseConnection.Connection);
                        var reader = cmd.ExecuteNonQuery();

                        if (reader == 1)
                            MessageBox.Show("Update successful!");
                        else
                            MessageBox.Show("Update failed!");

                        databaseConnection.Close();

                        UpdateTable();
                    }
                    else
                    {
                        MessageBox.Show("Failed to connect to server.");
                    }
                }
            }
        }

        private void userData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UserWindow userWindow = null;
            if (userData.SelectedItem is User user)
                userWindow = new UserWindow(user);

            if (userWindow != null)
            {
                userWindow.ShowDialog();

                if (userWindow.NeedsUpdate)
                {
                    // Update the product
                    if (databaseConnection.IsConnect())
                    {
                        string query = $"UPDATE `users` SET username = '{userWindow.ThisUser.username}', password = MD5('{userWindow.ThisUser.password}') WHERE id = '{userWindow.ThisUser.id}';";
                        var cmd = new MySqlCommand(query, databaseConnection.Connection);
                        var reader = cmd.ExecuteNonQuery();

                        if (reader == 1)
                            MessageBox.Show("Update successful!");
                        else
                            MessageBox.Show("Update failed!");

                        databaseConnection.Close();

                        UpdateUserTable();
                    }
                    else
                    {
                        MessageBox.Show("Failed to connect to server.");
                    }
                }
            }
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            UserWindow userWindow = new UserWindow();

            userWindow.ShowDialog();

            if (userWindow.NeedsUpdate)
            {
                // Update the product
                if (databaseConnection.IsConnect())
                {
                    string query = $"INSERT INTO `users` (`id`, `username`, `password`) VALUES(NULL, '{userWindow.ThisUser.username}', MD5('{userWindow.ThisUser.password}'));";
                    var cmd = new MySqlCommand(query, databaseConnection.Connection);
                    var reader = cmd.ExecuteNonQuery();

                    if (reader == 1)
                        MessageBox.Show("Update successful!");
                    else
                        MessageBox.Show("Update failed!");

                    databaseConnection.Close();

                    UpdateUserTable();
                }
                else
                {
                    MessageBox.Show("Failed to connect to server.");
                }
            }
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            ItemWindow itemWindow = new ItemWindow();

            itemWindow.ShowDialog();

            if (itemWindow.NeedsUpdate)
            {
                // Update the product
                if (databaseConnection.IsConnect())
                {
                    string query = $"INSERT INTO `{lstProductCategories.SelectedItem}` (`id`, `type`, `category`, `size`, `name`) VALUES(NULL, '{itemWindow.ThisProduct.type}', '{itemWindow.ThisProduct.category}', '{itemWindow.ThisProduct.size}', '{itemWindow.ThisProduct.name}');";
                    var cmd = new MySqlCommand(query, databaseConnection.Connection);
                    var reader = cmd.ExecuteNonQuery();

                    if (reader == 1)
                        MessageBox.Show("Update successful!");
                    else
                        MessageBox.Show("Update failed!");

                    databaseConnection.Close();

                    UpdateTable();
                }
                else
                {
                    MessageBox.Show("Failed to connect to server.");
                }
            }
        }
    }
}
