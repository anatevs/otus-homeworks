using System;
using UnityEngine;

public class DirectionComponent : IDirectionComponent
{
    public Vector3 Direction
    {
        get => _direction.Value;
        set => _direction.Value = value;
    }

    private IAtomicVariable<Vector3> _direction;

    public DirectionComponent(IAtomicVariable<Vector3> direction)
    {
        _direction = direction;
    }
}