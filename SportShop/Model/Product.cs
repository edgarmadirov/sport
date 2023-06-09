//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SportShop.Model
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public partial class Product
    {
        public string ProductFullInfo
        {
            get
            {

                return ($"Название: {ProductName}\n" +
                    $"Артикул: {ProductArticleNumber}\n" +
                    $"Описание: {ProductDescription}"
                    );
            }
        }
        public double ProductCostWithDiscount
        {
            get
            {
                double discountCost = Convert.ToDouble(ProductCost) * ProductDiscountAmount / 100;
                return Convert.ToDouble(ProductCost) - discountCost;
            }
        }
        public string GetImage
        {
            get
            {
                return (Directory.GetCurrentDirectory() + "/images/" + ProductPhoto);
            }

        }
        public string GetDiscount
        {
            get
            {
                return (ProductDiscountAmount + " %");
            }

        }
        public string GetPrice
        {
            get
            {
                return ($"{ProductCostWithDiscount} Р.");
            }

        }
        public string GetManufacturer
        {
            get
            {
                return ("Производитель: " + Manufacturer.Name);
            }

        }
        public string ProductArticleNumber { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int ProductCategoryID { get; set; }
        public string ProductPhoto { get; set; }
        public Nullable<int> ProductManufacturerID { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public Nullable<double> ProductCost { get; set; }
        public byte ProductDiscountAmount { get; set; }
        public int ProductQuantityInStock { get; set; }
        public string ProductStatus { get; set; }
    
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
