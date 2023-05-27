namespace Plitkarka.Infrastructure.Interfaces;

public interface IMessagingService
{
   void Send<TSender>(TSender sender, string messagingEvent) where TSender : class;
   void Send<TSender, TArgs>(TSender sender, string messagingEvent, TArgs args) where TSender : class;

   void Subscribe<TSender>(object subscriber, string messagingEvent, Action<TSender> messageReceivedCallback) where TSender : class;
   void Subscribe<TSender, TArgs>(object subscriber, string messagingEvent, Action<TSender, TArgs> callback) where TSender : class;

   void Unsubscribe<TSender>(object subscriber, string messagingEvent) where TSender : class;
   void Unsubscribe<TSender, TArgs>(object subscriber, string messagingEvent) where TSender : class;
}
