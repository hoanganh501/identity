using Application.Respsone;
using AutoMapper;
using Domain.Entity;

namespace Application.Mapper
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<ApplicationUser, AuthResponse>();        
        }
    }
}   
