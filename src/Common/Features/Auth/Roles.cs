using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Common.Features.Auth
{
	public static class CrudOperations
	{
		public static OperationAuthorizationRequirement Create =
			new OperationAuthorizationRequirement { Name = nameof(Create) };
		public static OperationAuthorizationRequirement Read =
			new OperationAuthorizationRequirement { Name = nameof(Read) };
		public static OperationAuthorizationRequirement Update =
			new OperationAuthorizationRequirement { Name = nameof(Update) };
		public static OperationAuthorizationRequirement Delete =
			new OperationAuthorizationRequirement { Name = nameof(Delete) };
	}
}
