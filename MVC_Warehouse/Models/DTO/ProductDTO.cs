using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Warehouse.Models.DTO
{
    public class ProductDTO:BaseDTO
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitInStock { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}