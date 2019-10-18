using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Common.Features.BaseEntity;
using FluentValidation;

namespace Common.Features.ResourcePoints.Crud
{
	public class CrudResourcePointControllerFactory<TEntity, TEntityDto, TFilterableEntity, TKey>: ResourcePointControllerFactory<TEntity, TEntityDto, TFilterableEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TEntityDto : class
		where TKey : struct
	{
		private readonly CrudControllerFactoryParameters _parameters;

		public CrudResourcePointControllerFactory(CrudControllerFactoryParameters parameters, IComponentContext context) : base(parameters, context)
		{
			_parameters = parameters;
		}

		public ICrudOperation<TEntity, TEntityDto, TKey> GetCrudOperation()
		{
			return (ICrudOperation<TEntity, TEntityDto, TKey>)context.Resolve(_parameters.CrudOperationType);
		}


		public IValidator<TEntityDto> GetValidator()
		{
			return (IValidator<TEntityDto>) context.Resolve(_parameters.EntityValidatorType);
		}

		public List<IEntityCorrector<TEntity, TEntityDto, TKey>> GetCorrectors()
		{
			var correctors =new List<IEntityCorrector<TEntity, TEntityDto, TKey>>();

			foreach (var parametersEntityCorrectorType in _parameters.EntityCorrectorTypes)
			{
				var corrector = (IEntityCorrector <TEntity, TEntityDto, TKey>)context.Resolve(parametersEntityCorrectorType);
				correctors.Add(corrector);
			}

			return correctors;
		}
	}
}
