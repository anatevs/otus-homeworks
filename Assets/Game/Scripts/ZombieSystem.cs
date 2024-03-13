using System;
using UnityEngine;
using VContainer;

public class ZombieSystem : MonoBehaviour
{
    [SerializeField]
    private PlayerEntity _playerEntity;

    public event Action<int> OnDestroyZombie;
    private int _destoyedCount = 0;

    private readonly int _initHP = 1;

    private event Action OnCountdown;
    private readonly float _spawnCooldown = 3f;
    private float _currentTimer;

    private PoolManager<ZombieEntity> _zombiePool;

    private readonly Vector3[] _spawnPoints = 
        {
            new Vector3(25, 0, 25),
            new Vector3(-25, 0, 25),
            new Vector3(25, 0, -25),
            new Vector3(-25, 0, -25)
        };

    [Inject]
    public void Construct(PoolManager<ZombieEntity> poolManager)
    {
        _zombiePool = poolManager;
    }

    private void Start()
    {
        ResetSpawnTimer();
    }

    private void Update()
    {
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

        zombieEntity.GetComponentFromEntity<UnspawnComponent>().OnUnspawn += UnSpawnZombie;
        zombieEntity.GetComponentFromEntity<DeathComponent>().OnDeath += OnDeathZombie;
    }

    private void UnSpawnZombie(GameObject zombieGO)
    {
        ZombieEntity zombieEntity = zombieGO.GetComponent<ZombieEntity>();
        _zombiePool.UnSpawn(zombieEntity);
        ResetZombieStates(zombieEntity);

        zombieEntity.GetComponentFromEntity<UnspawnComponent>().OnUnspawn -= UnSpawnZombie;
        zombieEntity.GetComponentFromEntity<DeathComponent>().OnDeath -= OnDeathZombie;
    }

    private void ResetZombieStates(ZombieEntity zombieEntity)
    {
        HPComponent hpComponent = zombieEntity.GetComponentFromEntity<HPComponent>();
        DeathComponent deathComponent = zombieEntity.GetComponentFromEntity<DeathComponent>();

        hpComponent.HP = _initHP;
        deathComponent.SetIsDeath(false);
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