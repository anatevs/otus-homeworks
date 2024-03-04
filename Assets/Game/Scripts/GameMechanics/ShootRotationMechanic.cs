using UnityEngine;

public class ShootRotationMechanic
{
    private readonly IAtomicEvent<Vector3> _startFireRotation;
    private readonly IAtomicAction _fireRequest;
    private readonly IAtomicEvent _shootIsDone;
    private readonly IAtomicVariable<bool> _isRotationDone;
    private readonly IAtomicValue<Vector3> _moveDirection;
    private readonly IAtomicVariable<Vector3> _rotDirection;

    private Vector3 _beforeDirection;
    private bool _isShooting;

    public ShootRotationMechanic(IAtomicEvent<Vector3> startFireRotation,
        IAtomicAction fireRequest, IAtomicEvent shootIsDone,
        IAtomicVariable<bool> isRotationDone,
        IAtomicValue<Vector3> moveDirection,
        IAtomicVariable<Vector3> rotDirection)
    {
        _startFireRotation = startFireRotation;
        _fireRequest = fireRequest;
        _shootIsDone = shootIsDone;
        _isRotationDone = isRotationDone;
        _moveDirection = moveDirection;
        _rotDirection = rotDirection;
    }

    public void OnEnable()
    {
        _startFireRotation.Subscribe(StartFireProcess);
        _isRotationDone.OnValueChanged += SentFireRequest;
        _shootIsDone.Subscribe(ReturnRotation);
    }

    public void OnDisable()
    {
        _startFireRotation.Unsubscribe(StartFireProcess);
        _isRotationDone.OnValueChanged -= SentFireRequest;
        _shootIsDone.Unsubscribe(ReturnRotation);
    }

    public void Update()
    {
        if (!_isShooting)
        {
            _rotDirection.Value = _moveDirection.Value;
        }
    }

    private void StartFireProcess(Vector3 shootDirection)
    {
        _isShooting = true;
        _beforeDirection = _rotDirection.Value;
        _rotDirection.Value = shootDirection;
    }

    private void SentFireRequest(bool rotationDone)
    {
        if (_isShooting && rotationDone)
        {
            _fireRequest.Invoke();
        }
    }

    private void ReturnRotation()
    {
        _isShooting = false;
        _rotDirection.Value = _beforeDirection;
    }
}