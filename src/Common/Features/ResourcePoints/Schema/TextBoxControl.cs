using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Common.Features.ResourcePoints.Schema
{
	public class TextBoxControl:ControlBase
	{
		[JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
		public TextBoxControlKind Kind { get; set; }

		[JsonConverter(typeof(StringEnumConverter), typeof(CamelCaseNamingStrategy))]
		public ControlValueKind ValueKind { get; set; }

		public string Id { get; set; }

		public string Label { get; set; }

		public TextBoxControlValidators Validators { get; set; }

		public object Value { get; set; }

		public string Hint { get; set; }
	}
}
