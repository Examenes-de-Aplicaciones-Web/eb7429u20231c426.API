using Cortex.Mediator.Notifications;
using eb7429u20231c426.API.Shared.Domain.Model.Events;

namespace eb7429u20231c426.API.Shared.Application.Internal.EvenHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
    
}