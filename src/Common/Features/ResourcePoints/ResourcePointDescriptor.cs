using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Features.ResourcePoints
{
	public class ResourcePointDescriptor: IResourcePointDescriptor
	{
		public string EntityName { get; }
		public string EntityPluralName { get; }
		public Type EntityTypeInfo { get; }
		public Type EntityDtoTypeInfo { get; }
		public Type FilterableEntityTypeInfo { get; }
		public Type KeyType { get; }
	}
}
