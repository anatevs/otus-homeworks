using UnityEngine;

public class TransformComponent : ITransformComponent
{
    public Transform Transform
    {
        get => _transform;
        private set => _transform = value;
    }

    private Transform _transform;

    public TransformComponent(Transform transform)
    {
        _transform = transform;
    }
}