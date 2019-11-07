using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common.Features.ResourcePoints.Crud.Messaging.Commands;
using Common.MessagingQueue.Producers;

namespace Common.Features.ResourcePoints.Crud.Messaging
{
	public class
		AfterEntityEditCommandProducer<TEditEntityDto, TAfterEntityEditCommand, TKey> : IAfterEntityEditCommandProducer<TEditEntityDto, TKey>
		where TEditEntityDto : class
		where TAfterEntityEditCommand : IAfterEntityEditCommand<TEditEntityDto, TKey>
		where TKey : struct
	{
		private readonly CrudCommandParameters _parameters;

		private readonly IComponentContext _context;

		private readonly CommandProducerBase _producer;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public AfterEntityEditCommandProducer(CrudCommandParameters parameters, IComponentContext context,
			CommandProducerBase producer)
		{
			_parameters = parameters;
			_context = context;
			_producer = producer;
		}

		public async Task Send(TEditEntityDto entityDto,TKey id)
		{
			var command = (TAfterEntityEditCommand) _context.Resolve(_parameters.CommandType);

			command.CorrelationId = Guid.NewGuid();
			command.EditEntityDto = entityDto;
			command.Id = id;

			await _producer.Send(_parameters.QueueName, command);
		}
	}
}
