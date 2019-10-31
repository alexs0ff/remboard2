using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
	public class UserBranch
	{
		public Guid UserId { get; set; }

		public Guid BranchId { get; set; }

		public User User { get; set; }
		public Branch Branch { get; set; }
	}
}
