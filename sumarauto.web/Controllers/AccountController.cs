using DataModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Caching;
using Microsoft.IdentityModel.Tokens;
using Service;

namespace sumarauto.web.Controllers
{
    public class AccountController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["sumarautoDb"].ConnectionString;

        [Route("login")]
        public ActionResult Login(string message)
        {
            ViewBag.Message = message;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginDataModel model)
        {
            try
            {
                bool Result = false;
                string Message = string.Empty;
                string defaultUrlString = "/";
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                using (var connection = new SqlConnection(connectionString))
                {
                    var hashPass = HelperService.Instance.Encrypt(model.Password);
                    var command = new SqlCommand("AuthenticateUser", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserName", model.Email);
                    command.Parameters.AddWithValue("@Password", hashPass);

                    // Add output parameters
                    var userIdParam = new SqlParameter("@UserId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    var userRoleParam = new SqlParameter("@UserRole", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    var defaultUrl = new SqlParameter("@Url", SqlDbType.NVarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(userIdParam);
                    command.Parameters.Add(userRoleParam);
                    command.Parameters.Add(defaultUrl);

                    connection.Open();
                    command.ExecuteNonQuery();
                    // Get the output values
                    var userId = userIdParam.Value != DBNull.Value ? (int)userIdParam.Value : (int?)null;
                    var userRole = userRoleParam.Value.ToString();               
                    if (userId != null)
                    {
                        var jwtToken = GenerateJwtToken(userId.Value, model.Email, userRole);
                        Result = true;
                        if (!string.IsNullOrEmpty(userRole))
                        {
                            defaultUrlString = userRole.Contains("Admin") ? "/admin" : "/";
                        }
                    }
                    else
                    {
                        Message = userRole;
                    }
                }
                return Json(new { result = Result, message = Message,redirectURL = defaultUrlString }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GenerateJwtToken(int userId, string userName, string userRoles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtHeaderParameterNames.Jku, userName),
                new Claim(JwtHeaderParameterNames.Kid, userId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, userName)
            };
            if (userRoles.Any())
            {
                var roleClaim = new Claim(ClaimTypes.Role, userRoles);
                claims.Add(roleClaim);
            }
            var key = new SymmetricSecurityKey(
       Encoding.UTF8.GetBytes(Convert.ToString(ConfigurationManager.AppSettings["config:JwtKey"])));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(3);
            if (userRoles == "Admin")
            {
                expires = DateTime.Now.AddDays(30);
            }
            var token = new JwtSecurityToken(
                Convert.ToString(ConfigurationManager.AppSettings["config:JwtIssuer"]),
                Convert.ToString(ConfigurationManager.AppSettings["config:JwtAudience"]),
                claims,
                expires: expires,
                signingCredentials: creds
            );
            Response.Cookies.Add(new HttpCookie(Constant.WebUserCookie, new JwtSecurityTokenHandler().WriteToken(token))
            {
                HttpOnly = true,
                Secure = Request.IsSecureConnection,
                Expires = expires
            });
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private readonly ObjectCache _cache = MemoryCache.Default;
        public ActionResult LogOut()
        {

            HttpCookie jwtCookie = Request.Cookies[Constant.WebUserCookie];
            if (jwtCookie != null)
            {
                string cacheKey = "GetUsernameIdUrlToken_" + jwtCookie.Value;
                if (_cache.Contains(cacheKey))
                {
                    _cache.Remove(cacheKey);
                }
            }
            Response.Cookies[Constant.WebUserCookie].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Login", "Account");
        }
    }
}