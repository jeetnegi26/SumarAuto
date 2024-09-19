using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sumarauto.Model
{
    public class MModel
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }

        // Navigation properties
        public ICollection<Make> Makes { get; set; }
        public ICollection<Year> Years { get; set; }
        public ICollection<Engine> Engines { get; set; }
        public ICollection<Chassis> Chassis { get; set; }
    }
}
