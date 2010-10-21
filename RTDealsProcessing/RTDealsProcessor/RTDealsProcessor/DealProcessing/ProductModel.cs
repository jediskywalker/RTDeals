using System;

namespace DealProcessing
{
    public class ProductModel
    {
        public int ModelID { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("<ProductModel id:{0} name:{1}>; ", ModelID, Name);
        }
    }

}