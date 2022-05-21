using Application.Identity.Responses;
using AutoMapper;
using Infrastructure.Models;

namespace Infrastructure.Mappings.Identity;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserResponse, ApplicationUser>().ReverseMap();
    }
}