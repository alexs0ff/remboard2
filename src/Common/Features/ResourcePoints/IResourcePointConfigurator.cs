using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Common.Features.ResourcePoints
{
	public interface IResourcePointConfigurator
	{
		void Finish(ContainerBuilder builder);
	}
}
