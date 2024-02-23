using System;

public interface IAtomicEvent
{
    public void Subscribe(Action action);
    public void Unsubscribe(Action action);
}

public interface IAtomicEvent<T>
{
    public void Subscribe(Action<T> action);
    public void Unsubscribe(Action<T> action);
}
