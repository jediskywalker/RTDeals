using System;
using System.Collections.Generic;

namespace DealProcessing
{
    public class Brand
    {
        public int BrandID { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }

        public Brand()
        {
            Products = new List<Product>();
        }

        public override string ToString()
        {
            string prods = "";
            foreach (Product prod in Products)
            {
                prods += prod.ToString();
            }
            return string.Format("<Category id:{0} Name:{1} subs:{2}>; ", BrandID, Name, prods);
        }

    }

}