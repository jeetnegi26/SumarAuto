using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
    public class AutoPart : Base
    {
        public int Id { get; set; }
        public string AutoPartSId { get; set; }
        public string Title { get; set; }
        public string ExtraField { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public decimal Price { get; set; }
        // Foreign keys
        public int Category_Id { get; set; }
        public bool IsFeatured { get; set; }
        public List<AutoPartMake> AutoPartMake { get; set; }
        public List<AutoPartImages> AutoPartImages { get; set; }

    }
}
