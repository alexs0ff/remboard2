using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Common.Extensions;
using Common.FeatureEntities;
using Common.Features.BaseEntity;
using Common.Features.ResourcePoints.Crud;
using Common.Features.ResourcePoints.Schema;
using Common.Features.Tenant;

namespace Common.Features.ResourcePoints
{
	public class CrudResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey>: ResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TEntityDto : class
		where TKey : struct
	{
		private readonly HashSet<ProjectRoles> _modifyRoles = new HashSet<ProjectRoles>();

		private Type _entityValidator = null;

		private Type _crudOperation;

		private readonly IList<Type> _entityCorrectorTypes = new List<Type>();

		private readonly Type _tenantedEntityCorrectorType = null;

		private Type _entityEditSchemaProviderType = null;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public CrudResourcePointConfigurator()
		{
			if (typeof(TEntity).HasImplementation<ITenantedEntity>())
			{
				_tenantedEntityCorrectorType = typeof(TenantedEntityCorrector<TEntityDto, TKey>);
				_entityCorrectorTypes.Add(_tenantedEntityCorrectorType);
			}
		}

		public CrudResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey> AddModifyRoles(params ProjectRoles[] roles)
		{
			AppendRoles(_modifyRoles, roles);
			return this;
		}

		public CrudResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey> UseValidator<TValidator>()
			where TValidator : BaseEntityDtoValidator<TEntityDto>
		{
			_entityValidator = typeof(TValidator);
			return this;
		}

		public CrudResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey> UseCrudOperation<TCrudOperation>()
			where TCrudOperation : ICrudOperation<TEntity, TEntityDto, TKey>
		{
			_crudOperation = typeof(TCrudOperation);
			return this;
		}

		public CrudResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey> AddEntityCorrector<TCorrector>()
			where TCorrector : IEntityCorrector<TEntity, TEntityDto,TKey>
		{
			_entityCorrectorTypes.Add(typeof(TCorrector));
			return this;
		}

		public CrudResourcePointConfigurator<TEntity, TEntityDto, TFilterableEntity, TKey> UseEntityEditSchemaProvider<TProvider>()
			where TProvider : IEntityEditSchemaProvider<TEntityDto>
		{
			_entityEditSchemaProviderType = typeof(TProvider);
			return this;
		}

		protected override Type GetResourcePointFactoryType(List<Type> listBaseTypes)
		{
			listBaseTypes.Add(typeof(ResourcePointControllerFactory<TEntity, TEntityDto, TFilterableEntity, TKey>));
			return typeof(CrudResourcePointControllerFactory<TEntity, TEntityDto, TFilterableEntity, TKey>);
		}

		protected override ControllerFactoryParameters CreateFactoryParameters()
		{
			return new CrudControllerFactoryParameters();
		}

		protected override void FillControllerFactoryParameters(ControllerFactoryParameters parameters)
		{
			var typedParameters = (CrudControllerFactoryParameters)parameters;
			base.FillControllerFactoryParameters(typedParameters);


			var controllerType = typeof(CrudResourcePointController<,,,>).MakeGenericType(typeof(TEntity), typeof(TEntityDto),
				typeof(TFilterableEntity), typeof(TKey));
			typedParameters.ControllerType = controllerType;
			typedParameters.EntityValidatorType = _entityValidator;
			typedParameters.CrudOperationType = _crudOperation;
			typedParameters.EntityEditSchemaProviderType = _entityEditSchemaProviderType;
			typedParameters.AccessRuleMap = new AccessRuleMap(_readRoles.ToArray(),_modifyRoles.ToArray());
			typedParameters.EntityCorrectorTypes = _entityCorrectorTypes;
		}

		protected override void RegisterTypes(ContainerBuilder builder)
		{
			builder.RegisterType(_entityValidator).AsSelf();
			builder.RegisterType(_crudOperation).AsSelf();

			if (_tenantedEntityCorrectorType!=null)
			{
				builder.RegisterType(_tenantedEntityCorrectorType).AsSelf().SingleInstance();
			}

			if (_entityEditSchemaProviderType!=null)
			{
				builder.RegisterType(_entityEditSchemaProviderType).AsSelf().SingleInstance();
			}
		}
	}
}
