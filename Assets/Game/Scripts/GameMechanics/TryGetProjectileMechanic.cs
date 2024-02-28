using UnityEngine;

public class TryGetProjectileMechanic
{
    private readonly IAtomicEvent<Vector3> _fireEvent;
    private readonly IAtomicEvent<Vector3> _startFireEvent;
    private readonly IAtomicVariable<int> _weaponMagazine;
    private readonly int _shootAmount;

    public TryGetProjectileMechanic(IAtomicEvent<Vector3> onFireEvent, IAtomicEvent<Vector3> startFireEvent, IAtomicVariable<int> weaponMagazine)
    {
        _fireEvent = onFireEvent;
        _startFireEvent = startFireEvent;
        _weaponMagazine = weaponMagazine;
        _shootAmount = 1;
    }

    public void OnEnable()
    {
        _fireEvent.Subscribe(StartFire);
    }

    public void OnDisable()
    {
        _fireEvent.Unsubscribe(StartFire);
    }

    private void StartFire(Vector3 shootDirection)
    {
        if (_weaponMagazine.Value <= 0)
        {
            return;
        }
        else
        {
            _weaponMagazine.Value -= _shootAmount;
            _startFireEvent.Invoke(shootDirection);
            Debug.Log("magazine shoot");
        }
    }
}