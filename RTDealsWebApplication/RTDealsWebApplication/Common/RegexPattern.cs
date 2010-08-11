using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using RTDealsWebApplication.Models;
using RTDealsWebApplication.DBAccess;
using RTDealsWebApplication.Common;

namespace RTDealsWebApplication.Common
{

    public class RegexPattern
    {
        // For Test Purpose
        public string dealaliveTitlePattern = "<img src='img/dealstatus/.*</a></td><td align='center' class='font_size11'>";
        public string dealaliveValuePattern = "&nbsp;&nbsp;(.*?)</a></td><td align='center' class='font_size11'>";
        public string dealaliveReplacementPattern = "detail";
        public string dealaliveExcludePattern = "index.html?page=1";

        public string dealseaTitlePattern = "<div class=\"dealbox\"><a href=\"/view-deal/.*<p><span class=\"carat\">&#8250;</span>";
        public string dealseaValuePattern = "<div class=\"dealbox\">(.*?)<p><span class=\"carat\">&#8250;</span>";
        public string dealseaReplacementPattern = "view-deal";
        public string dealseaExcludePattern = "";

        public string slickdealsTitlePattern = "<a id=\"deal_header.*</a>";
        public string slickdealsValuePattern = "<h3>(.*?)</h3>";
        public string slickdealsReplacementPattern = "/permadeal";
        public string slickdealsPattern = "";

        public string headlinedealsTitlePattern = "<td align=\"center\" width=\"110\">\r\n\t\t.*<tr>\r\n\t\t<td><img src=\"images/pixel_trans.gif";    //\r\n\t\t
        public string headlinedealsValuePattern = "<td align=\"center\" width=\"110\">\r\n\t\t(.*?)<tr>\r\n\t\t<td><img src=\"images/pixel_trans.gif";
        public string headlinedealsReplacementPattern = "";
        public string headlinedealsExcludePattern = "";

        public string fatwalletTitlePattern = "";
        public string fatwalletValuePattern = "";
        public string fatwalletReplacementPattern = "";
        public string fatwalletExcludePattern = "";
        




      //  <a href="/view-deal/42532">Fujifilm FinePix AV100 Digital Camera + 2GB SD Card $64</a>, </strong>Aug 07<br /><br />
      //  <a href="/exec/j/4/?pid=42532&lno=1&afsrc=1" target="_blank">Dell Home</a>
      //  has Fujifilm FinePix AV100 Silver 12MP 3X Zoom Digital Camera with 2GB SD Memory Card for $80 - 20% off coupon code <b>CMMMRLW7NRHGCJ</b> = <b>$64
      //  </b> with free shipping.<p><span class="carat">&#8250;</span>


    }


 
}