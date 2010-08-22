using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RTDealsWebApplication.RSS
{
  
        /// <summary>
        /// channel 
        /// </summary>
        [Serializable()]
        public class Channel
        {
            private string _title;
            private string _link;
            private string _description;
            private ItemCollection items = new ItemCollection();

            #region property
            /// <summary>
            /// title
            /// </summary>
            public string title
            {
                get { return _title; }
                set { _title = value.ToString(); }
            }
            /// <summary>
            /// link
            /// </summary>
            public string link
            {
                get { return _link; }
                set { _link = value.ToString(); }
            }
            /// <summary>
            /// description
            /// </summary>
            public string description
            {
                get { return _description; }
                set { _description = value.ToString(); }
            }
            public ItemCollection Items
            {
                get { return items; }
            }
            #endregion

            public Channel() { }


 
    }//   
}