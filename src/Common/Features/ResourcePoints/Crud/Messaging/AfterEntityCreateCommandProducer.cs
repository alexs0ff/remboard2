using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common.Features.ResourcePoints.Crud.Messaging.Commands;
using Common.MessagingQueue.Producers;

namespace Common.Features.ResourcePoints.Crud.Messaging
{
	public class AfterEntityCreateCommandProducer<TCreateEntityDto, TAfterEntityCreatedCommand, TKey> : IAfterEntityCreateCommandProducer<TCreateEntityDto,TKey>
		where TCreateEntityDto : class
		where TAfterEntityCreatedCommand: IAfterEntityCreateCommand<TCreateEntityDto,TKey>
		where TKey:struct
	{
		private readonly CrudCommandParameters _parameters;

		private readonly IComponentContext _context;

		private readonly CommandProducerBase _producer;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public AfterEntityCreateCommandProducer(CrudCommandParameters parameters,IComponentContext context, CommandProducerBase producer)
		{
			_parameters = parameters;
			_context = context;
			_producer = producer;
		}

		public async Task Send(TCreateEntityDto entityDto,TKey id)
		{
			var command = (TAfterEntityCreatedCommand)_context.Resolve(_parameters.CommandType);

			command.CorrelationId = Guid.NewGuid();
			command.CreatedEntityDto = entityDto;
			command.Id = id;

			await _producer.Send(_parameters.QueueName, command);
		}
	}
}
