using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sumarauto.Model
{
    public class Year:Base
    {
        public int YearId { get; set; }
        public int YearValue { get; set; }

        // Navigation properties
        public List<AutoPart> AutoPart { get; set; }
    }
}
