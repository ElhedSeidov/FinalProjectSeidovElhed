using MarketApp.Helpers;

namespace MarketApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int selectedOption;
            Console.WriteLine("Welcome to Market!");

            do
            {
                Console.WriteLine("1. For manging Products");
                Console.WriteLine("2. For managing Sales");
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
                        SubMenuHelper.DisplayProductsMenu();
                        break;
                    case 2:
                        SubMenuHelper.DisplaySalesMenu();
                        break;
                    case 0:
                        Console.WriteLine("Bye!");
                        break;
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }
            } while (selectedOption != 0);
        }
    }
}