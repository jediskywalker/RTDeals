﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTDealsWebApplication.Models
{
    public class CategoryModel
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }


        public string GetContentFillerText()
        {
            return "Hello";
            


        }

    }
}