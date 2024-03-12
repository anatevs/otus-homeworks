using System;

public interface IHPComponent
{
    public event Action<int> OnHPChanged;

    public int HP { get; }
}