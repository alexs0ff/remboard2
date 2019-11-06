using System;
using System.Collections.Generic;
using System.Text;

namespace Common.MessagingQueue.Consumers
{
	public class EntityQueuesRegistry
	{
		public IReadOnlyList<ReceiveEndpointDescriptor> ReceiveEndpointDescriptors { get;}

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public EntityQueuesRegistry(IEnumerable<ReceiveEndpointDescriptor> receiveEndpointDescriptors)
		{
			ReceiveEndpointDescriptors = new List<ReceiveEndpointDescriptor>(receiveEndpointDescriptors).AsReadOnly();
		}
	}
}
