using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Common.Features.ResourcePoints
{
	public class ResourcePointControllerRegistry
	{
		private readonly IReadOnlyDictionary<string,IResourcePointControllerFactory> _resourcePointControllerDescriptors;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public ResourcePointControllerRegistry(IEnumerable<IResourcePointControllerFactory> resourcePointControllerDescriptors)
		{
			_resourcePointControllerDescriptors = new ReadOnlyDictionary<string, IResourcePointControllerFactory>(resourcePointControllerDescriptors.ToDictionary(descriptor => descriptor.EntityName)); ;
		}

		public IEnumerable<IResourcePointControllerFactory> ResourcePointControllerDescriptors => _resourcePointControllerDescriptors.Values;

		public IResourcePointControllerFactory this[string name] => _resourcePointControllerDescriptors[name];
	}
}
