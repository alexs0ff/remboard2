using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Common.Features;
using Entities;
using Entities.Dto;
using Orders.Autocomplete;
using Orders.Branches;
using Orders.OrderStatuses;
using Orders.OrderTypes;

namespace Orders
{
    public class OrdersProfile: FeatureMapperProfile
    {
        public OrdersProfile()
        {
            CreateMap<AutocompleteItem, AutocompleteItemDto>()
                .ForMember(i=>i.AutocompleteKindTitle,m=>m.MapFrom(i=>i.AutocompleteKind.Name))
                .ReverseMap()
				.ForMember(i => i.AutocompleteKind, m => m.Ignore()); ;

            CreateMap<OrderStatus, OrderStatusDto>()
	            .ForMember(i => i.OrderStatusKindTitle, m => m.MapFrom(p => p.OrderStatusKind.Name))
	            .ReverseMap()
	            .ForMember(i=>i.OrderStatusKind,m=>m.Ignore());

            CreateMap<Branch, BranchDto>()
	            .ReverseMap();

            CreateMap<OrderType, OrderTypeDto>()
	            .ReverseMap();
		}
    }
}
