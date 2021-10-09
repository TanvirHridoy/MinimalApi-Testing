using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MinimalApi
{
    public class ApiConst
    {
        public const string Issuer = "Hridoy";
        public const string Audience = "MinimalAppUser";
        public const string key = "28216481291932476";

        public const string AuthSchemes = "Identity.Application" + "," + JwtBearerDefaults.AuthenticationScheme;
        public static object CreateToken(JwtToken model) 
        {
            var user = "hridoy";
            var password = "12345678";
            if(user==model.EmployeeId && password == model.Password)
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApiConst.key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                           ApiConst.Issuer,
                           ApiConst.Audience,
                           expires: DateTime.UtcNow.AddHours(2),
                           signingCredentials: creds
                        );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                return "Failed";
            }
            
        }
    }
    public class JwtToken
    {
        public string EmployeeId { get; set; }
        public string Password { get; set; }
    }
}
