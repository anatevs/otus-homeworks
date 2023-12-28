using ShootEmUp;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SceneLifetimeScope : LifetimeScope
{

    [SerializeField] public GameObject _character;

    [SerializeField] public BulletConfig _playerBulletConfig;

    [SerializeField] private LevelBordersStorage _levelBorders;

    [SerializeField] private BulletPoolParams _bulletPoolParams;

    [SerializeField] private EnemyPoolParams _enemyPoolParams;

    [SerializeField] private float _enemyCooldownTime = 1f;

    [SerializeField] private GameManager _gameManager;

    [SerializeField] private StartGameManagerParams _startGameManagerParams;

    [SerializeField] private PauseResumeButtons _pauseResumeButtons;

    [SerializeField] private LevelBackgroundParams _levelBackgroundParams;

    [Header("Enemy Positions")]
    [SerializeField]
    private Transform[] _spawnPositions;
    [SerializeField]
    private Transform[] _attackPositions;


    protected override void Configure(IContainerBuilder builder)
    {
        BuildGameManager(builder);

        BuildGameStates(builder);

        BuildInput(builder);
        
        BuildBulletPoolAndSystem(builder);

        BuildCharacterControllers(builder);

        BuildEnemyPool(builder);

        BuildEnemyManagerAndSpawner(builder);

        BuildLevelBackground(builder);

    }

    private void BuildGameManager(IContainerBuilder builder)
    {
        //game manager
        builder.RegisterComponent<GameManager>(_gameManager);
    }

    private void BuildGameStates(IContainerBuilder builder)
    {
        //game manager installer
        builder.Register<GameManagerInstaller>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

        //start game manager
        builder.Register<StartGameManager>(Lifetime.Singleton).
            WithParameter<StartGameManagerParams>(_startGameManagerParams).
            AsImplementedInterfaces().AsSelf();

        //pause resume observer
        builder.Register<PauseResumeButtonsObserver>(Lifetime.Singleton).
            WithParameter<PauseResumeButtons>(_pauseResumeButtons).
            AsImplementedInterfaces().AsSelf();
    }

    private void BuildInput(IContainerBuilder builder)
    {
        builder.Register<KeyboardInput>(Lifetime.Singleton).AsImplementedInterfaces();
    }

    private void BuildBulletPoolAndSystem(IContainerBuilder builder)
    {
        //bullet pool
        builder.Register<BulletPool>(Lifetime.Singleton).WithParameter<BulletPoolParams>(_bulletPoolParams);

        //bullet system
        LevelBounds _levelBounds = new LevelBounds(_levelBorders);
        builder.Register<BulletSystem>(Lifetime.Singleton).WithParameter<LevelBounds>(_levelBounds).AsImplementedInterfaces().AsSelf();
    }

    private void BuildCharacterControllers(IContainerBuilder builder)
    {
        //character components
        CharacterComponents _characterComponents = new CharacterComponents(_character);
        builder.RegisterComponent<CharacterComponents>(_characterComponents);

        //character's moving
        builder.Register<CharacterMoveController>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        
        //character fire controller
        builder.Register<CharacterFireController>(Lifetime.Singleton).WithParameter<BulletConfig>(_playerBulletConfig).AsImplementedInterfaces().AsSelf();

        //character death observer
        builder.Register<CharacterDeathObserver>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
    }

    private void BuildEnemyPool(IContainerBuilder builder)
    {
        //enemy pool
        builder.Register<EnemyPool>(Lifetime.Singleton).
            WithParameter<EnemyPoolParams>(_enemyPoolParams);
    }

    private void BuildEnemyManagerAndSpawner(IContainerBuilder builder)
    {
        //enemy positions
        EnemyPositions _enemyPositions = new EnemyPositions(_spawnPositions, _attackPositions);
        builder.RegisterComponent<EnemyPositions>(_enemyPositions);

        //enemy manager
        builder.Register<EnemyManager>(Lifetime.Singleton).
            WithParameter<GameObject>(_character);

        //enemy cooldown spawner
        builder.Register<EnemyCooldownSpawner>(Lifetime.Singleton).
            WithParameter<float>(_enemyCooldownTime).
            AsImplementedInterfaces().AsSelf();
    }

    private void BuildLevelBackground(IContainerBuilder builder)
    {
        //level background
        builder.Register<LevelBackground>(Lifetime.Singleton).
            WithParameter<LevelBackgroundParams>(_levelBackgroundParams).
            AsImplementedInterfaces().AsSelf();
    }

}
