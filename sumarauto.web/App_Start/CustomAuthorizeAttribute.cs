using DataModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace sumarauto.web.App_Start
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var jwtTokenCookie = filterContext.HttpContext.Request.Cookies[Constant.WebUserCookie];
            if (jwtTokenCookie == null)
            {
                var loginUrl = $"/Login";
                filterContext.Result = new RedirectResult(loginUrl);
                return;
            }

            var jwtHandler = new JwtSecurityTokenHandler();
            var token = jwtHandler.ReadToken(jwtTokenCookie.Value) as JwtSecurityToken;
            if (token != null)
            {
                var user = new ClaimsPrincipal(new ClaimsIdentity(token.Claims, "jwt"));
                var currentUrl = filterContext.HttpContext.Request.Url.AbsolutePath;
                var userRoles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

                if (user.IsInRole("Admin"))
                {
                    if (!currentUrl.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        filterContext.Result = new RedirectResult("/Admin/Index");
                    }
                    return;
                }
                else if (user.IsInRole("User"))
                {
                    var loginUrl = $"/";
                    filterContext.Result = new RedirectResult(loginUrl);
                    return;
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("/Login");
                return;
            }
        }
    }
}