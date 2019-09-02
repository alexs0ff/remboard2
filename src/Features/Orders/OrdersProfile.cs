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
                .ReverseMap();
        }
    }
}
