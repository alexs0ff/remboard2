using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints
{
	public interface IResourcePointControllerDescriptor
	{
		IResourcePointDescriptor ResourcePoint { get; }

		Type ControllerType { get; }

		string EntityName { get; }
	}
}
