using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sumarauto.Model
{
    public class Engine
    {
        public int EngineId { get; set; }
        public string EngineType { get; set; }

        // Navigation properties
        public ICollection<MModel> Models { get; set; }
        public ICollection<Liter> Liters { get; set; }
        public ICollection<Chassis> Chassis { get; set; }
    }
}
