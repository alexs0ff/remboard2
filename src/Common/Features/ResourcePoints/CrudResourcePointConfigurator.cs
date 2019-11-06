using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Common.Extensions;
using Common.FeatureEntities;
using Common.Features.BaseEntity;
using Common.Features.ResourcePoints.Crud;
using Common.Features.ResourcePoints.Crud.Messaging;
using Common.Features.ResourcePoints.Schema;
using Common.Features.Tenant;
using Entities;

namespace Common.Features.ResourcePoints
{
	public class CrudResourcePointConfigurator<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey>: ResourcePointConfigurator<TEntity, TFilterableEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TCreateEntityDto : class
		where TEditEntityDto : class
		where TKey : struct
	{
		private readonly HashSet<ProjectRoles> _modifyRoles = new HashSet<ProjectRoles>();

		private Type _entityCreateValidator = null;
		private Type _entityEditValidator = null;

		private Type _crudOperation;

		private readonly IList<Type> _entityCorrectorTypes = new List<Type>();

		private readonly Type _tenantedEntityCorrectorType = null;

		private Type _entityEditSchemaProviderType = null;

		private CrudOperationParameters _crudOperationParameters;

		private CrudCommandsProducerParameters _crudCommandsProducerParameters= new CrudCommandsProducerParameters();

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public CrudResourcePointConfigurator()
		{
			if (typeof(TEntity).HasImplementation<ITenantedEntity>())
			{
				_tenantedEntityCorrectorType = typeof(TenantedEntityCorrector<TCreateEntityDto,TEditEntityDto, TKey>);
				_entityCorrectorTypes.Add(_tenantedEntityCorrectorType);
			}
		}

		public CrudResourcePointConfigurator<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey> AddModifyRoles(params ProjectRoles[] roles)
		{
			AppendRoles(_modifyRoles, roles);
			return this;
		}

		public CrudResourcePointConfigurator<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey> UseValidators<TCreateValidator, TEditValidator>()
			where TCreateValidator : BaseEntityDtoValidator<TCreateEntityDto>
			where TEditValidator : BaseEntityDtoValidator<TEditEntityDto>
		{
			_entityCreateValidator = typeof(TCreateValidator);
			_entityEditValidator = typeof(TEditValidator);
			return this;
		}

		public CrudResourcePointConfigurator<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey> UseCrudOperation<TCrudOperation, TOperationParameters>(Action<TOperationParameters> cfg)
			where TCrudOperation : ICrudOperation<TEntity, TCreateEntityDto,TEditEntityDto, TKey>
			where TOperationParameters : CrudOperationParameters,new()
		{
			var parameters = new TOperationParameters();
			cfg(parameters);
			_crudOperation = typeof(TCrudOperation);
			_crudOperationParameters = parameters;
			return this;
		}

		public CrudResourcePointConfigurator<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey> UseEntityContextCrudOperation<TCrudOperation>(Action<EntityContextCrudOperationParameters> cfg)
			where TCrudOperation : EntityContextCrudOperation<TEntity, TCreateEntityDto, TEditEntityDto, TKey>
		{
			var parameters = new EntityContextCrudOperationParameters();
			cfg(parameters);
			_crudOperation = typeof(TCrudOperation);
			_crudOperationParameters = parameters;
			return this;
		}

		public CrudResourcePointConfigurator<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey> AddEntityCorrector<TCorrector>()
			where TCorrector : IEntityCorrector<TEntity, TCreateEntityDto, TEditEntityDto, TKey>
		{
			_entityCorrectorTypes.Add(typeof(TCorrector));
			return this;
		}

		public CrudResourcePointConfigurator<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey> UseEntityEditSchemaProvider<TProvider>()
			where TProvider : IEntityFormSchemaProvider<TCreateEntityDto, TEditEntityDto>
		{
			_entityEditSchemaProviderType = typeof(TProvider);
			return this;
		}

		public CrudResourcePointConfigurator<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey> AddAfterCreateCrudCommand<TAfterEntityCreatedCommand>(string queueName)
			where TAfterEntityCreatedCommand: class,IAfterEntityCreatedCommand<TCreateEntityDto>
		{
			_crudCommandsProducerParameters.AfterEntityCreatedCommands.Add(new CrudCommandParameters{CommandType = typeof(TAfterEntityCreatedCommand),QueueName = queueName});;
			return this;
		}

		protected override Type GetResourcePointFactoryType(List<Type> listBaseTypes)
		{
			listBaseTypes.Add(typeof(ResourcePointControllerFactory<TEntity, TFilterableEntity, TKey>));
			return typeof(CrudResourcePointControllerFactory<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey>);
		}

		protected override ControllerFactoryParameters CreateFactoryParameters()
		{
			return new CrudControllerFactoryParameters();
		}

		protected override void FillControllerFactoryParameters(ControllerFactoryParameters parameters)
		{
			var typedParameters = (CrudControllerFactoryParameters)parameters;
			base.FillControllerFactoryParameters(typedParameters);


			var controllerType = typeof(CrudResourcePointController<,,,,>).MakeGenericType(typeof(TEntity), typeof(TCreateEntityDto), typeof(TEditEntityDto),
				typeof(TFilterableEntity), typeof(TKey));
			typedParameters.ControllerType = controllerType;
			typedParameters.CreateEntityDtoValidatorType = _entityCreateValidator;
			typedParameters.EditEntityDtoValidatorType = _entityEditValidator;
			
			typedParameters.CrudOperationType = _crudOperation;
			typedParameters.CrudOperationParameters = _crudOperationParameters;
			typedParameters.CrudCommandsProducerParameters = _crudCommandsProducerParameters;

			typedParameters.EntityFormSchemaProviderType = _entityEditSchemaProviderType;
			typedParameters.AccessRuleMap = new AccessRuleMap(_readRoles.ToArray(),_modifyRoles.ToArray());
			typedParameters.EntityCorrectorTypes = _entityCorrectorTypes;
		}

		protected override void RegisterTypes(ContainerBuilder builder)
		{
			builder.RegisterType(_entityCreateValidator).AsSelf();
			builder.RegisterType(_entityEditValidator).AsSelf();
			builder.RegisterType(_crudOperation).AsSelf();

			if (_tenantedEntityCorrectorType!=null)
			{
				builder.RegisterType(_tenantedEntityCorrectorType).AsSelf().SingleInstance();
			}

			if (_entityEditSchemaProviderType!=null)
			{
				builder.RegisterType(_entityEditSchemaProviderType).AsSelf().SingleInstance();
			}

			foreach (var afterEntityCreatedCommand in _crudCommandsProducerParameters.AfterEntityCreatedCommands)
			{
				builder.RegisterType(afterEntityCreatedCommand.CommandType).AsSelf();
			}
		}
	}
}
