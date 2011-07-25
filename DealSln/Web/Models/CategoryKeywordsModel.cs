using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CategoryKeywordsModel
    {
        public int CategoryKeywordID { get; set; }
        public int CategoryID { get; set; }
        public string Keyword { get; set; }
    }
}