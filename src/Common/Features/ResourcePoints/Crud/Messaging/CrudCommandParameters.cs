using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Crud.Messaging
{
	public class CrudCommandParameters
	{
		public Type CommandType { get; set; }

		public string QueueName { get; set; }
	}
}
