using App.API.Models;
using App.Common.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.API.Data.Map
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(x => x.Product, mf => mf.MapFrom(m => m))
                .ForMember(x => x.ProductId, mf => mf.MapFrom(m => m.Id));

            CreateMap<Basket, BasketModel>()
                .ForMember(x => x.UserId, mf => mf.MapFrom(m => m.UserId))
                .ForMember(x => x.BasketId, mf => mf.MapFrom(m => m.Id))
                .ForMember(x => x.ProductsInBasket, mf => mf.MapFrom(m => m.ProductsInBasket));

            CreateMap<ProductInBasket, ProductModel>()
                .ForMember(x => x.Product, mf => mf.MapFrom(m => m.Product))
                .ForMember(x => x.Quantity, mf => mf.MapFrom(m => m.Quantity))
                .ForMember(x => x.BasketId, mf => mf.MapFrom(m => m.BasketId));
        }
    }
}
