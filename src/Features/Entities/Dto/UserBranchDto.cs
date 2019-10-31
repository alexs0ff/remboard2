using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
	//https://stackoverflow.com/a/42087190
	//It is not a good practice to have reference in one DTO to second DTO because it cause many troubles 
	public class UserBranchDto
	{
		public Guid BranchId { get; set; }

		public string BranchTitle { get; set; }
	}
}
