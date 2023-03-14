using SportShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for AssortimentsPage.xaml
    /// </summary>
    public partial class AssortimentsPage : Page
    {
        private int _countProduct = 0;
        private int _countProductWithFilter = 0;
        public AssortimentsPage()
        {
            InitializeComponent();
            var products = App.db.Products.ToList();
            _countProduct = products.Count;
            _countProductWithFilter = _countProduct;
            UpdateCountProdutsText();
            AssortimentList.ItemsSource = products;
        }
        
        private void UpdateData()
        {
            var currentGoods = App.db.Products.ToList();
            currentGoods = currentGoods.Where(p => p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            if (ComboDiscount.SelectedIndex > 0)
            {
                if (ComboDiscount.SelectedIndex == 1)
                    currentGoods = currentGoods.Where(p => p.ProductDiscountAmount >= 0 && p.ProductDiscountAmount < 10).ToList();
                if (ComboDiscount.SelectedIndex == 2)
                    currentGoods = currentGoods.Where(p => p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount < 15).ToList();
                if (ComboDiscount.SelectedIndex == 3)
                    currentGoods = currentGoods.Where(p => p.ProductDiscountAmount >= 15).ToList();
            }
            if (ComboSort.SelectedIndex >= 0)
            {
                if (ComboSort.SelectedIndex == 0)
                    currentGoods = currentGoods.OrderBy(p => p.ProductCostWithDiscount).ToList();
                if (ComboSort.SelectedIndex == 1)
                    currentGoods = currentGoods.OrderByDescending(p => p.ProductCostWithDiscount).ToList();
            }
            _countProductWithFilter = currentGoods.Count;
            UpdateCountProdutsText();
            AssortimentList.ItemsSource = currentGoods;
        }

        private void UpdateCountProdutsText ()
        {
            CountProducts.Text = $"Колл-во записей: {_countProductWithFilter} из {_countProduct}";
        }
        private Order OrderCheck()
        {
            var orders = App.db.Orders;
            Order order = orders.FirstOrDefault(el => el.UserID == cfg.AuthUser.UserID && el.OrderStatusID == 1);
            return order;
        }
        private int OrderAdd()
        {
            Random random = new Random();
            Order newOrder = new Order { OrderStatusID = 1, OrderCreate = DateTime.Now, OrderDeliveryDate = DateTime.Now.AddDays(3), OrderPickupPoint = 5, OrderPickupCode = Convert.ToString(random.Next(0, 10000)), UserID = cfg.AuthUser.UserID };
            App.db.Orders.Add(newOrder);
            App.db.SaveChanges();
            Order order = OrderCheck();
            if(order == null)
            {
                return 0;
            }
            return order.OrderID;
        }
        private void ProductFromOrderAdd(int orderId, Product newproduct)
        {

            OrderProduct orderProduct = App.db.OrderProducts.FirstOrDefault(el => el.OrderID == orderId && el.ProductArticleNumber == newproduct.ProductArticleNumber);
            if(orderProduct == null)
            {
                App.db.OrderProducts.Add(new OrderProduct { OrderID = orderId, ProductArticleNumber = newproduct.ProductArticleNumber, Count = 1 });
            }
            else
            {
                orderProduct.Count++;
            }
            App.db.SaveChanges();
        }

       
        private void OpenCardAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cfg.IsAuth)
            {
                Order order = OrderCheck();
                Product selectedProduct = AssortimentList.SelectedItems[0] as Product;
                if (order == null)
                {
                    int orderid = OrderAdd();
                    ProductFromOrderAdd(orderid, selectedProduct);
                }
                else
                {
                    ProductFromOrderAdd(order.OrderID, selectedProduct);
                }
            }
            else
            {
                MessageBox.Show("Вы не авторизированы");
            }
        }

        private void ComboDiscount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.AddPage(null));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("удалить?", "уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) ;
            {
                var CurrentUser = AssortimentList.SelectedItem as Product;
                App.db.Products.Remove(CurrentUser);
                App.db.SaveChanges();

                AssortimentList.ItemsSource = App.db.Products.ToList();
                MessageBox.Show("Успешно", "Удалено");
            }
        }
    }
}
