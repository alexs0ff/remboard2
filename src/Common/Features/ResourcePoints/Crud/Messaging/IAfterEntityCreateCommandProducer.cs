using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Features.ResourcePoints.Crud.Messaging
{
	public interface IAfterEntityCreateCommandProducer<TCreateEntityDto,TKey>
		where TCreateEntityDto : class
		where TKey:struct
	{
		Task Send(TCreateEntityDto entityDto,TKey id);
	}
}
