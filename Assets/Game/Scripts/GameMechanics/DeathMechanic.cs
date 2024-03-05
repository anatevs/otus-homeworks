using UnityEngine;

public class DeathMechanic
{
    private readonly AtomicVariable<bool> _isDeath;
    private readonly IAtomicValue<int> _hp;

    public DeathMechanic(AtomicVariable<bool> isDeath, IAtomicValue<int> hp)
    {
        _isDeath = isDeath;
        _hp = hp;
    }

    public void Update()
    {
        if (_hp.Value > 0)
        {
            return;
        }
        else
        {
            if (_isDeath.Value)
            {
                return;
            }
            else
            {
                _isDeath.Value = true;
            }
        }
    }
}