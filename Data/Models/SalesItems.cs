using MarketApp.Data.Common;


namespace MarketApp.Data.Models
{
    public class SalesItem:BaseModel
    {
        private static int id = 0;
        public SalesItem()
        {
            Id = id;
            id++;
        }
        public Product Product { get; set; } = null!;
        public int Count { get; set; }
    }
}
