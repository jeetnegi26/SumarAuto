﻿using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
    public class Blogs : Base
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public bool IsFeatured { get; set; }
        [NotMapped]
        public string NewImage { get; set; }
    }
}
