﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Warehouse.Models.DTO
{
    public class CategoryDTO:BaseDTO
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
    }
}