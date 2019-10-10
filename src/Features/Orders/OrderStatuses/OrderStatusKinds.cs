using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Orders.OrderStatuses
{
	public enum OrderStatusKinds : long
	{

		[Description("Новые")]
		New = 1,

		[Description("На исполнении")]
		OnWork = 2,
		
		[Description("Отложенные")]
		Suspended = 3,
		
		[Description("Исполненные")]
		Completed = 4,
		
		[Description("Закрытые")]
		Closed = 5,
	}
}
