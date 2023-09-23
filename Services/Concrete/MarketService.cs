using MarketApp.Data.Enums;
using MarketApp.Data.Models;
using MarketApp.Services.Abstract;

namespace MarketApp.Services.Concrete
{
    public class MarketService : IMarketService
    {
        private List<Product> products = new();
        private List<Sales> sales = new();
        private List<SalesItem> salesItems = new();

        public int AddNewProduct(string name, decimal pricePerProduct, Categories category, int amount)//this method  add product to List of Products 
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name can't be empty!");
            if (pricePerProduct < 0)
                throw new Exception("Price per product can't be less than 0 !");
            if (!Enum.IsDefined(typeof(Categories), category))
                throw new Exception("Please enter right value for category");
            if (amount <= 0)
                throw new Exception("Amount can't be lesser than one");
            var product = new Product
            {
                Name = name,
                PricePerProduct = pricePerProduct,//here we assign values to newly created product
                Category = category,
                Amount = amount
            };

            products.Add(product);

            return product.Id;
        }
        public int UpdateProduct(int id, string name, decimal pricePerProduct, Categories category, int amount)//in this method we update product
        {
            var prodex = products.FirstOrDefault(x => x.Id == id);//here we find product we want to update by id
            if (prodex is null)
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
            prodex.Amount = amount;   //here method assign new values

            return id;
        }
        public int DeleteProduct(int id)
        {
            if (id < 0)
                throw new Exception("Id can't be less than 0!");

            var product = products.FirstOrDefault(x => x.Id == id);//find product by id and delete it

            if (product is null)
                throw new Exception($"Product with ID:{id} was not found!");

            products.Remove(product);

            return id;
        }
        public List<Product> GetProducts()
        {
            return products;//shows full list of products we added before
        }
        public List<Product> ShowProductByCategory(Categories category)
        {
            var productcategory = products.Where(x => x.Category == category);//find product by category
            if (productcategory.Count() == 0)//if products was not found it will return an exception
                throw new Exception("Products in this category was not found");
            return productcategory.ToList();
        }
        public List<Product> ShowProductsByPrice(decimal minprice, decimal maxprice)   //show products in given price diapazone     
        {
            if (minprice > maxprice)
                throw new Exception($"Minimal price cannot be more than max price");

            var productcategory = products.Where(x => x.PricePerProduct >= minprice && x.PricePerProduct <= maxprice);//if product minprice equal to max price it will just return product with entered price
            if (productcategory.Count() == 0)
                throw new Exception("Products in this diapazone was not found");//if list is empty it will return exception

            return productcategory.ToList();
        }
        public List<Product> ShowProductByName(string name)
        {
            var productname = products.Where(x => x.Name.Contains(name));//show products which contains given name for example if we write cola it will return cola1l cola2l and etc.
            var productnameexist = products.FirstOrDefault(x => x.Name.Contains(name));

            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name can't be empty!");
            if (productnameexist is null)
                throw new Exception($"Product with mame {name} was not found!");

            return productname.ToList();
        }
        public int AddSales(DateTime date)//here we enter the date of created sale 
        {
            int selectedOption;
            TimeSpan currentTime = DateTime.Now.TimeOfDay;

            Sales sale = new Sales
            {
                Date = date + currentTime   //            here   i could actually write here Date=DateTime.Now because in real life sale date recorded by default, but i write this because i wanted to check 
            };                              //            another methods related with date of sale 
            if (sales.Count == 0)// <===start of the <<additional code>>
            {
                sale.Id = 0;//first sale id will be 0
            }
            else if (sales.Count >= 1)                    //    here i wrote an  <<additional code>> that regulates sale id , the id of first sale always should be 0 , the id another sales always should be
            {                                             //    one  more than id of previous one sale according to this if else control flow, if the of previous sale 1 the Id of next product will be 2
                sale.Id = sales[sales.Count - 1].Id + 1;  //    the created sale id always will be one more than previous sale id
            }     //<==end of the <<additional code>>
            do
            {
                Console.WriteLine("1.Add Product with and amount of it");//this method used to add products to sale
                Console.WriteLine($"2.Create Sale with ID {sale.Id}");//show sale id which will be created 
                Console.WriteLine($"0.Exit Without Creating Sale");
                Console.WriteLine("----------------------------");
                Console.WriteLine("Please, select an option:");
                while (!int.TryParse(Console.ReadLine(), out selectedOption))
                {
                    Console.WriteLine("Please enter valid option:");//this while loop prevents us from enering invalid value to selectoption input
                }
                switch (selectedOption)
                {
                    case 1:
                        AddSalesHelper1(sale);//method whic adds products and their count by chosen id ,first we should enter id after we should enter the amount of products we want to add
                        break;
                    case 2:
                        if (sale.SalesItems.Count == 0)//prevents from creating empty sale
                        {
                            throw new Exception("You could not create empty sale");
                        }
                        if (sales.Any(x => x.Id == sale.Id) == true)//prevents from creating sale with same id
                        {
                            throw new Exception("You have already created sale with this ID");
                        }
                        for (int i = 0; i < sale.SalesItems.Count; i++)//calculate the total payment of sale  by adding the product of count of saleitems multiplied to price of product in that saleitem
                        {
                            sale.Payment = sale.Payment + sale.SalesItems[i].Count * sale.SalesItems[i].Product.PricePerProduct;
                        }
                        sales.Add(sale);
                        Console.WriteLine($"Sale with ID {sale.Id}  created");
                        return sale.Id; // <=== 1 in this cases I used return instead of break because in other case it wouldn't exit from cycle            
                    case 0:
                        return sale.Id; //<===2           
                    default:
                        Console.WriteLine("No such option!");
                        break;
                }
            } while (selectedOption != 0 || selectedOption!=2);
            return sale.Id;
        }
        public List<Sales> GetSales()
        {
            return sales;//show list of sales
        }
        public int ReturnProductFromSale(int saleid, int productid, int prodamount)//this  method pull out needed product by id from needed sale in entered amount
        {
            if (string.IsNullOrEmpty(saleid.ToString()))
                throw new Exception("The Sale Id field is empty");
            if (saleid < 0)
                throw new Exception("Id can't be less than 0!");
            var saleexist = sales.FirstOrDefault(x => x.Id == saleid);
            if (saleexist is null)
                throw new Exception($"Sale with ID:{saleid} was not found!");
            if (string.IsNullOrEmpty(productid.ToString()))
                throw new Exception("The Product Id field is empty");
            if (productid < 0)
                throw new Exception("Id can't be less than 0!");
            if (saleexist.SalesItems.FirstOrDefault(x => x.Product.Id == productid) is null)
                throw new Exception($"There are no SaleItem which contain product with ID {productid} ");
            if (prodamount > saleexist.SalesItems.FirstOrDefault(x => x.Product.Id == productid).Count)//if product amount more than saleitem possess it will throw an exception
                throw new Exception($"The product count is too high , enter count which is less than {saleexist.SalesItems.FirstOrDefault(x => x.Product.Id == productid).Count} ");

            var salewithneededid = sales.FirstOrDefault(x => x.Id == saleid);
            var saleitemid = salewithneededid.SalesItems.FirstOrDefault(x => x.Product.Id == productid);

            saleitemid.Count = saleitemid.Count - prodamount;//substract from count entered product amount
            products.FirstOrDefault(x => x.Id == productid).Amount = products.FirstOrDefault(x => x.Id == productid).Amount + prodamount;// add to needed product with id of substrated product
                                                                                                                                         // the amount of substracted product from saleitem

            salewithneededid.Payment = salewithneededid.Payment - saleitemid.Product.PricePerProduct * prodamount;// calculate payment ass it did in AddSales() method
            if (saleitemid.Count == 0)//if saleitem product count is 0 remove that product 
            {
                salewithneededid.SalesItems.Remove(saleitemid);
                return productid;
            }
            else
            {
                return productid;
            }
        }
        public int DeleteSale(int saleid)//delete whole sale and return all products 
        {
            if (string.IsNullOrEmpty(saleid.ToString()))
            {
                throw new Exception("The Sale Id field is empty");
            }
            if (saleid < 0)
                throw new Exception("Id can't be less than 0!");
            var salecheck = sales.FirstOrDefault(x => x.Id == saleid);
            if (salecheck is null)
                throw new Exception($"Sale with ID:{saleid} was not found!");

            var saleexist = sales.FirstOrDefault(x => x.Id == saleid);

            products.ForEach(products => { products.Amount = products.Amount + saleexist.SalesItems.FirstOrDefault(x => x.Product.Id == products.Id).Count; }); //return all deleted product count to product list 

            sales.Remove(saleexist); //remove sale with given ID

            return saleid;
        }
        public List<Sales> GetByPaymentSales(decimal minprice, decimal maxprice)//return the sales in give diapazone of payments
        {
            if (minprice > maxprice)
                throw new Exception($"Minimal price cannot be more than max price");

            var saleprice = sales.Where(x => x.Payment >= minprice && x.Payment <= maxprice);
            return saleprice.ToList();
        }
        public List<Sales> GetBetweenDateOfSales(DateTime startDate, DateTime endDate)//return the sales in give diapazone of dates
        {
            if (startDate > endDate)
                throw new Exception("Mindate cannot me more than Maxdate");
            var salesdates = sales.Where(x => x.Date.Date >= startDate.Date && x.Date.Date <= endDate.Date).ToList();
            if (salesdates.Count == 0)
                throw new Exception("Sales in this diapazone was not found");
            return salesdates;
        }
        public List<Sales> GetSaleByDate(DateTime dateTime)//return sales with given date
        {
            var datesalesexist = sales.FirstOrDefault(x => x.Date.Date == dateTime.Date);
            if (datesalesexist == null)
                throw new Exception($"This date does not exist the min date is {sales.Min(x => x.Date)} the max date {sales.Max(x => x.Date)}  ");
            var datesales = sales.Where(x => x.Date.Date == dateTime.Date);
            return datesales.ToList();
        }
        public List<SalesItem> GetSaleById(int id, out List<Sales> saless) //return the saleitems  and return the sale by using out parameter
        {
            var salexist = sales.FirstOrDefault(x => x.Id == id);
            if (salexist is null)
                throw new Exception($"Sale with ID:{id} was not found!");
            saless = sales.Where(x => x.Id == id).ToList();
            var sale1 = sales.FirstOrDefault(x => x.Id == id);
            return sale1.SalesItems;
        }
        public void AddSalesHelper1(Sales sales)//this method is used to implement AddSalesHelper2 method without errors 
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
                throw new Exception($"The Prod with{productid} does not exist ");
            }
            if (productamount <= 0)
            {
                throw new Exception("Enter amount which is bigger than 0");
            }
            var prod0 = products.SingleOrDefault(x => x.Id == productid);
            if (prod0.Amount < productamount)
            {
                throw new Exception($"The Product with {prod0.Id} does not have {productamount} amount ,the amount which this has is {prod0.Amount}  ");
            }
            var prod = products.SingleOrDefault(x => x.Id == productid);
            AddSalesItem(productamount, prod, out SalesItem salasitemout);//creates saleitem which will be added to sale
            prod.Amount = prod.Amount - productamount;
            if (sale.SalesItems.Any(x => x.Product.Id == productid) == true)
            {
                sale.SalesItems.FirstOrDefault(x => x.Product.Id == productid).Count += productamount;// add  products to saleitem whose id is already exists in sale
                return sale.SalesItems.FirstOrDefault(x => x.Product.Id == productid).Id;
            }
            else
            {
                sale.SalesItems.Add(salasitemout);//adding saleitem to sale 
            }
            sale.SalesItems[sale.SalesItems.Count() - 1].Id = sale.SalesItems.Count() - 1;//regulates the last saleitem id the sale item id always will be equal to count of saleitem - 1
            return salasitemout.Id;
        }
        public int AddSalesItem(int count, Product product, out SalesItem salesItemout)//i added out for sales item for further operations with sales item
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
