using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DataModel
{
    public class AutoPartDataModel
    {
        public int Id { get; set; } = 0;
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ExtraField { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public int DisplayOrder { get; set; } = 0;
        public string Image { get; set; }
        public string AutoPartSId { get; set; }
        public string Category { get; set; }
        public string DefaultImage { get; set; }
        public string RewriteUrl { get; set; }
        public bool Status { get; set; }
        public bool IsFeatured { get; set; }

        // List to handle multiple cloned details
        public List<MultipleDetail> MultipleDetails { get; set; }
    }
    public class MultipleDetail
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Engine { get; set; }
        public string Liter { get; set; }
        public string Chassis { get; set; }
    }
    public class MakeDetail
    {
        public int Id { get; set; }
        public string AutoPartSId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Engine { get; set; }
        public string Liter { get; set; }
        public string Chassis { get; set; }
        public int DisplayOrder { get; set; }
    }
    public class MakeDetailAction
    {
        public int Id { get; set; }
        public string AutoPartSId { get; set; }
        public int AutoPartId { get; set; }
        [Required]
        public int MakeId { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Engine { get; set; }
        public string Liter { get; set; }
        public string Chassis { get; set; }
        public int DisplayOrder { get; set; }
        public bool Status { get; set; }
    }
}
