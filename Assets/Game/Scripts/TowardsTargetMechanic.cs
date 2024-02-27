﻿using UnityEngine;
public class TowardsTargetMechanic
{
    private Transform _target;
    private Transform _self;
    private IAtomicVariable<Vector3> _direction;

    public TowardsTargetMechanic(Transform target, Transform self, IAtomicVariable<Vector3> direction)
    {
        _target = target;
        _self = self;
        _direction = direction;
    }

    public void Update()
    {
        Vector3 fullDirection = _target.position - _self.position;
        _direction.Value = new Vector3(fullDirection.x, 0, fullDirection.z);
    }
}