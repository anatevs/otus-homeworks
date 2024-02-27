using UnityEngine;

public class MovementMechanic
{
    private readonly Transform _transform;
    private readonly IAtomicValue<Vector3> _direction;
    private readonly IAtomicValue<float> _speed;
    private readonly IAtomicValue<bool> _canMove;

    public MovementMechanic(Transform transform, IAtomicValue<Vector3> direction, IAtomicValue<float> speed, IAtomicValue<bool> canMove)
    {
        _transform = transform;
        _direction = direction;
        _speed = speed;
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
            _transform.Translate(_direction.Value * _speed.Value * Time.deltaTime, Space.World);
        }
    }
}