using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AutoPartImages
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public bool Default { get; set; }
        public int AutoPartId { get; set; }
        [ForeignKey("AutoPartId")]
        public AutoPart AutoPart { get; set; }

    }
}
