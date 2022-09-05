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
using static FreeDictionary.Application.Model.AuthModel;

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
        public async Task<SinginResponse?> Singin(SinginModel model)
        {
            var user = await _userRepository.AuthenticateAsync(model.Email, model.Password);

            if (user == null)
                return null;

            var token = CreateToken(user.Id, user.Email, user.Name);
            return new SinginResponse
            {
                Id = user.Id,
                Name = user.Name,
                Token = token
            };
        }

        public async Task<SingupResponse?> Singup(SingupModel model)
        {
            var user = await _userRepository.AddAsync(model.Name, model.Email, model.Password);

            if (user == null)
                return null;

            var token = CreateToken(user.Id, user.Email, user.Name);
            return new SingupResponse
            {
                Id = user.Id,
                Name = user.Name,
                Token = token
            };
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
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);
            return result;
        }
    }
}
