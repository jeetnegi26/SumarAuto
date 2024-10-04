using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DataModel
{
    public class HomeViewModel
    {
        public List<Banner> Banners { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Makes { get; set; }
    }
}
