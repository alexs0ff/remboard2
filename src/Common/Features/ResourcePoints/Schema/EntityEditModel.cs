using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Features.ResourcePoints.Schema
{
	public class EntityEditModel
	{
		public string EntitiesName { get; set; }

		public string Title { get; set; }

		public RemoveDialogOptions RemoveDialog { get; set; }

		public Dictionary<string, FormLayout> Layouts { get; set; }
	}
}
