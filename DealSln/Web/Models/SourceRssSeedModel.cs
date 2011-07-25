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
    public class SourceRssSeedModel
    {
        public int SourceID { get; set; }
        public string RSSAddress { get; set; }
        public string Additional { get; set; }
    }
}