using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class PoolManager<T> where T : Component
{
    private readonly Transform _worldTransform;

    private readonly Transform _poolTransform;

    private readonly int _initialCount;

    private readonly T _prefab;

    private IObjectResolver _container;

    private readonly Queue<T> _pool = new();

    public PoolManager(IObjectResolver container, PoolParams<T> poolParams)
    {
        _container = container;

        _worldTransform = poolParams.worldTransform;
        _poolTransform = poolParams.poolTransform;
        _initialCount = poolParams.initCount;
        _prefab = poolParams.prefab;

        for (var i = 0; i < _initialCount; i++)
        {
            var subject = _container.Instantiate(_prefab, _poolTransform);
            _pool.Enqueue(subject);
        }
    }

    public T Spawn()
    {
        if (_pool.TryDequeue(out var subject))
        {
            subject.transform.SetParent(_worldTransform);
            subject.gameObject.SetActive(true);
        }
        else
        {
            subject = _container.Instantiate(_prefab);
            subject.transform.SetParent(_worldTransform);
            subject.gameObject.SetActive(true);
        }
        return subject;
    }

    public void UnSpawn(T subject)
    {
        Debug.Log("return to pool");
        subject.transform.SetParent(_poolTransform);
        subject.gameObject.SetActive(false);
        _pool.Enqueue(subject);
    }
}