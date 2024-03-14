using UnityEngine;

public class TargetTransformComponent : ITransformComponent
{
    public Transform Transform
    {
        get => _transform.Value;
        //private set => _transform = value;
    }

    private AtomicVariable<Transform> _transform;

    public TargetTransformComponent(AtomicVariable<Transform> transform)
    {
        _transform = transform;
    }

    public void ChangeTargetTransform(Transform transform)
    {
        _transform.Value = transform;
    }
}