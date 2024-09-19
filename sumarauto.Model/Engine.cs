using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sumarauto.Model
{
    public class Engine : Base
    {
        public int EngineId { get; set; }
        public string EngineType { get; set; }

        // Navigation properties
        public List<AutoPart> AutoPart { get; set; }
    }
}
