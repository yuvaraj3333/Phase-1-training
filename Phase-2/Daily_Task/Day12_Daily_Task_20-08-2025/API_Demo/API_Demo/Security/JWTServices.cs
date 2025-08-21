////using Microsoft.IdentityModel.Token;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace API_Demo.Security
//{
//    public class JWTServices
//    {
//        public static string CreateJWTToken(JWTOptions options, IEnumerable<Claim> claims)
//        {
//            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key)),
//                SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(
//                issuer: options.Issuer,
//                audience: options.Audience,
//                claims: claims,
//                expires: DateTime.Now.AddMinutes(30),
//                signingCredentials: creds);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}
