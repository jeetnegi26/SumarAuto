﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataModel
{
    public class EmailTemplateModel
    {
        public string Destination { get; set; }
        [AllowHtml]
        public string Message { get; set; }
        public string Subject { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyContact { get; set; }
    }
}