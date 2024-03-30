using Scellecs.Morpeh;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class SpawnFromPoolECS
{
    private readonly GameListenersContainer _listenersContainer;

    private readonly Transform _worldTransform;

    private readonly Transform _poolTransform;

    private readonly int _initialCount;

    private readonly GameObject _prefab;

    private readonly IObjectResolver _container;

    private readonly Queue<GameObject> _pool = new();

    public SpawnFromPoolECS(IObjectResolver container, GameListenersContainer gameListenersContainer, PoolParamsGO poolParams)
    {
        _container = container;

        _listenersContainer = gameListenersContainer;

        _worldTransform = poolParams.worldTransform;
        _poolTransform = poolParams.poolTransform;
        _initialCount = poolParams.initCount;
        _prefab = poolParams.prefab;

        for (var i = 0; i < _initialCount; i++)
        {
            GameObject subject = _container.Instantiate(_prefab, _poolTransform);
            _listenersContainer.AddListener(subject);
            _pool.Enqueue(subject);
        }
    }

    public Entity Spawn(Vector3 position, Quaternion rotation)
    {
        if (_pool.TryDequeue(out GameObject go))
        {
            go.transform.SetParent(_worldTransform);
        }
        else
        {
            go = _container.Instantiate(_prefab, _worldTransform);
            _listenersContainer.AddListener(go);
        }
        go.transform.SetPositionAndRotation(position, rotation);
        go.SetActive(true);

        Entity entity = go.GetComponent<PositionProvider>().Entity;
        if (entity.Has<Inactive>())
        {
            entity.RemoveComponent<Inactive>();
        }
        return entity;
    }

    public void UnSpawn(Entity entity)
    {
        entity.AddComponent<Inactive>();
        GameObject go = entity.GetComponent<TransformView>().value.gameObject;
        go.transform.SetParent(_poolTransform);
        go.SetActive(false);
        _pool.Enqueue(go);
    }
}