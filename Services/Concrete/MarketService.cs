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
        private List<SalesItem> salesItems = new();

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

            var product = products.FirstOrDefault(x => x.Id == id);

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
            var productcategory = products.Where(x => x.Category == category);
            return productcategory.ToList();
        }

        public List<Product> ShowProductsByPrice(decimal minprice, decimal maxprice)
        {
            var productcategory = products.Where(x => x.PricePerProduct >= minprice && x.PricePerProduct <= maxprice);
            return productcategory.ToList();
        }

        public List<Product> ShowProductByName(string name)
        {
            var productcategory = products.Where(x => x.Name == name);
            return productcategory.ToList();
        }

        public int AddSales(DateTime date)
        {


            var sale = new Sales
            {

                Date = date
            };


            int selectedOption;
            do
            {
                Console.WriteLine("1. Add Product with and amount of it");
                Console.WriteLine("2. Create Sale");
                Console.WriteLine("0. Exit  ");
                Console.WriteLine("----------------------------");
                Console.WriteLine("Please, select an option:");

                while (!int.TryParse(Console.ReadLine(), out selectedOption))
                {
                    Console.WriteLine("Please enter valid option:");
                };

                switch (selectedOption)
                {
                    case 1:
                        Console.WriteLine("Enter Product id");
                        string a = Console.ReadLine()!;
                        if (string.IsNullOrEmpty(a))
                        {
                            throw new Exception("The Prod Id field is empty");
                        }

                        int prodid = int.Parse(a);

                        var prodany = products.Any(x => x.Id == prodid);
                        if (prodany == false)
                        {
                            throw new Exception($"The Prod with{prodid} does not exist ");
                        }
                        Console.WriteLine("Enter Product amount");
                        string b = Console.ReadLine()!;
                        if (string.IsNullOrEmpty(b))
                        {
                            throw new Exception("The Prod Count field is empty");
                        }
                        int count = int.Parse(b);
                        if(count == 0)
                        {
                            throw new Exception("Enter amount which is bigger than 0");
                        }
                        var prod0 = products.SingleOrDefault(x => x.Id == prodid);
                        if (prod0.Amount < count)
                        {
                            throw new Exception($"The Product with {prod0.Id} does not have {count} amount ,the amount which this has is {prod0.Amount}  ");
                        }



                        
                        var prod = products.SingleOrDefault(x => x.Id == prodid);
                        AddSalesItem(count, prod, out SalesItem salasitemout);
                        
                        sale.SalesItems.Add(salasitemout);

                        sale.SalesItems[sale.SalesItems.Count() - 1].Id = sale.SalesItems.Count() - 1;
                        Console.WriteLine($"The product with id {prodid}  in amount {salasitemout.Count}to saleitem with id {salasitemout.Id} which in sale with Id {sale.Id} ");
                        break;

                    case 2: 
                        sales.Add(sale);

                        break;
                    case 0:

                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }
            } while (selectedOption != 0);
            
          
            return sale.Id;
        }

        public int AddSalesItem(int count,Product product ,out SalesItem salesItemout)
        {
            var saleitem = new SalesItem
            {
                Product = product,
                Count = count
                
            };
            salesItemout = saleitem;

            return saleitem.Id;
       
        }
       
        public List<Sales> GetSales()
        {
            return sales;
        }
        public int ReturnProductFromSale(int prodid,int prodamount)
        { 
            return 0;
        }
        public int DeleteSale(int saleid)
        {
            if (saleid < 0)
                throw new Exception("Id can't be less than 0!");

            var product = sales.FirstOrDefault(x => x.Id == saleid);

            if (product is null)
                throw new Exception($"Patient with ID:{saleid} was not found!");

            sales.Remove(product);

            return saleid;
        }
        public int GetBetweenDateOfSales(DateOnly startDate, DateOnly endDate)
        {
            return 0;
        }
        public int GetByDateSales(DateOnly date)
        {
            return 0;
        }
        public int GetByPaymentSales(decimal payment)
        { 
            return 0;
        }
        public int GetSalesById(int id)
        {
            return 0;
        }

        public int ReturnWholeSale(int saleid)
        { return 0; }


    }


}
