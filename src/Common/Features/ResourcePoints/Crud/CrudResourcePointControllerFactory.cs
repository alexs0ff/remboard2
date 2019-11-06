using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Common.Features.ResourcePoints.Crud.Messaging;
using Entities;
using Common.Features.ResourcePoints.Schema;
using FluentValidation;

namespace Common.Features.ResourcePoints.Crud
{
	public class CrudResourcePointControllerFactory<TEntity, TCreateEntityDto, TEditEntityDto, TFilterableEntity, TKey>: ResourcePointControllerFactory<TEntity, TFilterableEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TFilterableEntity : class
		where TCreateEntityDto : class
		where TEditEntityDto : class
		where TKey : struct
	{
		private readonly CrudControllerFactoryParameters _parameters;

		public CrudResourcePointControllerFactory(CrudControllerFactoryParameters parameters, IComponentContext context) : base(parameters, context)
		{
			_parameters = parameters;
		}

		public ICrudOperation<TEntity, TCreateEntityDto, TEditEntityDto, TKey> GetCrudOperation()
		{
			return (ICrudOperation<TEntity, TCreateEntityDto, TEditEntityDto, TKey>)context.Resolve(_parameters.CrudOperationType, new NamedParameter("parameters", _parameters.CrudOperationParameters));
		}


		public IValidator<TCreateEntityDto> GetCreateDtoValidator()
		{
			return (IValidator<TCreateEntityDto>) context.Resolve(_parameters.CreateEntityDtoValidatorType);
		}

		public IValidator<TEditEntityDto> GetEditDtoValidator()
		{
			return (IValidator<TEditEntityDto>)context.Resolve(_parameters.EditEntityDtoValidatorType);
		}

		public List<IEntityCorrector<TEntity, TCreateEntityDto, TEditEntityDto, TKey>> GetCorrectors()
		{
			var correctors =new List<IEntityCorrector<TEntity, TCreateEntityDto, TEditEntityDto, TKey>>();

			foreach (var parametersEntityCorrectorType in _parameters.EntityCorrectorTypes)
			{
				var corrector = (IEntityCorrector <TEntity, TCreateEntityDto, TEditEntityDto, TKey>)context.Resolve(parametersEntityCorrectorType);
				correctors.Add(corrector);
			}

			return correctors;
		}

		public IEntityFormSchemaProvider<TCreateEntityDto, TEditEntityDto> GetEntityFormSchemaProvider()
		{
			return (IEntityFormSchemaProvider<TCreateEntityDto, TEditEntityDto>)context.Resolve(_parameters.EntityFormSchemaProviderType);
		}

		public List<IAfterCreateEntityCommandProducer<TCreateEntityDto>> GetAfterCreateEntityCommandProducersOrNull()
		{
			if (!_parameters.CrudCommandsProducerParameters.AfterEntityCreatedCommands.Any())
			{
				return null;
			}

			var result = new List<IAfterCreateEntityCommandProducer<TCreateEntityDto>>();

			foreach (var afterEntityCreatedCommand in _parameters.CrudCommandsProducerParameters.AfterEntityCreatedCommands)
			{
				var producerType = typeof(AfterCreateEntityCommandProducer<,>).MakeGenericType(typeof(TCreateEntityDto,afterEntityCreatedCommand.CommandType);

				var producer = (IAfterCreateEntityCommandProducer<TCreateEntityDto>)context.Resolve(producerType);
				result.Add(producer);
			}

			return result;
		}
	}
}
