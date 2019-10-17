﻿using System;
using System.Collections.Generic;
using System.Text;
using Common.Features.Cruds;

namespace Common.Features.ResourcePoints
{
	public interface IResourcePointControllerFactory
	{
		IResourcePointDescriptor ResourcePoint { get; }

		Type ControllerType { get; }

		string EntityName { get; }

		AccessRuleMap AccessRules { get; }
	}
}
