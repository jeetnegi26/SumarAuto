using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Constant
    {
        public const string WebUserCookie = "sumarauto";
        public enum ActionType
        {
            Add = 0,
            Edit = 1,
            View = 2,
        }
        public static class Message
        {
            public const string Error = "Something went wrong!";
            public const string Add = "{0}, is succesfully added!";
            public const string Edit = "{0}, is succesfully edited!";
            public const string Delete = "{0}, is succesfully deleted!";
            public const string Exist = "{0}, is already exist!";
        }
    }
    public enum BannerType
    {
        Home,
    }
    public static class BannerConstant
    {
        public const string Home = "Home";
        public const string About = "About";
        public const string Contact = "Contact";
    }
}
