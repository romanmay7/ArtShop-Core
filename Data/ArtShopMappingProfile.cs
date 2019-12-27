using AutoMapper;
using myArtShopCore.Data.Entities;
using myArtShopCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myArtShopCore.Data
{
    public class ArtShopMappingProfile:Profile
    {
        public ArtShopMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderID, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
              .ReverseMap();
        }
    }
}
