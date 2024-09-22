using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
    public class Liter : Base
    {
        public int LiterId { get; set; }
        public string Ttile { get; set; }
        [AllowHtml]
        public string Description { get; set; }
    }
}
