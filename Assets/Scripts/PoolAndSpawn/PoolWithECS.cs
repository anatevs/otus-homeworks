using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static UnityEngine.EventSystems.EventTrigger;

public sealed class PoolWithECS
{
    private readonly GameListenersContainer _listenersContainer;

    private readonly Transform _worldTransform;

    private readonly Transform _poolTransform;

    private readonly int _initialCount;

    private readonly GameObject _prefab;

    private readonly IObjectResolver _container;

    private readonly Queue<GameObject> _pool = new();

    public PoolWithECS(IObjectResolver container, GameListenersContainer gameListenersContainer, PoolParamsGO poolParams)
    {
        _container = container;

        _listenersContainer = gameListenersContainer;

        _worldTransform = poolParams.worldTransform;
        _poolTransform = poolParams.poolTransform;
        _initialCount = poolParams.initCount;
        _prefab = poolParams.prefab;

        for (var i = 0; i < _initialCount; i++)
        {
            var subject = _container.Instantiate(_prefab, _poolTransform);
            _listenersContainer.AddListener(subject);
            _pool.Enqueue(subject);
        }
    }

    public Entity Spawn(Vector3 position, Quaternion rotation)
    {
        GameObject subject;
        if (_pool.TryDequeue(out subject))
        {
            subject.transform.SetParent(_worldTransform);
        }
        else
        {
            subject = _container.Instantiate(_prefab, _worldTransform);
            _listenersContainer.AddListener(subject);
        }
        subject.transform.SetPositionAndRotation(position, rotation);
        subject.SetActive(true);

        Entity entity = subject.GetComponent<PositionProvider>().Entity;
        if (entity.Has<Inactive>())
        {
            entity.RemoveComponent<Inactive>();
        }
        return entity;
    }

    public void UnSpawn(Entity entity)
    {
        entity.AddComponent<Inactive>();
        GameObject subject = entity.GetComponent<TransformView>().value.gameObject;
        subject.transform.SetParent(_poolTransform);
        subject.SetActive(false);
        _pool.Enqueue(subject);
    }
}