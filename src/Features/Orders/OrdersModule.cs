using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common.Features;
using Common.Features.Cruds;
using Common.Features.Cruds.Filterable;
using Microsoft.EntityFrameworkCore;
using Orders.Autocomplete;

namespace Orders
{
    public sealed class OrdersModule : FeatureModule, IConfigureModelFeature
    {
        protected override void RegisterServices(ContainerBuilder builder)
        {
            AddMapperProfile<OrdersProfile>(builder);
        }

        public void OnContextFeatureCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AutocompleteItemConfiguration());
            modelBuilder.ApplyConfiguration(new AutocompleteKindConfiguration());
        }

        protected override IEnumerable<ICrudControllerConfgurator> RegisterCrudControllers()
        {
            yield return new CrudControllerConfgurator<AutocompleteItem, AutocompleteItemDto, AutocompleteItemDto>()
                .UseValidator<AutocompleteItemDtoValidator>()
                .UseFilterableEntityOperation<EntityContextFilterOperation<AutocompleteItem, AutocompleteItemDto>>()
                .AddModifyRoles();
        }
    }
}
