using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Common.Features.ResourcePoints
{
	public class ResourcePointControllerRegistry
	{
		private readonly IReadOnlyDictionary<string,IResourcePointControllerDescriptor> _resourcePointControllerDescriptors;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public ResourcePointControllerRegistry(IEnumerable<IResourcePointControllerDescriptor> resourcePointControllerDescriptors)
		{
			_resourcePointControllerDescriptors = new ReadOnlyDictionary<string, IResourcePointControllerDescriptor>(resourcePointControllerDescriptors.ToDictionary(descriptor => descriptor.EntityName)); ;
		}

		public IEnumerable<IResourcePointControllerDescriptor> ResourcePointControllerDescriptors => _resourcePointControllerDescriptors.Values;

		public IResourcePointControllerDescriptor this[string name] => _resourcePointControllerDescriptors[name];
	}
}
