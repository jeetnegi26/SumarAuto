using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Banner : Base
    {
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Type { get; set; }
        public string Heading { get; set; }
        public string Subheading { get; set; }
        public string url { get; set; }
        public string ButtonText { get; set; }
    }
}
