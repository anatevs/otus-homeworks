using UnityEngine;

public class MakeCollisionDamageMechanic
{
    private readonly IAtomicEvent _onCounted;
    private readonly IAtomicEvent _onResetCounter;
    private readonly IAtomicVariable<float> _count;
    private readonly IAtomicValue<int> _damage;
        
    private bool _isMakingDamage;
    private Player _player;

    public MakeCollisionDamageMechanic(IAtomicEvent onCounted,
        IAtomicEvent onResetCounter, IAtomicVariable<float> count,
        IAtomicValue<int> damage)
    {
        _onCounted = onCounted;
        _onResetCounter = onResetCounter;
        _count = count;
        _damage = damage;

        _isMakingDamage = false;
        _player = null;
    }

    public void Update()
    {
        if (!_isMakingDamage)
        {
            _onResetCounter.Invoke();
        }
        else
        {
            return;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out _player))
        {
            _isMakingDamage = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _player.gameObject)
        {
            _isMakingDamage = false;
        }
    }

    public void OnEnable()
    {
        _onCounted.Subscribe(MakeDamage);
    }

    public void OnDisable()
    {
        _onCounted.Unsubscribe(MakeDamage);
    }

    private void MakeDamage()
    {
        _player.OnDamage.Invoke(_damage.Value);
    }
}