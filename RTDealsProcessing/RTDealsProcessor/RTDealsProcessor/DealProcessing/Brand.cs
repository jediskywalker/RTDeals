using System;
using System.Collections.Generic;

namespace DealProcessing
{
    public class Brand
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public bool IsSpecial { get; set; }

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
            return string.Format("<Category id:{0} Name:{1} Pri:{2} Spcl:{3} subs:{4}>; ", CategoryID, Name, Priority, IsSpecial, prods);
        }

    }

}