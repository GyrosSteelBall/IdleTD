using System;
using System.Collections.Generic;

public class EventBus : Singleton<EventBus>
{
    private Dictionary<Type, Delegate> eventHandlers = new Dictionary<Type, Delegate>();

    public void Subscribe<T>(Action<T> handler)
    {
        Type eventType = typeof(T);
        if (eventHandlers.ContainsKey(eventType))
        {
            eventHandlers[eventType] = Delegate.Combine(eventHandlers[eventType], handler);
        }
        else
        {
            eventHandlers[eventType] = handler;
        }
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
        Type eventType = typeof(T);
        if (eventHandlers.ContainsKey(eventType))
        {
            Delegate currentDel = eventHandlers[eventType];
            currentDel = Delegate.Remove(currentDel, handler);

            if (currentDel != null)
            {
                eventHandlers[eventType] = currentDel;
            }
            else
            {
                eventHandlers.Remove(eventType);
            }
        }
    }

    public void Publish<T>(T eventData)
    {
        Type eventType = typeof(T);
        if (eventHandlers.ContainsKey(eventType))
        {
            ((Action<T>)eventHandlers[eventType])(eventData);
        }
    }
}
