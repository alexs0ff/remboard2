using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Common.Features;
using Orders.Autocomplete;

namespace Orders
{
    public class OrdersProfile: FeatureMapperProfile
    {
        public OrdersProfile()
        {
            CreateMap<AutocompleteItem, AutocompleteItemDto>()
                .ForMember(i=>i.AutocompleteKindTitle,m=>m.MapFrom(i=>i.AutocompleteKind.Name))
                .ReverseMap();
        }
    }
}
