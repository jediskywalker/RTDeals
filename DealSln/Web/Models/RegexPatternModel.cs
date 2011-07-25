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
    public class RegexPatternModel
    {

        public string TitlePattern { get; set; }
        public string ValuePattern { get; set; }
        public string ReplacementPattern { get; set; }
        public string ExcludePattern { get; set; }


    }
}