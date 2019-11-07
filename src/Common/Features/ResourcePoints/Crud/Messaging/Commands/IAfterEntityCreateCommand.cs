using Common.MessagingQueue;

namespace Common.Features.ResourcePoints.Crud.Messaging.Commands
{
	public interface IAfterEntityCreateCommand<TCreateEntityDto,TKey> :IQueueMessageBase
		where TCreateEntityDto : class
		where TKey:struct
	{
		TCreateEntityDto CreatedEntityDto { get; set; }
		
		TKey Id { get; set; }
	}
}
