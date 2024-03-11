using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class BulletPool : MonoBehaviour
{
    private Transform _worldTransform;

    private Transform _poolTransform;

    private int _initialCount;

    private Bullet _prefab;

    //private GameManagerData _gameManagerData;

    private IObjectResolver _container;

    private readonly Queue<Bullet> _pool = new();

    public BulletPool(IObjectResolver container, PoolParams<Bullet> poolParams)
    {
        Debug.Log("register bullet pool");
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

    public Bullet Spawn()
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

    public void UnSpawn(Bullet subject)
    {
        subject.transform.SetParent(_poolTransform);
        _pool.Enqueue(subject);
    }
}
