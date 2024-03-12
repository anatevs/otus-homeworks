using System;
using UnityEngine;
using VContainer;

public class ZombieSystem : MonoBehaviour
{
    [SerializeField]
    private PlayerEntity _playerEntity;

    private readonly int _initHP = 1;

    private event Action OnCountdown;
    private readonly float _spawnCooldown = 3f;
    private float _currentTimer;

    private PoolManager<Zombie> _zombiePool;

    private readonly Vector3[] _spawnPoints = 
        {
            new Vector3(25, 0, 25),
            new Vector3(-25, 0, 25),
            new Vector3(25, 0, -25),
            new Vector3(-25, 0, -25)
        };

    [Inject]
    public void Construct(PoolManager<Zombie> poolManager)
    {
        _zombiePool = poolManager;
    }

    private void Start()
    {
        ResetSpawnTimer();
        Debug.Log($"in z sys start {_playerEntity == null}");
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
        Zombie zombie = _zombiePool.Spawn();
        zombie.transform.position = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];

        Debug.Log($"in zmb sys {_playerEntity == null}");
        zombie.InitZombie(_playerEntity);

        if (zombie.gameObject.TryGetComponent<ZombieEntity>(out ZombieEntity entity))
        {
            //ResetZombieStates(entity);
            entity.RemoveAllComponents();
            entity.Inint(zombie);
        }
        else
        {
            Debug.Log("no entity in Zmb");
            //ZombieEntity zombieEntity = zombie.gameObject.AddComponent<ZombieEntity>();
            //zombieEntity.Inint(zombie);
        }
    }

    private void UnSpawnZombie(Zombie zombie)
    {
        _zombiePool.UnSpawn(zombie);
    }

    private void ResetZombieStates(ZombieEntity zombieEntity)
    {
        HPComponent hpComponent = zombieEntity.GetComponentFromEntity<HPComponent>();
        DeathComponent deathComponent = zombieEntity.GetComponentFromEntity<DeathComponent>();

        hpComponent.HP = _initHP;
        deathComponent.SetIsDeath(false);
    }
}