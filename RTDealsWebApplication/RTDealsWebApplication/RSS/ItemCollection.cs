using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace RTDealsWebApplication.RSS
{
    /// <summary>
    /// rssChannelCollection instruction。
    /// </summary>
    public class ItemCollection : System.Collections.CollectionBase
    {
        public Item this[int index]
        {
            get { return ((Item)(List[index])); }
            set 
            { 
                List[index] = value;
            }
        }
        public int Add(Item item)
        {
            return List.Add(item);
        }

        public ItemCollection()
        {            
        }

    }//
}//