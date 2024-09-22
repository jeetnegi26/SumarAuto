using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Banner : Base
    {
        public int Id { get; set; }
        public string TypeId { get; set; }
        public string BannerHeading { get; set; }
        public string BannerSubHeading { get; set; }
        public string BannerButtonText { get; set; }
        public string BannerButtonUrl { get; set; }
        [NotMapped]
        public List<BannerImages> BannerImages { get; set; }
    }
}
