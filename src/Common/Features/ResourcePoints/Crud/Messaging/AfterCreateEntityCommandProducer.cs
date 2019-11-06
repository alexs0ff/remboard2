using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common.MessagingQueue.Producers;

namespace Common.Features.ResourcePoints.Crud.Messaging
{
	public class AfterCreateEntityCommandProducer<TCreateEntityDto, TAfterEntityCreatedCommand>: IAfterCreateEntityCommandProducer<TCreateEntityDto>
		where TCreateEntityDto : class
		where TAfterEntityCreatedCommand: IAfterEntityCreatedCommand<TCreateEntityDto>
	{
		private readonly CrudCommandParameters _parameters;

		private readonly IComponentContext _context;

		private readonly CommandProducerBase _producer;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public AfterCreateEntityCommandProducer(CrudCommandParameters parameters,IComponentContext context, CommandProducerBase producer)
		{
			_parameters = parameters;
			_context = context;
			_producer = producer;
		}

		public async Task Send(TCreateEntityDto entityDto)
		{
			var command = (TAfterEntityCreatedCommand)_context.Resolve(_parameters.CommandType);

			command.CorrelationId = Guid.NewGuid();
			command.CreatedEntityDto = entityDto;

			await _producer.Send(_parameters.QueueName, command);
		}
	}
}
