using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Data;
using Common.Features;
using Common.Features.BaseEntity;
using Common.Features.Cruds;
using Common.Features.Cruds.Filterable;
using Common.Features.PermissibleValues;
using Common.Features.ResourcePoints;
using Common.Features.Tenant;
using Common.Tenant;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;

namespace Common
{
    public class CommonModule: FeatureModule, IConfigureModelFeature
    {
        protected override void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<EntityControllerRegistry>();
            builder.RegisterType<PermissibleValuesControllerRegistry>();
            builder.RegisterType<ResourcePointControllerRegistry>();

            builder.RegisterGeneric(typeof(EntityContextFilterOperation<,>));
            builder.RegisterGeneric(typeof(EntitySqlFilterOperation<,>));
            builder.RegisterGeneric(typeof(SqlFilterStatementParser<,>));
            builder.RegisterGeneric(typeof(ReflectionPermissibleValuesProvider<,>));
            builder.RegisterType<TenantInfoProvider>().As<ITenantInfoProvider>();


            //specifications
            builder.RegisterGeneric(typeof(OnlyTenantEntitiesSpecification<>));
            builder.RegisterGeneric(typeof(IsNotDeletedSpecification<>));

            //common validators
            builder.RegisterType<EmptyValidator>().AsSelf().SingleInstance();

            //correctors
            builder.RegisterType<TenantedEntityCorrector>().AsSelf().SingleInstance();
        }

        public void OnContextFeatureCreating(ModelBuilder modelBuilder, RemboardContextParameters contextParameters)
        {
			modelBuilder.ApplyConfiguration(new TenantEntityConfiguration());
		}
    }
}
