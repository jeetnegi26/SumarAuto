using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Base
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime EditedOn { get; set; }
        public bool Status { get; set; }
        public string UserHostAdd { get; set; }
        public string RewriteUrl { get; set; }
        [NotMapped]
        public string CreatedOnString { get; set; }
        [NotMapped]
        public string EditedOnString { get; set; }
    }
}
