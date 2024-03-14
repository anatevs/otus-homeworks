using UnityEngine;
public class TowardsTargetMechanic
{
    private readonly AtomicVariable<Transform> _target;
    private readonly Transform _self;
    private readonly IAtomicVariable<Vector3> _direction;

    public TowardsTargetMechanic(
        AtomicVariable<Transform> target, Transform self, 
        IAtomicVariable<Vector3> direction)
    {
        _target = target;
        _self = self;
        _direction = direction;
    }

    public void Update()
    {
        Vector3 direction = (_target.Value.position - _self.position).normalized;
        direction.y = 0;
        _direction.Value = direction;
    }
}