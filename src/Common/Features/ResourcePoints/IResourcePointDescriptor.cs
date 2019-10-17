using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints
{
	public interface IResourcePointDescriptor
	{
		string EntityName { get; }
		string EntityPluralName { get; }
		Type EntityTypeInfo { get; }
		Type EntityDtoTypeInfo { get; }
		Type FilterableEntityTypeInfo { get; }
		Type KeyType { get; set; }
	}
}
