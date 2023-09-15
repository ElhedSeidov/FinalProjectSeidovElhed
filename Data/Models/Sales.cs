using MarketApp.Data.Common;
using MarketApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Categories Category { get; set; }
        public SalesItem SalesItem { get; set; } = null!;

    }
}
