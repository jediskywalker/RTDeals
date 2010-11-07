﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Utilities;

namespace RTDealsWebApplication.Models
{
    public class CustomerModel
    {
        public string TableName() { return "Customer"; }
        public string IdentityColunmn() { return "CustomerID"; }

        #region table columns
          
        [Required]
        [DisplayName("CustomerID")]
        public int CustomerID { get; set; }


        [Required]
        [DisplayName("Username")]
        [StringLength(100)]
        public string Username { get; set; }


        [Required]
        [DisplayName("LastName")]
        [StringLength(100)]
        public string LastName { get; set; }


        [Required]
        [DisplayName("FirstName")]
        [StringLength(100)]
        public string FirstName { get; set; }


        [Required]
        [DisplayName("Password")]
        [StringLength(50)]
        public string Password { get; set; }


        [Required]
        [DisplayName("AccountLevel")]
        public int AccountLevel { get; set; }


        [Required]
        [DisplayName("Status")]
        [StringLength(10)]
        public string Status { get; set; }


        [Required]
        [DisplayName("SignUpDate")]
        public DateTime SignUpdate { get; set; }


        [Required]
        [DisplayName("Address1")]
        [StringLength(100)]
        public string Address1 { get; set; }

        
        [DisplayName("Address2")]
        [StringLength(100)]
        public string Address2 { get; set; }


        [DisplayName("Phone")]
        [StringLength(20)]
        public string Phone { get; set; }


        [DisplayName("State")]
        [StringLength(10)]
        public string State { get; set; }

        [DisplayName("ZipCode")]
        [StringLength(10)]
        public string ZipCode { get; set; }

        [DisplayName("CCNumber")]
        [StringLength(20)]
        public string CCNumber { get; set; }

        [DisplayName("CCCVV")]
        [StringLength(10)]
        public string CCCVV { get; set; }

        [DisplayName("CCExpiredOn")]
        [StringLength(20)]
        public string CCExpiredOn { get; set; }

        [DisplayName("CCInfo")]
        [StringLength(20)]
        public string CCInfo { get; set; }

        [DisplayName("CCToken")]
        [StringLength(20)]
        public string CCToken { get; set; }

        [DisplayName("Email")]
        [StringLength(20)]
        public string Email { get; set; }

        [DisplayName("SMSAddress")]
        [StringLength(20)]
        public string SMSAddress { get; set; }

        [DisplayName("IsNew")]
        public bool IsNew { get; set; }

        [DisplayName("IsPause")]
        public bool IsPause { get; set; }

        [DisplayName("PauseTime")]
        public DateTime PauseTime { get; set; }

        [DisplayName("PauseDays")]
        public Int16 PauseDays { get; set; }

        [DisplayName("LastIPAddress")]
        [StringLength(20)]
        public string LastIPAddress { get; set; }

        [DisplayName("LastLongitude")]
        [StringLength(20)]
        public string LastLongitude { get; set; }

        [DisplayName("LastLatitude")]
        [StringLength(20)]
        public string LastLatitude { get; set; }

        [DisplayName("LastCity")]
        [StringLength(20)]
        public string LastCity { get; set; }

        [DisplayName("LastZipCode")]
        [StringLength(20)]
        public string LastZipCode { get; set; }

        [DisplayName("LastCountryName")]
        [StringLength(20)]
        public string LastCountryName { get; set; }

        [DisplayName("LastTimeZone")]
        [StringLength(20)]
        public string LastTimeZone { get; set; }


        #endregion table columns

    }
}