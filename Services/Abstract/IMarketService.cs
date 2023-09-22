﻿using MarketApp.Data.Enums;

using MarketApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Services.Abstract
{
    public interface IMarketService
    {
        public List<Sales> GetSales();
        public int AddSales(DateTime date);
        
        public int ReturnProductFromSale(int saleid,int productid, int prodamount);
        public int DeleteSale(int saleid);
        public List<Sales> GetBetweenDateOfSales(DateTime startDate, DateTime endDate); 
        public int GetByDateSales(DateOnly date);
        public List<Sales> GetByPaymentSales(decimal minprice,decimal maxprice);
       
        public int AddNewProduct(string name, decimal pricePerProduct, Categories category, int amount);
       
        
        public int UpdateProduct(int id, string name, decimal pricePerProduct, Categories category, int amount);
        public List<Product> ShowProductByCategory(Categories category);
        public List<Product> ShowProductsByPrice(decimal minprice ,decimal maxprice);
        public List<Product> ShowProductByName(string name);
        public int DeleteProduct(int id);   
        public List<Product> GetProducts();

        public List<SalesItem> GetSaleById(int id, out List<Sales> sale);
         







    }
}
