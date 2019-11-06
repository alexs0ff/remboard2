using System;
using System.Collections.Generic;
using System.Text;

namespace Common.MessagingQueue.Consumers
{
	public class EntityQueuesRegistry
	{
		public IReadOnlyList<IReceiveEndpointDescriptor> ReceiveEndpointDescriptors { get;}

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public EntityQueuesRegistry(IEnumerable<IReceiveEndpointDescriptor> receiveEndpointDescriptors)
		{
			ReceiveEndpointDescriptors = new List<IReceiveEndpointDescriptor>(receiveEndpointDescriptors).AsReadOnly();
		}
	}
}
