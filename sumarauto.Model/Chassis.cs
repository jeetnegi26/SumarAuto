using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sumarauto.Model
{
    public class Chassis
    {
        public int ChassisId { get; set; }
        public string ChassisType { get; set; }

        // Navigation properties
        public ICollection<MModel> Models { get; set; }
        public ICollection<Year> Years { get; set; }
        public ICollection<Engine> Engines { get; set; }
    }
}
