using FreeDictionary.Application.Interface;
using FreeDictionary.Application.Model;
using FreeDictionary.Data.Interface;
using FreeDictionary.Domain;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Application.Business
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IUserRepository _userRepository;
        private readonly string _secretKey = "262db528-2080-4fb0-b15d-8a96764a33a7";
        public AuthBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<AuthModel.SinginResponse> Singim(AuthModel.SinginModel model)
        {
            var user = await _userRepository.AuthenticateAsync(model.Email, model.Password);
            return default;
        }

        public async Task<AuthModel.SingupResponse> Singup(AuthModel.SingupModel model)
        {
            var user = await _userRepository.AddAsync(model.Name, model.Email, model.Password);
            return default;
        }

        private string CreateToken(Guid id, string email, string name)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);  //_appSettings.Secret
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Name, name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);
            return result;
        }
    }
}
