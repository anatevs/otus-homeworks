using UnityEngine;

public class RotationMechanic
{
    private readonly Transform _transform;
    private readonly IAtomicVariable<Vector3> _directionVector;
    private readonly IAtomicValue<float> _rotSpeed;
    private readonly IAtomicValue<bool> _canMove;
    private readonly IAtomicVariable<bool> _isRotationDone;

    private float _lerpPersent = 0f;
    private Vector3 _prevDirection;

    public RotationMechanic(Transform transform, 
        IAtomicVariable<Vector3> directionVector,
        IAtomicValue<float> rotSpeed, IAtomicValue<bool> canMove,
        IAtomicVariable<bool> isRotationDone)
    {
        _transform = transform;
        _directionVector = directionVector;
        _rotSpeed = rotSpeed;
        _canMove = canMove;
        _isRotationDone = isRotationDone;
    }

    public void OnEnable()
    {
        _directionVector.OnValueChanged += StartNewRotation;
    }

    public void OnDisable()
    {
        _directionVector.OnValueChanged -= StartNewRotation;
    }

    public void Update()
    {
        if (!_canMove.Value)
        {
            return;
        }
        else
        {
            if (_directionVector.Value != Vector3.zero)
            {
                Quaternion lookQuaternion = Quaternion.LookRotation(_directionVector.Value);
                _lerpPersent += Time.deltaTime * _rotSpeed.Value;
                _transform.rotation = Quaternion.Slerp(_transform.rotation, lookQuaternion, _lerpPersent);

                if (_lerpPersent >= 1f & !_isRotationDone.Value)
                {
                    _isRotationDone.Value = true;
                }
            }
        }

        _prevDirection = _directionVector.Value;
    }

    private void StartNewRotation(Vector3 _newDirection)
    {
        if (_prevDirection != _newDirection)
        {
            _lerpPersent = 0f;
            _isRotationDone.Value = false;
        }
    }
}