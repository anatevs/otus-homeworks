using System;
using UnityEngine;
using VContainer;

public class ZombieSystem : 
    MonoBehaviour,
    IFinishGameListener
{
    private PlayerEntity _playerEntity;

    public event Action<int> OnDestroyZombie;

    private int _destoyedCount = 0;

    private readonly int _initHP = 1;

    private event Action OnCountdown;
    private readonly float _spawnCooldown = 3f;
    private float _currentTimer;
    private bool _stopCountdown;

    private PoolManager<ZombieEntity> _zombiePool;

    private readonly Vector3[] _spawnPoints = 
        {
            new Vector3(25, 0, 25),
            new Vector3(-25, 0, 25),
            new Vector3(25, 0, -25),
            new Vector3(-25, 0, -25)
        };

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

    private void UnSpawnZombie(GameObject zombieGO)
    {
        ZombieEntity zombieEntity = zombieGO.GetComponent<ZombieEntity>();
        _zombiePool.UnSpawn(zombieEntity);
        ResetZombieStates(zombieEntity);

        zombieEntity.GetEntityComponent<UnspawnComponent>().OnUnspawn -= UnSpawnZombie;
        zombieEntity.GetEntityComponent<DeathComponent>().OnDeath -= OnDeathZombie;
    }

    private void ResetZombieStates(ZombieEntity zombieEntity)
    {
        HPComponent hpComponent = zombieEntity.GetEntityComponent<HPComponent>();
        DeathComponent deathComponent = zombieEntity.GetEntityComponent<DeathComponent>();

        hpComponent.HP = _initHP;
        deathComponent.IsDead = false;
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