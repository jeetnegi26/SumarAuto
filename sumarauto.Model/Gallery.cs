using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Gallery : Base
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [NotMapped]
        public List<GalleryImage> galleryImages { get; set; }
        [NotMapped]
        public string DefaultImage { get; set; }
    }
}
