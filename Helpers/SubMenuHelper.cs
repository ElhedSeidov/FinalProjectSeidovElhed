using MarketApp.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Helpers
{
    public class SubMenuHelper
    {
        public static void DisplayProductsMenu()
        {
            int selectedOption;

            do
            {
                Console.WriteLine("1 Add Product");
                Console.WriteLine("2.");
                Console.WriteLine("3.");

                Console.WriteLine("0. Exit");
                Console.WriteLine("----------------------------");
                Console.WriteLine("Please, select an option:");

                while (!int.TryParse(Console.ReadLine(), out selectedOption))
                {
                    Console.WriteLine("Please enter valid option:");
                }

                switch (selectedOption)
                {
                    case 1:
                        MenuService.AddNewProduct();
                        break;
                    case 2:

                        break;
                    case 3:

                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }
            } while (selectedOption != 0);
        }

        public static void DisplaySalesMenu()
        {
            int selectedOption;

            do
            {
                Console.WriteLine("1.Add Sale Item ");
                Console.WriteLine("2.ShowSales ");
                Console.WriteLine("3.ShowSales by Id ");

                Console.WriteLine("0. Exit");
                Console.WriteLine("----------------------------");
                Console.WriteLine("Please, select an option:");

                while (!int.TryParse(Console.ReadLine(), out selectedOption))
                {
                    Console.WriteLine("Please enter valid option:");
                }

                switch (selectedOption)
                {
                    case 1:
                        MenuService.AddSales();
                        break;
                    case 2:
                        MenuService.ShowSales();
                        break;
                    case 3:
                        MenuService.ShowSaleById();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }
            } while (selectedOption != 0);
        }


    }

   
}
