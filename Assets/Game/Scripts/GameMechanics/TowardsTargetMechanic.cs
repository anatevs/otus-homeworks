using UnityEngine;
public class TowardsTargetMechanic
{
    private readonly Transform _target;
    private readonly Transform _self;
    private readonly IAtomicVariable<Vector3> _direction;

    public TowardsTargetMechanic(Transform target, Transform self, IAtomicVariable<Vector3> direction)
    {
        Debug.Log($"construct tow target with {target}");
        _target = target;
        _self = self;
        _direction = direction;
    }

    public void Update()
    {
        if (_target != null)
        {
            Vector3 direction = (_target.position - _self.position).normalized;
            direction.y = 0;
            _direction.Value = direction;
        }
        else
        {
            Debug.Log("no target to follow");
            return;
        }
    }
}