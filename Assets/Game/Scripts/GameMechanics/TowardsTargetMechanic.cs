using UnityEngine;
public class TowardsTargetMechanic
{
    private readonly AtomicVariable<Transform> _target;
    private readonly Transform _self;
    private readonly IAtomicVariable<Vector3> _direction;
    private readonly IAtomicValue<bool> _isGameFinished;

    public TowardsTargetMechanic(
        AtomicVariable<Transform> target, Transform self, 
        IAtomicVariable<Vector3> direction,
        IAtomicValue<bool> isGameFinished)
    {
        _target = target;
        _self = self;
        _direction = direction;
        _isGameFinished = isGameFinished;
    }

    public void Update()
    {
        if (_isGameFinished.Value)
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