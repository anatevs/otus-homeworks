using UnityEngine;

public class RotationMechanic
{
    private readonly Transform _transform;
    private readonly IAtomicVariable<Vector3> _directionVector;
    private readonly IAtomicValue<float> _rotSpeed;
    private readonly IAtomicValue<bool> _canMove;

    private float _lerpPersent = 0f;
    private bool _isRotating = true;
    private Vector3 _prevDirection;

    public RotationMechanic(Transform transform, IAtomicVariable<Vector3> directionVector, IAtomicValue<float> rotSpeed, IAtomicValue<bool> canMove)
    {
        _transform = transform;
        _directionVector = directionVector;
        _rotSpeed = rotSpeed;
        _canMove = canMove;
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

                if (_lerpPersent >= 1f & _isRotating)
                {
                    _isRotating = false;
                    Debug.Log("rotation is done");
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
            _isRotating = true;
        }
    }
}