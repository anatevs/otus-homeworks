using System;

public class AtomicEvent<T>
{
    private event Action<T> OnEvent;

    public void Invoke(T eventData)
    {
        OnEvent?.Invoke(eventData);
    }

    public void Subscribe(Action<T> action)
    {
        OnEvent += action;
    }

    public void Unsubscribe(Action<T> action)
    {
        OnEvent -= action;
    }
}

public class AtomicEvent
{
    private event Action OnEvent;

    public void Invoke()
    {
        OnEvent?.Invoke();
    }

    public void Subscribe(Action action)
    {
        OnEvent += action;
    }

    public void Unsubscribe(Action action)
    {
        OnEvent -= action;
    }
}