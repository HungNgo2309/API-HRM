using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            // Tạo khóa bảo mật từ giá trị SigningKey trong cấu hình
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }
        // Phương thức tạo token cho người dùng
        public string CreateToken(AppUser user)
        {
            // Tạo danh sách các claims (yêu cầu) của người dùng
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,user.UserName)
            };
            // Tạo thông tin xác thực ký sử dụng HMAC SHA512
            var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
            // Mô tả token
            var tokenDescriptor= new SecurityTokenDescriptor{
                Subject =new ClaimsIdentity(claims),
                 // Thiết lập thời gian hết hạn của token (7 ngày)
                Expires =DateTime.Now.AddDays(7),
                SigningCredentials=creds,
                Issuer=_config["JWT:Issuer"],
                Audience=_config["JWT:Audience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            // Tạo token dựa trên tokenDescriptor
            var token =tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}