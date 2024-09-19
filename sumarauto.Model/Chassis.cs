using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sumarauto.Model
{
    public class Chassis : Base
    {
        public int ChassisId { get; set; }
        public string ChassisType { get; set; }
        public List<AutoPart> AutoPart { get; set; }
    }
}
