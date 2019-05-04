using MVC_Warehouse.DAL.ORM.Entity;
using MVC_Warehouse.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Warehouse.Models.VM
{
    public class ProductVM
    {
        public ProductVM()
        {
            Categories = new List<Category>();
            Product = new ProductDTO();
        }

        public List<Category> Categories { get; set; }
        public ProductDTO Product { get; set; }
    }
}