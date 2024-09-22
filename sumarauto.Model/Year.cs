using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
    public class Year:Base
    {
        public int YearId { get; set; }
        public int Title { get; set; }
        [AllowHtml]
        public string Description { get; set; }
    }
}
