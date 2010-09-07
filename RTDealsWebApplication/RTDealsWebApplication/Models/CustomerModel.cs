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

        #endregion table columns

    }
}