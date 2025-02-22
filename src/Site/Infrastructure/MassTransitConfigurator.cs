﻿using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Common.MessagingQueue;
using Common.MessagingQueue.Consumers;
using Common.MessagingQueue.Producers;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using MassTransit.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MassTransit.ExtensionsDependencyInjectionIntegration;

namespace Remboard.Infrastructure
{
	public static class MassTransitConfigurator
	{

		public static void Configure(IServiceCollection services,IContainer temporaryContainer)
		{
			var entityQueuesRegistry = temporaryContainer.Resolve<EntityQueuesRegistry>();

			services.AddSingleton<IQueueUriBuilder, InMemoryQueueUriBuilder>();
			
			services.AddMassTransit(provider =>
			{
				//TODO: the logging should be None, must check with new version
				//var loggerFactory = provider.GetService<ILoggerFactory>();
				var loggerFactory = LoggerFactory.Create(loggingBuilder =>
				{
					loggingBuilder.SetMinimumLevel(LogLevel.None);
				});

				LogContext.ConfigureCurrentLogContext(loggerFactory);
				
				return Bus.Factory.CreateUsingInMemory(cfg =>
				{
					

					if (!entityQueuesRegistry.ReceiveEndpointDescriptors.Any())
					{
						return;
					}
					
					cfg.Host(hCfg => { hCfg.TransportConcurrencyLimit = 10; });

					foreach (var receiveEndpointDescriptor in entityQueuesRegistry.ReceiveEndpointDescriptors)
					{
						cfg.ReceiveEndpoint(receiveEndpointDescriptor.QueueName, ep =>
						{
							receiveEndpointDescriptor.Config?.Invoke(ep);

							foreach (var consumerDescriptor in receiveEndpointDescriptor.ConsumerDescriptors)
							{
								ep.ConfigureConsumer(provider, consumerDescriptor.ConsumerType);
							}
						});
					}
				});
			}, configurator =>
			{
				foreach (var consumerDescriptor in entityQueuesRegistry.ReceiveEndpointDescriptors.SelectMany(i=>i.ConsumerDescriptors))
				{
					configurator.AddConsumer(consumerDescriptor.ConsumerType);
				}
			});
		}
	}
}
