using AutoMapper;
using FreeDictionary.Application.Model;
using FreeDictionary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Application.AutoMapper
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AuthModel, User>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => $"{Convert.ToInt32(src.Id)}")
                )
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name)
                ).ReverseMap();
        }
    }
}
