using UnityEngine;

public class RotationMechanic
{
    private readonly Transform _transform;
    private readonly IAtomicValue<Vector3> _directionPoint;
    private readonly IAtomicValue<float> _rotSpeed;
    private readonly IAtomicValue<bool> _canMove;

    public RotationMechanic(Transform transform, IAtomicValue<Vector3> directionPoint, IAtomicValue<float> rotSpeed, IAtomicValue<bool> canMove)
    {
        _transform = transform;
        _directionPoint = directionPoint;
        _rotSpeed = rotSpeed;
        _canMove = canMove;
    }

    public void Update()
    {
        if (!_canMove.Value)
        {
            return;
        }
        else
        {
            Quaternion lookQuaternion = Quaternion.LookRotation(_directionPoint.Value - _transform.position);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, lookQuaternion, _rotSpeed.Value * Time.deltaTime);
        }
    }
}