using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PoolManager<T> where T : Component
{
    private Transform _worldTransform;

    private Transform _poolTransform;

    private int _initialCount;

    private T _prefab;

    //private GameManagerData _gameManagerData;

    private IObjectResolver _container;

    private readonly Queue<T> _pool = new();

    public PoolManager(IObjectResolver container, PoolParams<T> poolParams)
    {
        _container = container;

        //_gameManagerData = gameManagerData;
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
        }
        else
        {
            subject = _container.Instantiate(_prefab, _worldTransform);
            _pool.Enqueue(subject);
            //_gameManagerData.AddListeners(subject.gameObject);
        }
        return subject;
    }

    public void UnSpawn(T subject)
    {
        subject.transform.SetParent(_poolTransform);
        _pool.Enqueue(subject);
    }
}