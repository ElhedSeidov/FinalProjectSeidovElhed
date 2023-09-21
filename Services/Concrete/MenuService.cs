using ConsoleTables;
using MarketApp.Data.Enums;
using MarketApp.Data.Models;
using MarketApp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Services.Concrete
{
    public class MenuService
    {
        private static IMarketService marketService = new MarketService();
        public static void AddNewProduct()
        {
            try
            {
                Console.WriteLine("Enter product  name:");
                string fullname = Console.ReadLine()!;

                Console.WriteLine("Enter Product Category:");
                string a= Console.ReadLine()!;
                if (int.TryParse(a, out _))
                {
                    throw new Exception("Enter Right Value");
                }
                Categories category = (Categories)Enum.Parse(typeof(Categories), a);

                Console.WriteLine("Enter Product Price:");
                decimal productPrice = decimal.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter Product Amount:");
                int amount = int.Parse(Console.ReadLine()!);

                int id = marketService.AddNewProduct(fullname,productPrice,category ,amount);

                Console.WriteLine($"Product with ID:{id} was created!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void UpdateProduct()
        {
            try
            {
                Console.WriteLine("Enter Product ID:");
                int id = int.Parse(Console.ReadLine()!);


                Console.WriteLine("Enter product  name:");
                string fullname = Console.ReadLine()!;

                Console.WriteLine("Enter Product Category:");
                string a = Console.ReadLine()!;
                if (int.TryParse(a, out _))
                {
                    throw new Exception("Enter Right Value");
                }
                Categories category = (Categories)Enum.Parse(typeof(Categories), a);

                Console.WriteLine("Enter Product Price:");
                decimal productPrice = decimal.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter Product Amount:");
                int amount = int.Parse(Console.ReadLine()!);

                int idupd = marketService.UpdateProduct(id, fullname, productPrice, category, amount);

                Console.WriteLine($"Product with ID:{idupd} was updated!");
            }

            catch (Exception ex)

            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
        public static void DeleteProduct()
        {
            try
            {
                Console.WriteLine("Enter Product ID you want to delete:");
                int id = int.Parse(Console.ReadLine()!);

                int a = marketService.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
        
        public static void ShowProductByCategory()
        {
            try
            {
                Console.WriteLine("Enter Product Category:");
                string a = Console.ReadLine()!;
                if (int.TryParse(a, out _))
                {
                    throw new Exception("Enter Right Value");
                }
                Categories category = (Categories)Enum.Parse(typeof(Categories), a);

                var table = new ConsoleTable("Category","ID","Name","pricePerProducts","Amounts");

                foreach (var product in marketService.ShowProductByCategory(category)) 
                {
                    table.AddRow(product.Category,product.Id,product.Name,product.PricePerProduct,product.Amount);
                }

                table.Write();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public static void ShowProductByPrice()
        {
            try
            {
                Console.WriteLine("Enter Product minimal Price:");
                decimal minPrice = decimal.Parse(Console.ReadLine()!);
                Console.WriteLine("Enter Product maximal Price:");
                decimal maxPrice = decimal.Parse(Console.ReadLine()!);

                marketService.ShowProductsByPrice(minPrice,maxPrice);   
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public static void ShowProductByName()
        {
            try
            {
                string str = Console.ReadLine()!;
                var table = new ConsoleTable("Name","ID", "price Per Product ", "Category", "Amount");

                foreach (var product in marketService.ShowProductByName(str))
                {
                    table.AddRow( product.Id,product.Name, product.PricePerProduct, product.Category, product.Amount);
                }

                table.Write();


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

     

            public static void ShowProducts()
        {
            var table = new ConsoleTable("id", "Name","price Per Product ","Category","Amount");

            foreach (var product in marketService.GetProducts())
            {
                table.AddRow(product.Id,product.Name,product.PricePerProduct,product.Category,product.Amount);
            }
            table.Write();
        }
        public static void AddSales()
        {
            try
            {
                Console.WriteLine("Enter  sale's date yyyy/MM/dd:");
                var date = DateTime.ParseExact(Console.ReadLine()!, "yyyy/MM/dd", null);
                int a = marketService.AddSales(date); 
               

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static void ShowSales()
        {
            var table = new ConsoleTable("id","Payment","Date");

            foreach (var sale in marketService.GetSales()) 
            {
                table.AddRow(sale.Id,sale.Payment,sale.Date);
            }

            table.Write();
        }

        public static void ShowSaleById ()
        {

            try
            {
                Console.WriteLine("Enter Sale ID to show salesitems ");
                int id = int.Parse(Console.ReadLine()!);


                var a = marketService.GetSaleById(id, out List<Sales> sale);

                var table1 = new ConsoleTable("id", "Payment", "Date");
                foreach (var sales in sale)
                {
                    table1.AddRow(sales.Id, sales.Payment, sales.Date);
                }

                table1.Write();
                Console.WriteLine("=================================================");


                Console.WriteLine("Show Sale Items :");

                var table2 = new ConsoleTable("id", "ProdName", "Count");

                foreach (var saleitem in a)
                {
                    table2.AddRow(saleitem.Id, saleitem.Product.Name, saleitem.Count);
                }

                table2.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        public static void ReturnProductFromSale()
        {
            try
            {
                Console.WriteLine("Enter Sale ID  ");
                int id = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter Produc ID  ");
                int prodid = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Enter Produc ID  ");
                int prodamount = int.Parse(Console.ReadLine()!);

                marketService.ReturnProductFromSale(id, prodid, prodamount);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }






        }

    }


}
