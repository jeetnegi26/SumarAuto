using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sumarauto.DataModel
{
    public class LoginDataModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class ResetPassword
    {
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string OldPassword { get; set; }
    }
    public class CurrentLogin
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public List<string> Role { get; set; }
    }
}
