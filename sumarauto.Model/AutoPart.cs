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
        public string Title { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public decimal Price { get; set; }

        // Foreign keys
        public int MakeId { get; set; }
        public int MModelId { get; set; }
        public int YearId { get; set; }
        public int EngineId { get; set; }
        public int LiterId { get; set; }
        public int ChassisId { get; set; }
        public bool IsFeatured { get; set; }

    }
}
