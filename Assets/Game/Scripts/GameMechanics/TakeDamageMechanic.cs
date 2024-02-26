using UnityEngine;

public class TakeDamageMechanic
{
    private readonly IAtomicEvent<int> _onTakeDamage;
    private readonly IAtomicVariable<int> _hp;

    public TakeDamageMechanic(IAtomicEvent<int> onTakeDamage, IAtomicVariable<int> hp)
    {
        _onTakeDamage = onTakeDamage;
        _hp = hp;
    }

    public void OnEnable()
    {
        _onTakeDamage.Subscribe(MakeDamage);
    }

    public void OnDesable()
    {
        _onTakeDamage.Unsubscribe(MakeDamage);
    }

    private void MakeDamage(int damage)
    {
        _hp.Value -= damage;
        _hp.Value = Mathf.Max(0, _hp.Value);
    }
}