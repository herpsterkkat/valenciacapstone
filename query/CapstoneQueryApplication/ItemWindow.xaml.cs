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
    public partial class ItemWindow : Window
    {
        public MainWindow.ShopProduct ThisProduct {get; set;}

        public bool NeedsUpdate { get; set; }

        public ItemWindow(MainWindow.ShopProduct product)
        {
            InitializeComponent();
            ThisProduct = product;

            Title = "Item ID: " + product.id.ToString();
            txtType.Text = product.type;
            txtSize.Text = product.size;
            txtCategory.Text = product.category;
            txtName.Text = product.name;

            NeedsUpdate = false;
        }

        public ItemWindow()
        {
            InitializeComponent();
            Title = "New Product";
            ThisProduct = new MainWindow.ShopProduct(0, "", "", "", "");
            btnUpdate.Content = "Add Product";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ThisProduct.type = txtType.Text;
            ThisProduct.size = txtSize.Text;
            ThisProduct.category = txtCategory.Text;
            ThisProduct.name = txtName.Text;
            NeedsUpdate = true;
            Close();
        }
    }
}
