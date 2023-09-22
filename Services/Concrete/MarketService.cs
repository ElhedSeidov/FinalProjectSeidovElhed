﻿using MarketApp.Data.Enums;
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
            if(minprice>maxprice)
                throw new Exception($"Minimal price cannot be more than max price");


            var productcategory = products.Where(x => x.PricePerProduct >= minprice && x.PricePerProduct <= maxprice);
            
            return productcategory.ToList();
        }

        public List<Product> ShowProductByName(string name)
        {
            var productname = products.Where(x=>x.Name.Contains(name));
            var productnameexist = products.FirstOrDefault(x => x.Name.Contains(name));

            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Name can't be empty!");
            if (productnameexist is null)            
                throw new Exception($"Product with mame {name} was not found!");

            return productname.ToList();
        }

        public int AddSales(DateTime date)
        {
            int selectedOption;
            TimeSpan currentTime = DateTime.Now.TimeOfDay;

            Sales sale = new Sales
            {
                
                Date = date + currentTime
            };


            if( sales.Count==0)
            {
                sale.Id = 0;
            }
            if (sales.Count >= 1)
            {
                sale.Id = sales[sales.Count - 1].Id + 1;
            }
             
             bool saleidregulator = true;
            if(saleidregulator)
            do
            {
                Console.WriteLine("1.Add Product with and amount of it");
                Console.WriteLine($"2.Create Sale with ID {sale.Id}");
                Console.WriteLine($"0.Exit ");
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
                      
                        if (sale.SalesItems.Count == 0 )
                        {                           
                            throw new Exception("You could not create empty sale");
                        }
                        if (sales.Any(x=>x.Id==sale.Id)==true)
                        {
                                throw new Exception("You have already created sale with this ID");
                        }
                        for (int i = 0; i < sale.SalesItems.Count; i++)
                        {
                            sale.Payment = sale.Payment + sale.SalesItems[i].Count * sale.SalesItems[i].Product.PricePerProduct;
                        }
                            
                        sales.Add(sale);
                        Console.WriteLine($"Sale with ID {sale.Id}  created");
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
            if (saleexist.SalesItems.FirstOrDefault(x=>x.Product.Id==productid) is null)
                throw new Exception($"There are no SaleItem witch contain product with ID {productid} ");
            if (prodamount>saleexist.SalesItems.FirstOrDefault(x=>x.Product.Id==productid).Count )
                throw new Exception($"The product count is too high , enter count which is less than {saleexist.SalesItems.FirstOrDefault(x => x.Product.Id == productid).Count} ");

            var salewithneededid=sales.FirstOrDefault(x=>x.Id==saleid);
            var saleitemid = salewithneededid.SalesItems.FirstOrDefault(x=>x.Product.Id == productid);

            saleitemid.Count = saleitemid.Count - prodamount;
            products.FirstOrDefault(x => x.Id == productid).Amount = products.FirstOrDefault(x => x.Id == productid).Amount + prodamount;
            if(saleitemid.Count ==0)
            {
                salewithneededid.SalesItems.Remove(saleitemid);
                return productid;
            }
            else
            {
                return productid;
            }
            
        }
        public int DeleteSale(int saleid)
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

            var saleexist=sales.FirstOrDefault(x => x.Id == saleid);


            products.ForEach(products => { products.Amount = products.Amount + saleexist.SalesItems.FirstOrDefault(x => x.Product.Id == products.Id).Count; });
           
            sales.Remove(saleexist); 

            return saleid;
        }
      
        public int GetByDateSales(DateOnly date)
        {
            return 0;
        }
        public List<Sales> GetByPaymentSales(decimal minprice, decimal maxprice)
        {
            if (minprice > maxprice)
                throw new Exception($"Minimal price cannot be more than max price");


            var saleprice = sales.Where(x => x.Payment >= minprice && x.Payment <= maxprice);

            return saleprice.ToList();
        }

        public List<Sales> GetBetweenDateOfSales(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new Exception("Mindate cannot me more than Maxdate");
            var salesdates=sales.Where(x=>x.Date >= startDate && x.Date <= endDate).ToList(); 
            
            return salesdates;
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
            AddSalesItem(productamount, prod, out SalesItem salasitemout);
            prod.Amount = prod.Amount - productamount;
            if (sale.SalesItems.Any(x => x.Product.Id == productid) == true)
            {
                sale.SalesItems.FirstOrDefault(x => x.Product.Id == productid).Count += productamount;
                return sale.SalesItems.FirstOrDefault(x => x.Product.Id == productid).Id;
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
