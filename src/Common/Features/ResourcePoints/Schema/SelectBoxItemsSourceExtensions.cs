using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Extensions;

namespace Common.Features.ResourcePoints.Schema
{
	public static class SelectBoxItemsSourceExtensions
	{
		public static SelectBoxItemsSource<long> SourceFromEnum<TEntity, TEnum>()
			where TEntity : Common.Features.BasePermissibleValue<TEnum>, new()
			where TEnum : Enum
		{
			var result = new SelectBoxItemsSource<long>();
			var entities = EnumExtensions.EnumToEntities<TEntity, TEnum>();

			result.Kind = SelectBoxItemsSourceKind.Items;
			
			result.Items = entities.Select(i=>new KeyValuePair<long, string>(Convert.ToInt64(i.Id),i.Name)).ToArray();

			return result;

		}
	}
}
