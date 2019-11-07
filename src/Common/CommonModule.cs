using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Data;
using Common.Features;
using Common.Features.BaseEntity;
using Common.Features.PermissibleValues;
using Common.Features.ResourcePoints;
using Common.Features.ResourcePoints.Crud.Messaging;
using Common.Features.Tenant;
using Common.MessagingQueue;
using Common.MessagingQueue.Consumers;
using Common.MessagingQueue.Producers;
using Common.Tenant;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;

namespace Common
{
    public class CommonModule: FeatureModule, IConfigureModelFeature
    {
        protected override void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<PermissibleValuesControllerRegistry>().AsSelf();
            builder.RegisterType<ResourcePointControllerRegistry>().AsSelf();
            builder.RegisterType<EntityQueuesRegistry>().AsSelf();
            builder.RegisterType<CommandProducerBase>().AsSelf();
            builder.RegisterType<InMemoryQueueUriBuilder>().As<IQueueUriBuilder>().SingleInstance();

			builder.RegisterGeneric(typeof(Common.Features.ResourcePoints.Filterable.EntityContextFilterOperation<,,>));
            builder.RegisterGeneric(typeof(Common.Features.ResourcePoints.Filterable.EntitySqlFilterOperation<,,>));
            builder.RegisterGeneric(typeof(Common.Features.ResourcePoints.Filterable.SqlFilterStatementParser<,,>));
            builder.RegisterGeneric(typeof(ReflectionPermissibleValuesProvider<,>));
            
            builder.RegisterType<TenantInfoProvider>().As<ITenantInfoProvider>();
			
            builder.RegisterGeneric(typeof(AfterEntityCreateCommandProducer<,,>));
            builder.RegisterGeneric(typeof(AfterEntityEditCommandProducer<,,>));


			//specifications
			builder.RegisterGeneric(typeof(OnlyTenantEntitiesSpecification<,>));
            builder.RegisterGeneric(typeof(IsNotDeletedSpecification<,>));

            //common validators
            builder.RegisterType<EmptyValidator>().AsSelf().SingleInstance();

            //correctors
        }

        public void OnContextFeatureCreating(ModelBuilder modelBuilder, RemboardContextParameters contextParameters)
        {
			modelBuilder.ApplyConfiguration(new TenantEntityConfiguration());
		}
    }
}
