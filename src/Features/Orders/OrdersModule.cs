using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common.Data;
using Common.Extensions;
using Common.FeatureEntities;
using Common.Features;
using Common.Features.PermissibleValues;
using Common.Features.ResourcePoints;
using Common.Features.ResourcePoints.Crud;
using Microsoft.EntityFrameworkCore;
using Orders.Autocomplete;
using Orders.Branches;
using Orders.OrderStatuses;
using Orders.OrderTypes;
using Common.Features.ResourcePoints.Filterable;
using Entities;
using Entities.Dto;

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
	        yield return new CrudResourcePointConfigurator<OrderType, OrderTypeDto, OrderTypeDto, OrderTypeDto, Guid>()
		        .AddModifyRoles()
		        .UseValidators<OrderTypeDtoValidator, OrderTypeDtoValidator>()
		        .UseEntityContextCrudOperation<EntityContextCrudOperation<OrderType,OrderTypeDto, OrderTypeDto, Guid>>(p=>{})
				.SetEntityPluralName("OrderTypes")
		        .UseFilterableEntityOperation<Common.Features.ResourcePoints.Filterable.EntityContextFilterOperation<
			        OrderType, OrderTypeDto, Guid>>(
			        parameters => { })
				.AddReadRoles(ProjectRoles.Admin,ProjectRoles.Engineer,ProjectRoles.Manager);

	        yield return new CrudResourcePointConfigurator<AutocompleteItem, AutocompleteItemDto, AutocompleteItemDto, AutocompleteItemDto,
			        Guid>()
		        .AddModifyRoles()
		        .UseValidators<AutocompleteItemDtoValidator, AutocompleteItemDtoValidator>()
		        .UseEntityEditSchemaProvider<AutocompleteItemEditSchemaProvider>()
		        .UseEntityContextCrudOperation<EntityContextCrudOperation<AutocompleteItem, AutocompleteItemDto, AutocompleteItemDto, Guid>>(p => { })
		        .UseEntitySchemaProvider<AutocompleteItemSchemaProvider>()
		        .UseFilterableEntityOperation<EntitySqlFilterOperation<AutocompleteItem, AutocompleteItemDto,Guid>>(
			        parameters =>
			        {
				        parameters.Sql = @"SELECT a.[Id]
								         ,a.[IsDeleted]
								         ,a.[DateCreated]
								         ,a.[DateModified]
								         ,a.[RowVersion]
								         ,a.[TenantId]
								         ,a.[AutocompleteKindId]
								         ,a.[Title] ,
										k.Name as AutocompleteKindTitle
										FROM [dbo].[AutocompleteItem] a
										join [dbo].[AutocompleteKind] k on k.Id = a.AutocompleteKindId
								         {WhereClause}
								         {OrderByClause}
								         {PaggingClause}
"
							;
				        parameters.DefaultOrderColumn = "Title";
				        parameters.AliasName = "a";

			        });
				/*.UseFilterableEntityOperation<Common.Features.ResourcePoints.Filterable.EntityContextFilterOperation<AutocompleteItem, AutocompleteItemDto, Guid>>(
			        parameters =>
			        {
				        parameters.AddSortFieldsMapping(nameof(AutocompleteItemDto.AutocompleteKindTitle),
					        nameof(AutocompleteItem.AutocompleteKind) + "." + nameof(AutocompleteItem.AutocompleteKind.Name));
			        })*/;

	        yield return new CrudResourcePointConfigurator<OrderStatus, OrderStatusDto, OrderStatusDto, OrderStatusDto, Guid>()
		        .AddModifyRoles()
		        .UseValidators<OrderStatusDtoValidator, OrderStatusDtoValidator>()
		        .UseEntityContextCrudOperation<EntityContextCrudOperation<OrderStatus, OrderStatusDto, OrderStatusDto, Guid>>(p => { })
		        .SetEntityPluralName("OrderStatuses")
		        .UseFilterableEntityOperation<Common.Features.ResourcePoints.Filterable.EntityContextFilterOperation<
			        OrderStatus, OrderStatusDto, Guid>>(
			        parameters =>
			        {
				        parameters.AddSortFieldsMapping(nameof(OrderStatusDto.OrderStatusKindTitle), nameof(OrderStatus.OrderStatusKind) + "." + nameof(OrderStatus.OrderStatusKind.Name));
					});

	        yield return new CrudResourcePointConfigurator<Branch, BranchDto, BranchDto, BranchDto, Guid>()
		        .AddModifyRoles()
		        .UseValidators<BranchDtoValidator, BranchDtoValidator>()
		        .UseEntityContextCrudOperation<EntityContextCrudOperation<Branch, BranchDto, BranchDto, Guid>>(p => { })
		        .SetEntityPluralName("Branches")
		        .UseFilterableEntityOperation<Common.Features.ResourcePoints.Filterable.EntityContextFilterOperation<
			        Branch, BranchDto, Guid>>(
			        parameters => { });

        }
    }
}
