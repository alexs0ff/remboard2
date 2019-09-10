using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common.Features;
using Common.Features.Cruds;
using Common.Features.Cruds.Filterable;
using Common.Features.PermissibleValues;
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
            modelBuilder.ApplyConfiguration(new EntityDtoConfiguration<AutocompleteItemDto>());
        }

        protected override IEnumerable<IPermissibleValuesControllerConfigurator> RegisterPermissibleValuesControllers()
        {
            yield return new PermissibleValuesControllerConfigurator<AutocompleteKind, AutocompleteKinds>()
                .AddValuesProvider<ReflectionPermissibleValuesProvider<AutocompleteKind, AutocompleteKinds>>()
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
        }
    }
}
