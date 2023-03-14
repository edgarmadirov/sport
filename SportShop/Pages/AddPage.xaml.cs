using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using SportShop.Model;

namespace SportShop.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        private Product _currentGood = new Product();
        private static string _currentDirectory = Directory.GetCurrentDirectory() + @"\images\";
        private string _filePath = null;
        private string _photoName = null;
        public AddPage(Product selectedGood)
        {
            InitializeComponent();
            if (selectedGood != null)
            {
                List<Product> products = new List<Product>();
            }
            CmbCategory.ItemsSource = App.db.ProductCategories.ToList();
            CmbMagaz.ItemsSource = App.db.Suppliers.ToList();
            CmbProiz.ItemsSource = App.db.Manufacturers.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_filePath != null)
                {
                    string photo = ChangePhotoName();
                    string desc = _currentDirectory + photo;
                    File.Copy(_filePath, desc);
                    _currentGood.ProductPhoto = photo;

                    _currentGood.ProductArticleNumber = TBArticul.Text;
                    _currentGood.ProductName = TBNAME.Text;
                    _currentGood.ProductCategoryID = (CmbCategory.SelectedItem as ProductCategory).ProductCategoryID;
                    _currentGood.SupplierID = (CmbMagaz.SelectedItem as Supplier).SupplierID;
                    _currentGood.ProductManufacturerID = (CmbProiz.SelectedItem as Manufacturer).ManufacturerID;
                    _currentGood.ProductCost = (float)Convert.ToDouble(TBCost.Text);
                    _currentGood.ProductQuantityInStock = (int)Convert.ToDouble(TBKolvo.Text);
                    _currentGood.ProductDiscountAmount = (byte)Convert.ToDouble(TBSkida.Text);
                    _currentGood.ProductDescription = TBOpis.Text;

                    App.db.Products.Add(_currentGood);
                    App.db.SaveChanges();
                    MessageBox.Show("save");
                    NavigationService.Navigate(new Pages.AssortimentsPage());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CmbProiz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    
        string ChangePhotoName()
        {
            string x = _currentDirectory + _photoName;
            string photoname = _photoName;
            int i = 0;
            if (File.Exists(x))
            {
                while (File.Exists(x))
                {
                    i++;
                    x = _currentDirectory + i.ToString() + photoname;
                }
                photoname = i.ToString() + photoname;
            }
            return photoname;
        }
        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {               
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "JPEG Files(*.jpeg)|*.jpeg|PNG Files(*.png)|*.png|JPG Files(*.jpg)| *.jpg|GIF Files(*.gif)|*.gif";
                if (op.ShowDialog() == true)
                {
                    FileInfo fileInfo = new FileInfo(op.FileName);
                    if (fileInfo.Length > (1024 * 1024 * 2))
                    {
                        throw new Exception("Размер файла должен быть меньше 2Мб");
                    }
                    ImagePhoto.Source = new BitmapImage(new Uri(op.FileName));
                    _photoName = op.SafeFileName;
                    _filePath = op.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                _filePath = null;
            }
        }

        private void CmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CmbMagaz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
