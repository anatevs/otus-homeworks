using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class ZombieSystem : MonoBehaviour
{
    private PoolManager<Zombie> _zombiePool;

    private Vector3[] _spawnPoints = 
    {
        new Vector3(5, 0, 5),
        new Vector3(-5, 0, 5),
        new Vector3(5, 0, -5),
        new Vector3(-5, 0, -5)
    };

    private Dictionary<Zombie, ZombieEntity> _entities;

    [Inject]
    public void Construct(PoolManager<Zombie> poolManager)
    {
        _zombiePool = poolManager;
    }

    public void SpawnZombie()
    {
        Zombie zombie = _zombiePool.Spawn();
        zombie.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        if (_entities.TryGetValue(zombie, out ZombieEntity entity))
        {
            entity.RemoveAllComponents();
            entity.Inint(zombie);
        }
        else
        {
            ZombieEntity zombieEntity = new ZombieEntity();
            zombieEntity.Inint(zombie);
        }
    }
}