﻿using AutoMapper;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Profiles
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, ExternalModels.MenuDTO>();
            CreateMap<ExternalModels.MenuDTO, Menu>();

            CreateMap<Category, ExternalModels.CategoryDTO>();
            CreateMap<ExternalModels.CategoryDTO, Category>();
        }
    }
}
