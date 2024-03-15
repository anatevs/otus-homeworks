using UnityEngine;

public class MoveDirectionComponent : IDirectionComponent
{
    public Vector3 Direction
    {
        get => _direction.Value;
        set => _direction.Value = value;
    }

    private readonly IAtomicVariable<Vector3> _direction;

    public MoveDirectionComponent(IAtomicVariable<Vector3> direction)
    {
        _direction = direction;
    }
}