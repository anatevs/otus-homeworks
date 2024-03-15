using System;
using UnityEngine;
using VContainer;

public class ZombieSystem : 
    MonoBehaviour,
    IFinishGameListener
{
    public event Action<int> OnDestroyZombie;

    [SerializeField]
    private float _spawnCooldown = 3f;

    private int _destoyedCount = 0;
    private event Action OnCountdown;
    private float _currentTimer;
    private bool _stopCountdown;

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

    private void Start()
    {
        _stopCountdown = false;
        ResetSpawnTimer();
    }

    private void Update()
    {
        if (_stopCountdown)
        {
            return;
        }
        SpawnTimer();
    }

    private void OnEnable()
    {
        OnCountdown += SpawnZombie;
    }

    private void OnDisable()
    {
        OnCountdown -= SpawnZombie;
    }

    public void OnFinishGame()
    {
        _stopCountdown = true;
    }

    private void SpawnTimer()
    {
        _currentTimer -= Time.deltaTime;
        if (_currentTimer <= 0)
        {
            ResetSpawnTimer();
            OnCountdown.Invoke();
        }
        else
        {
            return;
        }
    }

    private void ResetSpawnTimer()
    {
        _currentTimer = _spawnCooldown;
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