using MarketApp.Data.Enums;
using MarketApp.Data.Models;
using MarketApp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Services.Concrete
{


    public class MarketService : IMarketService
    {
        private List<Product> products = new();
        private List<Sales> sales = new();


        public int AddNewProduct(string name, decimal pricePerProduct, Categories category, int amount)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name can't be empty!");
            if (pricePerProduct < 0)
                throw new Exception("Price per product can't be less than 0!");
            if (amount <= 0)
                throw new Exception("Amount can't be lesser than one");
            var product = new Product
            {
                Name = name,
                PricePerProduct = pricePerProduct,
                Category = category,
                Amount = amount
            };

            products.Add(product);

            return product.Id;
        }
        public int UpdateProduct(int id)
        {
            return id;
        }
        public int DeleteProduct(int id)
        {
            if (id < 0)
                throw new Exception("Id can't be less than 0!");

            var product =products.FirstOrDefault(x => x.Id == id);

            if (product is null)
                throw new Exception($"Patient with ID:{id} was not found!");

            products.Remove(product);

            return id;
        }
        public List<Product> ShowProducts()
        {
            return products;
        }
     
        public List<Product> ShowProductByCategory(Categories category)
        {
            var productcategory=products.Where(x => x.Category == category);
            return productcategory.ToList();
        }

        public List<Product> ShowProductsByPrice(decimal minprice, decimal maxprice)
        {
            var productcategory = products.Where(x => x.PricePerProduct>=minprice && x.PricePerProduct<=maxprice );
            return productcategory.ToList();
        }

        public List<Product> ShowProductByName(string name)
        {
            var productcategory = products.Where(x => x.Name==name);
            return productcategory.ToList();
        }

        public int AddSales(decimal payment, Categories category, SalesItem salesItem, DateOnly date)
        {
            if (payment < 0)
                throw new Exception("Price per product can't be less than 0!");

            var sale = new Sales
            {
                Payment = payment,
                Category = category,
                SalesItem = salesItem,
                Date = date
            };
            sales.Add(sale);
            return sale.Id;
        }




    }


}
