using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.RSS
{

    /// <summary>
    /// rssChannelCollection instruction。
    /// </summary>
    public class ChannelCollection : System.Collections.CollectionBase
    {
        public Channel this[int index]
        {
            get
            {
                return ((Channel)(List[index]));
            }
            set
            {
                List[index] = value;
            }
        }

        public int Add(Channel item)
        {
            return List.Add(item);
        }


        public ChannelCollection()
        {
        }


    }


}