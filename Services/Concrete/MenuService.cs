using MarketApp.Data.Enums;
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
        public static void AddSales()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }



    }


}
