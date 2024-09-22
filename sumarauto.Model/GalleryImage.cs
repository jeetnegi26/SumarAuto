using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GalleryImage
    {
        public int Id { get; set; }
        public int GalleryId { get; set; }
        public string Image { get; set; }
        public bool DefaultImage { get; set; }
        [ForeignKey("GalleryId")]
        public Gallery Gallery { get; set; }
    }
}
