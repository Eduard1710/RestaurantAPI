﻿using AutoMapper;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, ExternalModels.OrderDTO>();
            CreateMap<ExternalModels.OrderDTO, Order>();

            CreateMap<OrderMenu, ExternalModels.OrderMenuDTO>();
            CreateMap<ExternalModels.OrderMenuDTO, OrderMenu>();

            CreateMap<Order, ExternalModels.UpdateOrderDTO>();
            CreateMap<ExternalModels.UpdateOrderDTO, Order>();

            CreateMap<OrderMenu, ExternalModels.UpdateOrderMenuDTO>();
            CreateMap<ExternalModels.UpdateOrderMenuDTO, OrderMenu>();
        }
    }
}
