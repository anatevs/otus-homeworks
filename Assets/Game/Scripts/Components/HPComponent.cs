using System;

public class HPComponent : IHPComponent
{
    public event Action<int> OnHPChanged;
    private readonly IAtomicVariable<int> _hp;

    public HPComponent(IAtomicVariable<int> hp)
    {
        _hp = hp;
    }


    public int GetHP()
    {
        return _hp.Value;
    }
}