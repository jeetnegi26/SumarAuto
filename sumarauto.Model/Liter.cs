using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sumarauto.Model
{
    public class Liter
    {
        public int LiterId { get; set; }
        public decimal LiterValue { get; set; }

        // Navigation properties
        public ICollection<Engine> Engines { get; set; }
    }
}
