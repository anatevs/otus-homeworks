using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public class GameObjectPool<T>
        where T : MonoBehaviour
    {
        private Transform _worldTransform;

        private Transform _poolTransform;

        private int _initPoolSize;

        private T _prefab;

        private GameManagerData _gameManagerData;

        private IObjectResolver _objectResolver;

        private readonly Queue<T> _poolQueue = new();

        public GameObjectPool(IObjectResolver objectResolver, GameManagerData gameManagerData, PoolParams<T> poolParams)
        {
            _objectResolver = objectResolver;

            _worldTransform = poolParams.worldTransform;
            _poolTransform = poolParams.poolTransform;
            _initPoolSize = poolParams.initialPoolSize;
            _prefab = poolParams.prefab;
            _gameManagerData = gameManagerData;

            CreateInitialPool();
        }

        private void CreateInitialPool()
        {
            for (var i = 0; i < _initPoolSize; i++)
            {
                InstantiateToPool();
            }
        }

        private void InstantiateToPool()
        {
            T instance = _objectResolver.Instantiate(_prefab, _poolTransform);
            _poolQueue.Enqueue(instance);
        }

        private void ChangeInstanceTransform(T instance, Transform goalTransform)
        {
            instance.transform.SetParent(goalTransform);
        }

        public void UnSpawn(T objectToUnspawn)
        {
            ChangeInstanceTransform(objectToUnspawn, _poolTransform);
            _poolQueue.Enqueue(objectToUnspawn);
        }
    }
}