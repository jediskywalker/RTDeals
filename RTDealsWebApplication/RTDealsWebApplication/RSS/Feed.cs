using System;
using System.Xml;
using System.IO;
using System.Net;
using System.Text;

namespace RTDealsWebApplication.RSS
{
    /// <summary>
    /// rssFeed instruction。
    /// </summary>
    public class Feed
    {
        private string _url;
        private System.DateTime _lastModified;
        private System.DateTime _lastRssDate;
        private Channel channel = new Channel();

        #region public property
        public string url
        {
            get { return _url; }
            set { _url = value; }
        }
        public System.DateTime lastModified
        {
            get { return _lastModified; }
        }
        public System.DateTime lstRssDate
        {
            set { _lastRssDate = value; }
        }
        public Channel Channel
        {
            get { return channel; }
        }
        #endregion


        public Feed()
        {
        }

        public Feed(string url, System.DateTime dt)
        {
            this._url = url;
            this._lastRssDate = dt;
        }

        public void Read()
        {
            XmlDocument xDoc = new XmlDocument();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
            request.Timeout = 15000;
            request.UserAgent = @"Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.40607; .NET CLR 1.1.4322)";
            Stream stream;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            this._lastModified = response.LastModified;
            stream = response.GetResponseStream();
            StreamReader sr;
            //System.Xml.XmlReader = new XmlReader();
            //stream=Encoding.Convert(Encoding.GetEncoding("GBK"),Encoding.GetEncoding("gb2312"),Convert.ToSByte(stream));
            if (this.Get_CH(response.Headers["Content-Type"].ToString()) == "GBK")
            {
                sr = new StreamReader(stream, System.Text.Encoding.GetEncoding("GB2312"));
                xDoc.Load(sr);
            }
            else
            {
                //                sr= new StreamReader(stream,System.Text.Encoding.UTF8);
                xDoc.Load(stream);
            }

            if (this._lastRssDate < this._lastModified)
            {
                XmlNodeList xnList = xDoc.DocumentElement["channel"].SelectNodes("item");
                //                XmlNodeList xnList=xDoc.SelectNodes("items");
                int a = xnList.Count;
                foreach (XmlNode xNode in xnList)
                {
                    Item rt = new Item();
                    rt.title = xNode.SelectSingleNode("title").InnerText.Replace("'", "''");
                    rt.link = xNode.SelectSingleNode("link").InnerText.Replace("'", "''");
                    rt.description = xNode.SelectSingleNode("description").InnerText.Replace("'", "''");
                    try
                    {
                        rt.pubDate = xNode.SelectSingleNode("pubDate").InnerText;
                    }
                    catch
                    {
                        rt.pubDate = this._lastModified.ToString();
                    }
                    channel.Items.Add(rt);
                }
            }
        }



        public string Create()
        {
            return "";
        }

        private string Get_CH(string s)
        {
            int l = s.IndexOf("charset=") + 8;
            return s.Substring(l, s.Length - l);
        }

    }//
}//
    
