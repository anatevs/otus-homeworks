using UnityEngine;

public class DestroyMechanic
{
    private readonly GameObject _gameObject;

    private readonly AtomicVariable<bool> _death;

    public DestroyMechanic(GameObject gameObject, AtomicVariable<bool> isDeath)
    {
        _gameObject = gameObject;
        _death = isDeath;
    }

    public void OnEnable()
    {
        _death.Subscribe(OnDeath);
    }

    public void OnDisable()
    {
        _death.Unsubscribe(OnDeath);
    }

    private void OnDeath(bool isDead)
    {
        if (!isDead)
        {
            return;
        }
        else
        {
            Object.Destroy(_gameObject);
        }
    }
}