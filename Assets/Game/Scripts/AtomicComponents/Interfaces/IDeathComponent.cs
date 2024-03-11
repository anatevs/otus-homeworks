using System;

public interface IDeathComponent
{
    public event Action<bool> OnDeath;
}