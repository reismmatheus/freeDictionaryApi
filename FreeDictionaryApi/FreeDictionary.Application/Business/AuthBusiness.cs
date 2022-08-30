using FreeDictionary.Application.Interface;
using FreeDictionary.Application.Model;
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
        private readonly IAuthBusiness _authBusiness;
        public AuthBusiness()
        {

        }
        public Task<AuthModel.SinginResponse> Singim(AuthModel.SinginModel model)
        {
            throw new NotImplementedException();
        }

        public Task<AuthModel.SingupModel> Singup(AuthModel.SingupModel model)
        {
            throw new NotImplementedException();
        }

        private string CreateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("d3e84769b4fce34f69a850e42b25282c");  //_appSettings.Secret
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // new Claim(ClaimTypes.Name, user.Id.ToString())
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
