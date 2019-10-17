using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common.Data;
using Common.Extensions;
using Common.Features;
using Common.Features.Cruds;
using Common.Features.Cruds.Filterable;
using Common.Features.PermissibleValues;
using Common.Features.ResourcePoints;
using Microsoft.EntityFrameworkCore;
using Orders.Autocomplete;
using Orders.Branches;
using Orders.OrderStatuses;
using Orders.OrderTypes;

namespace Orders
{
    public sealed class OrdersModule : FeatureModule, IConfigureModelFeature
    {
        protected override void RegisterServices(ContainerBuilder builder)
        {
            AddMapperProfile<OrdersProfile>(builder);
        }

        public void OnContextFeatureCreating(ModelBuilder modelBuilder, RemboardContextParameters contextParameters)
        {
			modelBuilder.ApplyConfiguration(new AutocompleteItemConfiguration());
			modelBuilder.ApplyConfiguration(new AutocompleteKindConfiguration());
			modelBuilder.ApplyEntityDtoConfiguration<AutocompleteItemDto>(contextParameters);
			

			modelBuilder.ApplyConfiguration(new OrderStatusConfiguration());
			modelBuilder.ApplyConfiguration(new OrderStatusKindConfiguration());
			modelBuilder.ApplyEntityDtoConfiguration<OrderStatusDto>(contextParameters);

			modelBuilder.ApplyConfiguration(new BranchConfiguration());
			modelBuilder.ApplyConfiguration(new OrderTypeConfiguration());
        }

        protected override IEnumerable<IPermissibleValuesControllerConfigurator> RegisterPermissibleValuesControllers()
        {
            yield return new PermissibleValuesControllerConfigurator<AutocompleteKind, AutocompleteKinds>()
                .AddValuesProvider<ReflectionPermissibleValuesProvider<AutocompleteKind, AutocompleteKinds>>()
                .AddReadRoles();

            yield return new PermissibleValuesControllerConfigurator<OrderStatusKind, OrderStatusKinds>()
	            .AddValuesProvider<ReflectionPermissibleValuesProvider<OrderStatusKind, OrderStatusKinds>>()
	            .AddReadRoles();
		}

        protected override IEnumerable<IResourcePointConfigurator> RegisterResourcePoints()
        {
	        yield return new ResourcePointConfigurator<OrderType, OrderTypeDto, OrderTypeDto, Guid>()
		        .SetEntityPluralName("OrderTypes")
		        .UseFilterableEntityOperation<Common.Features.ResourcePoints.Filterable.EntityContextFilterOperation<
			        OrderType, OrderTypeDto, Guid>>(
			        parameters => { })
		        .AddReadRoles();

        }

        protected override IEnumerable<ICrudControllerConfigurator> RegisterCrudControllers()
        {
            yield return new CrudControllerConfigurator<AutocompleteItem, AutocompleteItemDto, AutocompleteItemDto>()
                .UseValidator<AutocompleteItemDtoValidator>()
                .UseFilterableEntityOperation<EntityContextFilterOperation<AutocompleteItem, AutocompleteItemDto>>(
                    parameters =>
                    {
                        parameters.AddSortFieldsMapping(nameof(AutocompleteItemDto.AutocompleteKindTitle),nameof(AutocompleteItem.AutocompleteKind) + "." +nameof(AutocompleteItem.AutocompleteKind.Name));
                    })
                /*.UseFilterableEntityOperation<EntitySqlFilterOperation<AutocompleteItem, AutocompleteItemDto>>(
                    parameters =>
                    {
                        parameters.Sql = @"SELECT [Id]
                                          ,[IsDeleted]
                                          ,[DateCreated]
                                          ,[DateModified]
                                          ,[RowVersion]
                                          ,[TenantId]
                                          ,[AutocompleteKindId]
                                          ,[Title] FROM [dbo].[AutocompleteItem] a
                                          {WhereClause}
                                          {OrderByClause}
                                          {PaggingClause}
"
                            ;
                        parameters.DefaultOrderColumn = "Title";
                        parameters.AliasName = "a";

                    })*/
                .AddModifyRoles();

            yield return new CrudControllerConfigurator<OrderStatus, OrderStatusDto, OrderStatusDto>()
	            .SetEntityPluralName("OrderStatuses")
	            .UseValidator<OrderStatusDtoValidator>()
	            .UseFilterableEntityOperation<EntityContextFilterOperation<OrderStatus, OrderStatusDto>>(
		            parameters =>
		            {
			            parameters.AddSortFieldsMapping(nameof(OrderStatusDto.OrderStatusKindTitle), nameof(OrderStatus.OrderStatusKind) + "." + nameof(OrderStatus.OrderStatusKind.Name));
		            })
	            .AddModifyRoles();

            yield return new CrudControllerConfigurator<Branch, BranchDto, BranchDto>()
	            .SetEntityPluralName("Branches")
	            .UseValidator<BranchDtoValidator>()
	            .UseFilterableEntityOperation<EntityContextFilterOperation<Branch, BranchDto>>(
		            parameters =>
		            {
			            
		            })
	            .AddModifyRoles();

            //yield return new CrudControllerConfigurator<OrderType, OrderTypeDto, OrderTypeDto>()
	           // .SetEntityPluralName("OrderTypes")
	           // .UseValidator<OrderTypeDtoValidator>()
	           // .UseFilterableEntityOperation<EntityContextFilterOperation<OrderType, OrderTypeDto>>(
		          //  parameters =>
		          //  {

		          //  })
	           // .AddModifyRoles();

		}
    }
}
