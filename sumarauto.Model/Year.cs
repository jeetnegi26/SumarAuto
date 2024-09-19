using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sumarauto.Model
{
    public class Year
    {
        public int YearId { get; set; }
        public int YearValue { get; set; }

        // Navigation properties
        public ICollection<Model> Models { get; set; }
        public ICollection<Chassis> Chassis { get; set; }
    }
}
