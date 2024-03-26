using System;
using UnityEngine;
using VContainer;

public class ZombieSpawnSystem : MonoBehaviour
{
    public event Action<int> OnDestroyZombie;

    public AtomicVariable<float> _countAmount;
    private readonly AtomicEvent OnCounted = new AtomicEvent();
    private readonly AtomicEvent ResetCounter = new AtomicEvent();
    private CounterMechanic _counterMechanic;

    private int _destoyedCount = 0;

    private readonly Vector3[] _spawnPoints = 
        {
            new Vector3(25, 0, 25),
            new Vector3(-25, 0, 25),
            new Vector3(25, 0, -25),
            new Vector3(-25, 0, -25)
        };

    private PoolManager<ZombieEntity> _zombiePool;
    private PlayerEntity _playerEntity;

    [Inject]
    public void Construct(PoolManager<ZombieEntity> poolManager, PlayerEntity playerEntity)
    {
        _zombiePool = poolManager;
        _playerEntity = playerEntity;
    }

    private void Awake()
    {
        _counterMechanic = new CounterMechanic(OnCounted, ResetCounter, _countAmount);
    }

    private void Update()
    {
        _counterMechanic.Update();
    }

    private void OnEnable()
    {
        OnCounted.Subscribe(SpawnZombie);
        _counterMechanic.OnEnable();
    }

    private void OnDisable()
    {
        OnCounted.Unsubscribe(SpawnZombie);
        _counterMechanic.OnDisable();
    }

    private void SpawnZombie()
    {
        ZombieEntity zombieEntity = _zombiePool.Spawn();

        zombieEntity.transform.position = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];
        zombieEntity.GetComponent<Zombie>().InitZombie(_playerEntity);

        if (zombieEntity.GetEntitySize() == 0)
        {
            zombieEntity.Init(zombieEntity.GetComponent<Zombie>());
        }

        zombieEntity.GetEntityComponent<UnspawnComponent>().OnUnspawn += UnSpawnZombie;
        zombieEntity.GetEntityComponent<DeathComponent>().OnDeath += OnDeathZombie;
    }

    private void UnSpawnZombie(Entity zombieEntity)
    {
        _zombiePool.UnSpawn((ZombieEntity)zombieEntity);

        zombieEntity.GetEntityComponent<UnspawnComponent>().OnUnspawn -= UnSpawnZombie;
        zombieEntity.GetEntityComponent<DeathComponent>().OnDeath -= OnDeathZombie;
    }

    private void OnDeathZombie(bool isDeath)
    {
        if (isDeath)
        {
            _destoyedCount += 1;
            OnDestroyZombie?.Invoke(_destoyedCount);
        }
        else
        {
            return;
        }
    }
}