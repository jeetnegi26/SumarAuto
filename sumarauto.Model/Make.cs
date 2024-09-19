using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sumarauto.Model
{
    public class Make : Base
    {
        public int MakeId { get; set; }
        public string MakeName { get; set; }

        // Navigation properties
        public List<AutoPart> AutoPart { get; set; }
    }
}
