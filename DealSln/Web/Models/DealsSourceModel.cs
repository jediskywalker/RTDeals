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

namespace Web.Models
{
    public class DealsSourceModel
    {

        public string TableName() { return "DealsSource"; }
        public string IdentityColunmn() { return "SourceName"; }

        #region table columns

        [Required]
        [DisplayName("SourceID")]
        public int SourceID { get; set; }

        [Required]
        [DisplayName("SourceName")]
        public string SourceName { get; set; }

        [Required]
        [DisplayName("SourceURL")]
        public string SourceURL { get; set; }


        [Required]
        [DisplayName("SourceCategoryID")]
        public int SourceCategoryID { get; set; }


        [Required]
        [DisplayName("Status")]
        public string Status { get; set; }


        [Required]
        [DisplayName("Notes")]
        public string Notes { get; set; }


        [Required]
        [DisplayName("CreateDate")]
        public DateTime CreateDate { get; set; }



        [Required]
        [DisplayName("CreateByUserID")]
        public int CreateByUserID { get; set; }



       
        #endregion table columns

    }
}