using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sumarauto.Model
{
    public class Liter : Base
    {
        public int LiterId { get; set; }
        public decimal LiterValue { get; set; }

        // Navigation properties
        public List<AutoPart> AutoPart { get; set; }
    }
}
