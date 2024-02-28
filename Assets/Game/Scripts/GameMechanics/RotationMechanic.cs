using UnityEngine;

public class RotationMechanic
{
    private readonly Transform _transform;
    private readonly IAtomicVariable<Vector3> _directionVector;
    private readonly IAtomicValue<float> _rotSpeed;
    private readonly IAtomicValue<bool> _canMove;

    private float _lerpPersent = 0f;
    private bool _canRotate = true;

    public RotationMechanic(Transform transform, IAtomicVariable<Vector3> directionVector, IAtomicValue<float> rotSpeed, IAtomicValue<bool> canMove)
    {
        _transform = transform;
        _directionVector = directionVector;
        _rotSpeed = rotSpeed;
        _canMove = canMove;
    }

    public void OnEnable()
    {
        _directionVector.OnValueChanged += ResetLerp;
    }

    public void OnDisable()
    {
        _directionVector.OnValueChanged -= ResetLerp;
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
                _lerpPersent = Mathf.MoveTowards(_lerpPersent, 1f, Time.deltaTime * _rotSpeed.Value);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, lookQuaternion, _lerpPersent);

                if (_lerpPersent >= 1f & _canRotate)
                {
                    //rotation done
                    _canRotate = false;
                    Debug.Log("rotation is done");
                }
            }
        }
    }

    private void ResetLerp(Vector3 _)
    {
        _lerpPersent = 0f;
        _canRotate = true;
    }
}