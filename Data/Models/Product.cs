using MarketApp.Data.Common;
using MarketApp.Data.Enums;

namespace MarketApp.Data.Models
{
    public class Product : BaseModel
    {
        private static int id = 0;
        public Product()
        {
            Id = id;
            id++;
        }
        public string Name { get; set; } = null!;
        public decimal PricePerProduct { get; set; }
        public Categories Category { get; set; }
        public int Amount { get; set; }
    }
}