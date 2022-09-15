using AutoMapper;
using FreeDictionary.Application.Configuration;
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
using static FreeDictionary.CrossCutting.Extensions.TokenExtensions;

namespace FreeDictionary.Application.Business
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly AppSettingsConfiguration _appSettingsConfiguration;
        public AuthBusiness(IMapper mapper, IUserRepository userRepository, IOptions<AppSettingsConfiguration> appSettingsConfiguration)
        {
            _mapper = mapper;
            _appSettingsConfiguration = appSettingsConfiguration.Value;
            _userRepository = userRepository;
        }
        public async Task<(AuthModel?, bool)> Singin(SinginModel model)
        {
            var user = await _userRepository.AuthenticateAsync(model.Email, model.Password);

            if (user == null)
                return (null, false);

            var token = CreateToken(user.Id, user.Email, user.Name, _appSettingsConfiguration.SecretKey);

            var result = _mapper.Map<AuthModel>(user);
            result.Token = token;

            return (result, false);
        }

        public async Task<(AuthModel?, bool)> Singup(SingupModel model)
        {
            var user = await _userRepository.AddAsync(model.Name, model.Email, model.Password);

            if (user == null)
                return (null, false);

            var token = CreateToken(user.Id, user.Email, user.Name, _appSettingsConfiguration.SecretKey);

            var result = _mapper.Map<AuthModel>(user);
            result.Token = token;

            return (result, false);
        }
    }
}
