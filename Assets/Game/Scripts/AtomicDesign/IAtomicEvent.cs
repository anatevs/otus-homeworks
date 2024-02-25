using System;

public interface IAtomicEvent : IAtomicAction
{
    public void Subscribe(Action action);
    public void Unsubscribe(Action action);
}

public interface IAtomicEvent<T> : IAtomicAction<T>
{
    public void Subscribe(Action<T> action);
    public void Unsubscribe(Action<T> action);
}
