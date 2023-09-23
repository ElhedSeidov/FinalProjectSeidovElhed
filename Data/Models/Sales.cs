using MarketApp.Data.Common;


namespace MarketApp.Data.Models
{
    public class Sales : BaseModel
    {
        private static int id = 0;
        public Sales()
        {
            Id = id;
            id++;
        }    
        public decimal Payment { get; set; }      
        public List<SalesItem> SalesItems { get; set; } = new();
        public DateTime Date { get; set; }
    }
}
