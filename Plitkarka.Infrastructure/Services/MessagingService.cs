using Plitkarka.Infrastructure.Interfaces;

namespace Plitkarka.Infrastructure.Services;

public class MessagingService : IMessagingService
{
    public void Send<TSender>(TSender sender, string messagingEvent)
        where TSender : class
    {
        MessagingCenter.Send<TSender>(sender, messagingEvent);
    }

    public void Send<TSender, TArgs>(
        TSender sender,
        string messagingEvent,
        TArgs args)
        where TSender : class
    {
        MessagingCenter.Send<TSender, TArgs>(
            sender,
            messagingEvent,
            args);
    }

    public void Subscribe<TSender>(
        object subscriber,
        string messagingEvent,
        Action<TSender> messageReceivedCallback)
        where TSender : class
    {
        MessagingCenter.Subscribe<TSender>(
            subscriber,
            messagingEvent,
            messageReceivedCallback);
    }

    public void Subscribe<TSender, TArgs>(
        object subscriber,
        string messagingEvent,
        Action<TSender, TArgs> callback) where TSender : class
    {
        MessagingCenter.Subscribe<TSender, TArgs>(
            subscriber,
            messagingEvent,
            callback);
    }

    public void Unsubscribe<TSender>(object subscriber, string messagingEvent)
        where TSender : class
    {
        MessagingCenter.Unsubscribe<TSender>(subscriber, messagingEvent);
    }

    public void Unsubscribe<TSender, TArgs>(object subscriber, string messagingEvent)
        where TSender : class
    {
        MessagingCenter.Unsubscribe<TSender, TArgs>(subscriber, messagingEvent);
    }
}