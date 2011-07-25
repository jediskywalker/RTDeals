using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class UserLocationModel
    {
        public string IPAddress { get; set; }
        public string Status { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string ZipPostalCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Timezone { get; set; }
        public string Gmtoffset { get; set; }
        public string Dstoffset { get; set; }

    }
}