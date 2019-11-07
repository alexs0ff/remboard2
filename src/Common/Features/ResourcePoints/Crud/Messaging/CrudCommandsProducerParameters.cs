using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Features.ResourcePoints.Crud.Messaging
{
	public class CrudCommandsProducerParameters
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public CrudCommandsProducerParameters()
		{
			AfterEntityCreatedCommands = new List<CrudCommandParameters>();
			AfterEntityEditCommands = new List<CrudCommandParameters>();
		}

		public List<CrudCommandParameters> AfterEntityCreatedCommands { get; }
		public List<CrudCommandParameters> AfterEntityEditCommands { get; }
	}
}
