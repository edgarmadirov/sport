using SportShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace SportShop.Pages
{
    /// <summary>
    /// Interaction logic for ShopingCartPage.xaml
    /// </summary>
    
    public partial class ShopingCartPage : Page
    {
        static private double _sum = 0;
        List<Product> shopingCart = new List<Product>();
        int PickupPointId = 0;
        public ShopingCartPage()
        {
            InitializeComponent();
            ComboPickup.ItemsSource = App.db.OrderPickups.ToList();
            ComboPickup.SelectedIndex= 0;
            LoadDataGrid();
        }

        private void UpdatePage()
        {
            cfg.MainFrame.Navigate(new ShopingCartPage());
        }
        private void UpdateTotalSumText(double sum)
        { TotalSum.Text = Convert.ToString(sum) + " Р."; }
        public void LoadDataGrid()
        {
            List<OrderProduct> orderProducts = App.db.OrderProducts.Where(el => el.Order.UserID == cfg.AuthUser.UserID && el.Order.OrderStatusID == 1).ToList();
            List<Product> allProduct = App.db.Products.ToList();
            _sum = 0;
            foreach (OrderProduct product in orderProducts)
            {
                Product productadd = allProduct.Where(el=>el.ProductArticleNumber == product.ProductArticleNumber).FirstOrDefault();
                if(productadd != null)
                {
                    productadd.ProductQuantityInStock = product.Count ?? 0;
                    _sum += productadd.ProductCostWithDiscount * Convert.ToDouble(productadd.ProductQuantityInStock);
                    shopingCart.Add(productadd);
                }
            }
            UpdateTotalSumText(_sum);
            DataGridSC.ItemsSource = shopingCart;
        }
        public void DeleteProduct(Product product)
        {
            shopingCart.Remove(product);
            
            List<OrderProduct> orderProducts = App.db.OrderProducts.ToList();
            OrderProduct findProduct = orderProducts.FirstOrDefault(el => el.ProductArticleNumber == product.ProductArticleNumber && el.Order.UserID == cfg.AuthUser.UserID && el.Order.OrderStatusID == 1);
            if (findProduct != null)
            {
                App.db.OrderProducts.Remove(findProduct);
                MessageBox.Show("Товар удален из корзины!");
            }
            else
            {
                MessageBox.Show("Товар уже удален!");
            }
            App.db.SaveChanges();
        }
        public void UpdateProductQuantity(Product product, int Value)
        {
            List<OrderProduct> orderProducts = App.db.OrderProducts.ToList();
            OrderProduct findProduct = orderProducts.FirstOrDefault(el => el.ProductArticleNumber == product.ProductArticleNumber && el.Order.UserID == cfg.AuthUser.UserID && el.Order.OrderStatusID == 1);
            if (findProduct != null)
            {
                findProduct.Count = Value;
            }
            else
            {
                MessageBox.Show("Товар уже удален!");
            }
            App.db.SaveChanges();
        }
        private void DataGridSC_LoadingRow(object sender, DataGridRowEventArgs e)
        { }
        private void DataGridSC_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {  }
        
        private void Order_Click(object sender, RoutedEventArgs e)
        {
            var orders = App.db.Orders;
            Order order = orders.FirstOrDefault(el => el.UserID == cfg.AuthUser.UserID && el.OrderStatusID == 1);
            if (order != null)
            {
                if (PickupPointId == 0)
                {
                    MessageBox.Show("Выбери пункт выдачи.");
                    return;
                }
                order.OrderCreate = DateTime.Now;
                if (shopingCart.Count > 3)
                {
                    order.OrderDeliveryDate = DateTime.Now.AddDays(3);
                }
                else
                {
                    order.OrderDeliveryDate = DateTime.Now.AddDays(6);
                }
                
                order.OrderPickupPoint = PickupPointId;
                order.OrderStatusID = 2;
                App.db.SaveChanges();
                UpdatePage();
            }
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Product selected = (sender as Button).DataContext as Product;
            DeleteProduct(selected);
            UpdatePage();
        }
        private void count_TextChanged(object sender, TextChangedEventArgs e)
        {
            var ob = (sender as TextBox);
            Product selected = (sender as TextBox).DataContext as Product;
            if (ob.IsLoaded)
            {
                int selectedIndex = DataGridSC.SelectedIndex;
                var text = ob.Text;
                int number;
                bool isNumber = int.TryParse(text, out number);
                if (isNumber)
                {
                   if(number == 0)
                    {
                        DeleteProduct(selected);
                    }
                   if(number > 0)
                    {
                        UpdateProductQuantity(selected, number);
                    }
                   if(number < 0)
                    {
                        MessageBox.Show("Колл-во не может быть меньше нуля");
                    }
                    UpdatePage();

                }
                else
                { MessageBox.Show("Не должны быть букв и знаков!"); }
            }
        }

        private void ComboPickup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderPickup pickup = ComboPickup.SelectedValue as OrderPickup;
            PickupPointId = pickup.OrderPickupPointID;

        }
    }
}
