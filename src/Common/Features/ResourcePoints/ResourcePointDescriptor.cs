using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Features.ResourcePoints
{
	public class ResourcePointDescriptor: IResourcePointDescriptor
	{
		public string EntityName { get; set; }
		public string EntityPluralName { get; set; }
		public Type EntityTypeInfo { get; set; }
		public Type EntityDtoTypeInfo { get; set; }
		public Type FilterableEntityTypeInfo { get; set; }

		public Type KeyType { get; set; }
	}
}
