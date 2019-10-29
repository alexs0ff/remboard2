using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Common.Features.ResourcePoints.Schema
{
	public class FormLayoutRowContent
	{
		[JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
		public FormLayoutRowContentKind Kind { get; set; }
	}
}
