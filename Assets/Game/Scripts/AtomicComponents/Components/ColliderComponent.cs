using UnityEngine;

public class ColliderComponent : IColliderComponent
{
    public Collider Collider
    {
        get => _collider;
        private set => _collider = value;
    }

    private Collider _collider;

    public ColliderComponent(Collider collider)
    {
        _collider = collider;
    }
}