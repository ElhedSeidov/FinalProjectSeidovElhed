using MarketApp.Data.Enums;

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
        
        public int ReturnProductFromSale(int productid, int prodamount);
        public int ReturnWholeSale(int saleid);
        public int GetBetweenDateOfSales(DateOnly startDate, DateOnly endDate); 
        public int GetByDateSales(DateOnly date);
        public int GetByPaymentSales(decimal payment);
        public int GetSalesById(int id);
        public int AddNewProduct(string name, decimal pricePerProduct, Categories category, int amount);
       
        
        public int UpdateProduct(int id);
        public List<Product> ShowProductByCategory(Categories category);
        public List<Product> ShowProductsByPrice(decimal minprice ,decimal maxprice);
        public List<Product> ShowProductByName(string name);

        
        
        
        
        

    }
}
