using UnityEngine;

public class CounterMechanic
{
    private readonly IAtomicAction _onCounted;
    private readonly IAtomicEvent _onReset;
    private readonly IAtomicValue<float> _count;
    private float _timer;

    public CounterMechanic(IAtomicAction onCounted, IAtomicEvent onReset, IAtomicValue<float> count)
    {
        _onCounted = onCounted;
        _onReset = onReset;
        _count = count;
        _timer = count.Value;
    }

    public void Update()
    {
        _timer -= Time.deltaTime;
        if ( _timer > 0 )
        {
            return;
        }
        else
        {
            _timer = _count.Value;
            _onCounted?.Invoke();
        }
    }

    public void OnEnable()
    {
        _onReset.Subscribe(Reset);
    }

    public void OnDisable()
    {
        _onReset.Unsubscribe(Reset);
    }

    private void Reset()
    {
        _timer = _count.Value;
    }
}