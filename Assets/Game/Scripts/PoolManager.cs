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

    private IObjectResolver _objectResolver;

    private readonly Queue<T> _pool = new();

    public PoolManager(IObjectResolver objectResolver, Transform worldTransform, Transform poolTransform, int initCount, T prefab)
    {
        _objectResolver = objectResolver;

        //_gameManagerData = gameManagerData;
        _worldTransform = worldTransform;
        _poolTransform = poolTransform;
        _initialCount = initCount;
        _prefab = (T)prefab;

        for (var i = 0; i < _initialCount; i++)
        {
            var subject = _objectResolver.Instantiate(_prefab, _poolTransform);
            _pool.Enqueue(subject);
        }
    }

    public T Spawn()
    {
        if (_pool.TryDequeue(out var subject))
        {
            subject.transform.SetParent(_worldTransform);
            //_gameManagerData.AddListener(_bulletControllers[subject]);
        }
        else
        {
            subject = _objectResolver.Instantiate(_prefab, _worldTransform);
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