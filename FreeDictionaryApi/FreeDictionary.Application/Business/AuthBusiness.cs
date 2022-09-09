using FreeDictionary.Application.Interface;
using FreeDictionary.Application.Model;
using FreeDictionary.Data.Interface;
using FreeDictionary.Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static FreeDictionary.Application.Model.AuthModel;
using static FreeDictionary.CrossCutting.Extensions.TokenExtensions;

namespace FreeDictionary.Application.Business
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettingsConfiguration _appSettingsConfiguration;
        public AuthBusiness(IUserRepository userRepository, IOptions<AppSettingsConfiguration> appSettingsConfiguration)
        {
            _appSettingsConfiguration = appSettingsConfiguration.Value;
            _userRepository = userRepository;
        }
        public async Task<SinginResponse?> Singin(SinginModel model)
        {
            var user = await _userRepository.AuthenticateAsync(model.Email, model.Password);

            if (user == null)
                return null;

            var token = CreateToken(user.Id, user.Email, user.Name, _appSettingsConfiguration.SecretKey);
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

            var token = CreateToken(user.Id, user.Email, user.Name, _appSettingsConfiguration.SecretKey);
            return new SingupResponse
            {
                Id = user.Id,
                Name = user.Name,
                Token = token
            };
        }
    }
}
