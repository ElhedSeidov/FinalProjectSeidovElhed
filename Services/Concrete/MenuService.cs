using ConsoleTables;
using MarketApp.Data.Enums;
using MarketApp.Data.Models;
using MarketApp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Categories category = (Categories)Enum.Parse(typeof(Categories), Console.ReadLine()!);

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
        public static void ShowProductByCategory()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
               
            }
        }
        public static void ShowProductByPrice()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }
        public static void ShowProductByName()
        {
            try
            {

            }
            catch (Exception ex)
            {

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
                Console.WriteLine("Enter meeting's date yyyy/MM/dd:");
                var date = DateTime.ParseExact(Console.ReadLine()!, "yyyy/MM/dd", null);
                int a = marketService.AddSales(date);
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
            }

        }

    }


}
