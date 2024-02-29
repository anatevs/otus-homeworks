using UnityEngine;

public class ShootRotationMechanic
{
    private readonly IAtomicEvent<Vector3> _startFire;
    private readonly IAtomicAction _shootAction;
    private readonly IAtomicValue<Vector3> _moveDirection;
    private readonly IAtomicVariable<Vector3> _rotDirection;
    private readonly IAtomicValue<bool> _isRotationDone;

    private Vector3 _beforeDirection;
    private bool _isShooting;

    public ShootRotationMechanic(IAtomicEvent<Vector3> startFire,
        IAtomicAction shootAction, IAtomicValue<Vector3> moveDirection,
        IAtomicVariable<Vector3> rotDirection, IAtomicValue<bool> isRotationDone)
    {
        _startFire = startFire;
        _shootAction = shootAction;
        _moveDirection = moveDirection;
        _rotDirection = rotDirection;
        _isRotationDone = isRotationDone;
    }

    public void OnEnable()
    {
        _startFire.Subscribe(StartFireProcess);
    }

    public void OnDisable()
    {
        _startFire.Unsubscribe(StartFireProcess);
    }

    public void Update()
    {
        if (!_isShooting)
        {
            _rotDirection.Value = _moveDirection.Value;
        }
        else
        {
            if (!_isRotationDone.Value)
            {
                return;
            }
            else
            {
                _shootAction.Invoke();
                _isShooting = false;
                _rotDirection.Value = _beforeDirection;
            }
        }
    }

    private void StartFireProcess(Vector3 shootDirection)
    {
        _isShooting = true;
        _beforeDirection = _rotDirection.Value;
        _rotDirection.Value = shootDirection;
    }
}