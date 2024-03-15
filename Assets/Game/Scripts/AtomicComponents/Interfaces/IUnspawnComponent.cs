using System;

public interface IUnspawnComponent
{
    public event Action<Entity> OnUnspawn;
}