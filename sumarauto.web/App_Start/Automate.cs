using Model;
using Service;
using sumarauto.database;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace sumarauto.web.App_Start
{
    public class Automate
    {
        public static void CreateRoles()
        {
            using (var context = new AppDbContext())
            {
                if (!context.Roles.Any())
                {
                    string role = ConfigurationManager.AppSettings["Roles"];
                    string[] roles = role.Split(',');

                    foreach (var item in roles)
                    {
                        var model = new Role
                        {
                            Title = item,
                            CreatedOn = HelperService.Instance.getCurrentDateTime(),
                            EditedOn = HelperService.Instance.getCurrentDateTime(),
                            Status = true,
                        };

                        context.Roles.Add(model);
                    }

                    context.SaveChanges();
                }
            }
        }
        public static void CreateAdminUser()
        {
            using (var context = new AppDbContext())
            {
                if (context.User.FirstOrDefault(x => x.CreatedBy == "Admin") == null)
                {
                    string adminPassword = ConfigurationManager.AppSettings["AdminPassword"];
                    var user = new User
                    {
                        CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        EditedOn = HelperService.Instance.getCurrentDateTime(),
                        CreatedBy = "Admin",
                        Email = ConfigurationManager.AppSettings["AdminEmail"],
                        HashPassword = HelperService.Instance.Encrypt(adminPassword),
                        Status = true,
                    };

                    context.User.Add(user);
                    context.SaveChanges();
                    var userRole = new RoleAssign
                    {
                        UserId = user.Id,
                        RoleId = context.Roles.FirstOrDefault(x => x.Title == "Admin").Id,
                        CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        EditedOn = HelperService.Instance.getCurrentDateTime(),
                        CreatedBy = "Admin",
                        Status = true,
                    };
                    context.RoleAssign.Add(userRole);
                    context.SaveChanges();
                }
            }
        }
        public static void CreateWebInfo()
        {
            using (var context = new AppDbContext())
            {
                var data = context.Keys.ToList();
                var result = new List<Key>();
                if (!data.Any(x => x.Type == "BrandName"))
                {
                    result.Add(new Key
                    {
                        Type = "BrandName",
                        Name = "WOR",
                        Description = "",
                        CreatedOn = HelperService.Instance.getCurrentDateTime(),
                    });
                }
                if (!data.Any(x => x.Type == "BrandLogo"))
                {
                    result.Add(new Key
                    {
                        Type = "BrandLogo",
                        Name = "https://websonrent.com/Content/LogoImage/w.png",
                        Description = "",
                        CreatedOn = HelperService.Instance.getCurrentDateTime(),
                    });
                }
                if (!data.Any(x => x.Type == "BrandInfo"))
                {
                    result.AddRange(new List<Key>
                    {
                        new Key
                        {
                            Type = "BrandInfo",Name = "Address",Description = "Miyanwala, Dehradun, Uttarakhand 248005",CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        },
                        new Key
                        {
                            Type = "BrandInfo",Name = "Email",Description = "WebOnRent@gmail.com",CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        },
                        new Key
                        {
                            Type = "BrandInfo",Name = "Phone",Description = "9082968558, 9004755779",CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        },
                        new Key
                        {
                            Type = "BrandInfo",Name = "Business Hours",
                            Description = "Mon - Fri, 10AM -8PM",
                            CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        },
                    });
                }
                if (!data.Any(x => x.Type == "SocialMedia"))
                {
                    result.AddRange(new List<Key>
                    {
                        new Key
                        {
                            Type = "SocialMedia",Name = "facebook",Description = "",CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        },
                        new Key
                        {
                            Type = "SocialMedia",Name = "instagram",Description = "",CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        },
                        new Key
                        {
                            Type = "SocialMedia",Name = "linkedin",Description = "",CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        },
                        new Key
                        {
                            Type = "SocialMedia",Name = "twitter-x",Description = "",CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        },
                        new Key
                        {
                            Type = "SocialMedia",Name = "youtube",Description = "",CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        },
                        new Key
                        {
                            Type = "SocialMedia",Name = "whatsapp",Description = "",CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        },
                        new Key
                        {
                            Type = "SocialMedia",Name = "pinterest",Description = "",CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        },
                        new Key
                        {
                            Type = "SocialMedia",Name = "google",Description = "",CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        },
                        new Key
                        {
                            Type = "SocialMedia",Name = "threads",Description = "",CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        },
                        new Key
                        {
                            Type = "SocialMedia",Name = "github",Description = "",CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        },
                        new Key
                        {
                            Type = "SocialMedia",Name = "Icon-Type",Description = "fa",CreatedOn = HelperService.Instance.getCurrentDateTime(),
                        },
                    });
                }
                if (!data.Any(x => x.Type == "ContactMail"))
                {
                    result.Add(new Key
                    {
                        Type = "ContactMail",
                        Name = "Message",
                        Description = "Thank you for reaching out to us at",
                        CreatedOn = HelperService.Instance.getCurrentDateTime(),
                    });
                }
                context.Keys.AddRange(result);
                context.SaveChanges();
            }
        }
    }
}