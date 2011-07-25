using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.RSS
{


    /// <summary>
    /// rssItem instruction。
    /// </summary>
    public class Item
    {
        private string _title;
        private string _link;
        private string _description;
        private string _pubDate;

        #region Property

        /// <summary>
        /// Time
        /// </summary>
        public string title
        {
            get { return _title; }
            set { _title = value.ToString(); }
        }
        /// <summary>
        /// Link
        /// </summary>
        public string link
        {
            get { return _link; }
            set { _link = value.ToString(); }
        }
        /// <summary>
        /// Description
        /// </summary>
        public string description
        {
            get { return _description; }
            set { _description = value.ToString(); }
        }
        /// <summary>
        /// Channel Publication date
        /// </summary>
        public string pubDate
        {
            get { return _pubDate; }
            set { _pubDate = C_Date(value); }
        }

        #endregion

        public Item() { }

        private string C_Date(string input)
        {
            System.DateTime dt;
            try
            {
                dt = Convert.ToDateTime(input);
            }
            catch
            {
                dt = System.DateTime.Now;
            }
            return dt.ToString();
        }

    }//
}//&nbsp;

