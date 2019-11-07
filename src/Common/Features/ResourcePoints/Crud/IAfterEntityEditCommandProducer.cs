using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Features.ResourcePoints.Crud
{
	public interface IAfterEntityEditCommandProducer<TEditEntityDto, TKey>
		where TEditEntityDto : class
		where TKey : struct
	{
		Task Send(TEditEntityDto entityDto, TKey id);
	}
}
