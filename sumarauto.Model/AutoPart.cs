using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sumarauto.Model
{
    public class AutoPart : Base
    {
        public int PartId { get; set; }
        public string PartName { get; set; }
        public decimal Price { get; set; }

        // Foreign keys
        public int MakeId { get; set; }
        public int ModelId { get; set; }
        public int YearId { get; set; }
        public int EngineId { get; set; }
        public int LiterId { get; set; }
        public int ChassisId { get; set; }

        // Navigation properties
        public Make Make { get; set; }
        public MModel Model { get; set; }
        public Year Year { get; set; }
        public Engine Engine { get; set; }
        public Liter Liter { get; set; }
        public Chassis Chassis { get; set; }
    }
}
