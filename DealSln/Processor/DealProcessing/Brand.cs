using System;
using System.Collections.Generic;

namespace DealProcessing
{
    public class Brand
    {
        public int BrandID { get; set; }
        public string Name { get; set; }
        public string Aliases { get; set; }
        public int Accuracy { get; set; }

        public List<Product> MyProducts { get; set; }

        public Brand()
        {
            MyProducts = new List<Product>();
        }

        public override string ToString()
        {
            string prods = "";
            foreach (Product prod in MyProducts)
            {
                prods += prod.ToString();
            }
            return string.Format("<Category id:{0} Name:{1} subs:{2}>; ", BrandID, Name, prods);
        }

    }

}