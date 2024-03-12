using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeComfort.Classes
{
    internal class ProductExstend
    {
        public Model.Product Product { get; set; }
        public double? PriceDiscount
        {
            get
            {
                return Product.Price - (Product.Price * Product.Discount / 100);
            }
        }

        public static List<Classes.ProductExstend> Filling(Model.HomeComfortEntities DB)
        {
            var products = DB.Product.ToList();
            var productExstend = new List<ProductExstend>();
            foreach (var item in products)
            {
                var productsItem = new ProductExstend()
                {
                    Product = item
                };
                productExstend.Add(productsItem);
            }
            return productExstend;
        }

        public static List<Classes.ProductExstend> Filling(List<Model.Product> productsList)
        {
            var productExstend = new List<ProductExstend>();
            foreach (var item in productsList)
            {
                var productsItem = new ProductExstend()
                {
                    Product = item
                };
                productExstend.Add(productsItem);
            }
            return productExstend;
        }
    }  
}
