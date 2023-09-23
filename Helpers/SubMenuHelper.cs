using MarketApp.Services.Concrete;


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
                Console.WriteLine("2.Update Product");
                Console.WriteLine("3.Delete product ");
                Console.WriteLine("4.Show Products");
                Console.WriteLine("5.Show products by category");
                Console.WriteLine("6.Show products by name");
                Console.WriteLine("7.Show products by price Range");

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
                        MenuService.UpdateProduct();
                        break;
                    case 3:
                        MenuService.DeleteProduct();                        
                        break;
                    case 4:
                        MenuService.ShowProducts();
                        break;
                    case 5:
                        MenuService.ShowProductByCategory();
                        break;
                    case 6:
                       MenuService.ShowProductByName();
                        break;
                    case 7:
                        MenuService.ShowProductByPrice();
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
                Console.WriteLine("1.Add Sale ");
                Console.WriteLine("2.Delete Whole Sale");
                Console.WriteLine("3.Return Product From Sale");
                Console.WriteLine("4.ShowSales ");
                Console.WriteLine("5.ShowSales by specific Id ");
                Console.WriteLine("6.Show Sale by specific Date");
                Console.WriteLine("7.Show sale between minimum and maximum  Dates");
                Console.WriteLine("8.Show Sales between Minimal Payment and Maximal Payment");

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
                        MenuService.DeleteSale();
                       break;
                    case 3:
                        MenuService.ReturnProductFromSale();
                        break;
                    case 4:
                         MenuService.ShowSales();
                        break;
                    case 5:
                        MenuService.ShowSaleById();
                        break;
                    case 6:
                        MenuService.ShowSaleByDate();
                        break;
                    case 7:
                        MenuService.ShowBetweeMinDateandMaxDate();
                        break;
                    case 8:
                        MenuService.ShowAmongPaymentSales();
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
