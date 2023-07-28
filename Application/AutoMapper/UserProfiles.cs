using Application.Dtos.User;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapper
{
    public class UserProfiles : Profile
    {
        public UserProfiles() 
        {
            CreateMap<UserRegisterDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserRegisterDto>();
        }

    }
}
