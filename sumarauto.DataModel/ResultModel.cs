using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class RoleAssignModel
    {
        public string Roles { get; set; }
        public string Url { get; set; }
    }
    public class ResultModel
    {
        public bool Result { get; set; }
        public string Messsage { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public List<string> values3 { get; set; }
    }
    public class ResultViewModels
    {
        public bool Result { get; set; }
        public bool EditResult { get; set; }
        public string Messsage { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }

        public ResultViewModels()
        {
            EditResult = false;
        }
    }
    public class ResultViewModelInfo
    {
        public string Type { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
    }
}
