using MarketApp.Data.Enums;
using MarketApp.Data.Models;
using MarketApp.Services.Abstract;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                throw new Exception("Price per product can't be less than 0 !");
            if (!Enum.IsDefined(typeof(Categories), category) )
                throw new Exception("Please enter right value for category");
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
        public int UpdateProduct(int id,string name, decimal pricePerProduct, Categories category, int amount)
        {   
            
            
            var prodex = products.FirstOrDefault(x => x.Id == id);
            if (prodex == null)
                throw new Exception($"Product with ID:{id} was not found!");


            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name can't be empty!");
            if (pricePerProduct < 0)
                throw new Exception("Price per product can't be less than 0 !");
            if (!Enum.IsDefined(typeof(Categories), category))
                throw new Exception("Please enter right value for category");
            if (amount <= 0)
                throw new Exception("Amount can't be lesser than one");
            

            prodex.Name = name;
            prodex.PricePerProduct = pricePerProduct;  
            prodex.Category = category;
            prodex.Amount = amount;
        







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
        public List<Product> GetProducts()
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
            int selectedOption;


            Sales sale = new Sales
            {
                Date = date,
            };

            do
            {
                Console.WriteLine("1.Add Product with and amount of it");
                Console.WriteLine($"2.Create Sale with ID {sale.Id}");
                Console.WriteLine($"0.Exit without creating sale {sale.Id} ");
                Console.WriteLine("----------------------------");
                Console.WriteLine("Please, select an option:");
                while (!int.TryParse(Console.ReadLine(), out selectedOption))
                {
                    Console.WriteLine("Please enter valid option:");
                }

                switch (selectedOption)
                {

                    case 1:
                        AddSalesHelper1(sale);
                        break;
                    case 2:
                        if (sale.SalesItems.Count == 0)
                        {
                            
                            throw new Exception("You could not create empty sale");
                        }
                        for (int i = 0; i < sale.SalesItems.Count; i++)
                        {
                            sale.Payment = sale.Payment + sale.SalesItems[i].Count * sale.SalesItems[i].Product.PricePerProduct;
                        }
                        sales.Add(sale);
                        Console.WriteLine($"Sale with ID {sale.Id}  ");
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

       
       
        public List<Sales> GetSales()
        {
            return sales;
        }
        public int ReturnProductFromSale(int saleid,int productid, int prodamount)
        { 
             if (string.IsNullOrEmpty(saleid.ToString()))       
                throw new Exception("The Sale Id field is empty");        
            if (saleid < 0)
                throw new Exception("Id can't be less than 0!");   
            var saleexist= sales.FirstOrDefault(x => x.Id == saleid);
            if (saleexist is null)
                throw new Exception($"Sale with ID:{saleid} was not found!");
            if (string.IsNullOrEmpty(productid.ToString()))
                throw new Exception("The Product Id field is empty");
            if (productid < 0)
                throw new Exception("Id can't be less than 0!");


            var productamountadd = products.FirstOrDefault(x => x.Id == productid).Amount; 
            var productexist =saleexist.SalesItems.FirstOrDefault(x => x.Product.Id==productid);
            var productexistcount = productexist.Count;
            productexistcount = productexistcount - prodamount;
            productamountadd = productamountadd + prodamount;
            if (productexistcount == 0)
            {
                saleexist.SalesItems.Remove(productexist);
            }









            return 0;
        }
        public int DeleteSale(int saleid)
        {
            if (string.IsNullOrEmpty(saleid.ToString()))
            {
                throw new Exception("The Sale Id field is empty");
            }
            if (saleid < 0)
                throw new Exception("Id can't be less than 0!");
            var saleexist = sales.FirstOrDefault(x => x.Id == saleid);
            if (saleexist is null)
                throw new Exception($"Sale with ID:{saleid} was not found!");
            for (int i = 0;i <saleexist.SalesItems.Count;i++)
            {
                products.FirstOrDefault(x => x.Id == i).Amount += saleexist.SalesItems[i].Count;
                products.Add(saleexist.SalesItems[i].Product);               
            }
            sales.Remove(saleexist);

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
        
     

        public List<SalesItem> GetSaleById(int id,out List<Sales> saless)
        {
            var salexist = sales.FirstOrDefault(x => x.Id == id);
            if (salexist is null)
                throw new Exception($"Sale with ID:{id} was not found!");
            saless = sales.Where(x => x.Id == id).ToList();         
            var sale1 = sales.FirstOrDefault(x => x.Id == id);
            return sale1.SalesItems;
        }

        public void AddSalesHelper1(Sales sales)
        {
            try
            {
                Console.WriteLine("Enter the product id which you want to add");
                int productid = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Enter the product count you want to add");
                int productcount = int.Parse(Console.ReadLine()!);

                int a = AddSalesHelper2(sales, productid, productcount);
                Console.WriteLine($"The Product with {productid} which's count is {productcount} , to saleitem with ID {a}  which in sale {sales.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }              
        }


        public int AddSalesHelper2(Sales sale, int productid, int productamount)
        {
            var prodany = products.Any(x => x.Id == productid);
            if (prodany == false)
            {
                sale.Id = sale.Id - 1;
                throw new Exception($"The Prod with{productid} does not exist ");
            }                        
            if (productamount <= 0)
            {
                sale.Id = sale.Id - 1;
                throw new Exception("Enter amount which is bigger than 0");
            }
            var prod0 = products.SingleOrDefault(x => x.Id == productid);
            if (prod0.Amount < productamount)
            {
                sale.Id = sale.Id - 1;
                throw new Exception($"The Product with {prod0.Id} does not have {productamount} amount ,the amount which this has is {prod0.Amount}  ");
            }
            var prod = products.SingleOrDefault(x => x.Id == productid);
            AddSalesItem(productamount, prod, out SalesItem salasitemout);

            prod.Amount = prod.Amount - productamount;
            if (sale.SalesItems.Any(x => x.Product.Id == productid) == true)
            {
                sale.SalesItems.FirstOrDefault(x => x.Product.Id == productid).Count += productamount;
            }
            else
            {
                sale.SalesItems.Add(salasitemout);
            }

            sale.SalesItems[sale.SalesItems.Count() - 1].Id = sale.SalesItems.Count() - 1;

            return salasitemout.Id;
        }

        public int AddSalesItem(int count, Product product, out SalesItem salesItemout)
        {
            var saleitem = new SalesItem
            {
                Product = product,
                Count = count

            };
            salesItemout = saleitem;

            return saleitem.Id;

        }
    }


}
