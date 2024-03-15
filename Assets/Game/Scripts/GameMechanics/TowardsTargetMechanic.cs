using UnityEngine;
public class TowardsTargetMechanic
{
    private readonly AtomicVariable<Transform> _target;
    private readonly Transform _self;
    private readonly IAtomicVariable<Vector3> _direction;
    private readonly IAtomicValue<bool> _isGameFinished;
    private readonly IAtomicValue<bool> _canMove;

    public TowardsTargetMechanic(
        AtomicVariable<Transform> target, Transform self, 
        IAtomicVariable<Vector3> direction,
        IAtomicValue<bool> isGameFinished,
        IAtomicValue<bool> canMove)
    {
        _target = target;
        _self = self;
        _direction = direction;
        _isGameFinished = isGameFinished;
        _canMove = canMove;
    }

    public void Update()
    {
        if (_isGameFinished.Value || !_canMove.Value)
        {
            return;
        }
        else
        {
            Vector3 direction = (_target.Value.position - _self.position).normalized;
            direction.y = 0;
            _direction.Value = direction;
        }
    }
}