﻿using UnityEngine;

public class DestroyMechanic
{
    private readonly GameObject _gameObject;
    private readonly AtomicVariable<bool> _isDestroyed;

    public DestroyMechanic(GameObject gameObject, AtomicVariable<bool> isDestroyed)
    {
        _gameObject = gameObject;
        _isDestroyed = isDestroyed;
    }

    public void OnEnable()
    {
        _isDestroyed.Subscribe(OnDestroy);
    }

    public void OnDisable()
    {
        _isDestroyed.Unsubscribe(OnDestroy);
    }

    private void OnDestroy(bool isDead)
    {
        if (!isDead)
        {
            return;
        }
        else
        {
            Object.Destroy(_gameObject);
        }
    }
}