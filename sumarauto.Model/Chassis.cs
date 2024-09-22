using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
    public class Chassis : Base
    {
        public int ChassisId { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Description { get; set; }
    }
}
