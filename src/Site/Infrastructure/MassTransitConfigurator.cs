using System.Linq;
using Autofac;
using Common.MessagingQueue;
using Common.MessagingQueue.Consumers;
using Common.MessagingQueue.Producers;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.Extensions.DependencyInjection;

namespace Remboard.Infrastructure
{
	public static class MassTransitConfigurator
	{
		public static void Configure(IServiceCollection services)
		{
			services.AddSingleton<IQueueUriBuilder, InMemoryQueueUriBuilder>();

			services.AddMassTransit(provider =>Bus.Factory.CreateUsingInMemory(cfg =>
			{
				var entityQueuesRegistry = provider.GetService<EntityQueuesRegistry>();

				if (!entityQueuesRegistry.ReceiveEndpointDescriptors.Any())
				{
					return;
				}

				cfg.Host(hCfg =>
				{
					hCfg.TransportConcurrencyLimit = 10;
				});

				foreach (var receiveEndpointDescriptor in entityQueuesRegistry.ReceiveEndpointDescriptors)
				{
					cfg.ReceiveEndpoint(receiveEndpointDescriptor.QueueName, ep =>
					{
						receiveEndpointDescriptor.Config?.Invoke(ep);

						foreach (var consumerDescriptor in receiveEndpointDescriptor.ConsumerDescriptors)
						{
							ep.ConfigureConsumer(provider,consumerDescriptor.ConsumerType);
						}

					});
				}
			}));
		}
	}
}
