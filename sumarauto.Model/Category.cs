using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
    public class Category : Base
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        [AllowHtml]
        public string Description { get; set; }

    }
}
