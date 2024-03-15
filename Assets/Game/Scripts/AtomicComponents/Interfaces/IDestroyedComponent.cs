using System;

public interface IDestroyedComponent
{
    public event Action<bool> OnDestroyed;
}