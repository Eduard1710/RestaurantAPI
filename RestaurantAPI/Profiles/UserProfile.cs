﻿using AutoMapper;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, ExternalModels.UserDTO>();
            CreateMap<ExternalModels.UserDTO, User>();

            CreateMap<User, ExternalModels.UpdateUserDTO>();
            CreateMap<ExternalModels.UpdateUserDTO, User>();
        }
    }
}
