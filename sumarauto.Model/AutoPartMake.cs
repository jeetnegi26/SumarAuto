using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AutoPartMake : Base
    {
        public int Id { get; set; }
        public int Make_Id { get; set; }
        public string Model_Title { get; set; }
        public string Year_Title { get; set; }
        public string Engine_Title { get; set; }
        public string Chassis_Title { get; set; }
        public string Liter_Title { get; set; }
        public AutoPart AutoPart { get; set; }
    }
}
