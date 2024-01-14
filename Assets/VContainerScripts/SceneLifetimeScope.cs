using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShootEmUp
{
    public sealed class SceneLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameObject _character;

        [SerializeField] private BulletConfig _playerBulletConfig;

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

            BuildGameListeners(builder);

            BuildInput(builder);

            BuildBulletPoolAndSystem(builder);

            BuildCharacterControllers(builder);

            BuildEnemyPool(builder);

            BuildEnemyManagerAndSpawner(builder);

            BuildLevelBackground(builder);
        }

        private void BuildGameManager(IContainerBuilder builder)
        {
            GameManagerData gameManagerData = new GameManagerData();
            builder.RegisterComponent<GameManagerData>(gameManagerData);
            builder.RegisterComponent<GameManager>(_gameManager);
        }

        private void BuildGameListeners(IContainerBuilder builder)
        {
            builder.Register<GameManagerInstaller>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            builder.Register<StartGameManager>(Lifetime.Singleton).
                WithParameter<StartGameManagerParams>(_startGameManagerParams).
                AsImplementedInterfaces().AsSelf();

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
            builder.Register<BulletPool>(Lifetime.Singleton).WithParameter<BulletPoolParams>(_bulletPoolParams);

            LevelBounds levelBounds = new LevelBounds(_levelBorders);
            builder.Register<BulletSystem>(Lifetime.Singleton).WithParameter<LevelBounds>(levelBounds).AsImplementedInterfaces().AsSelf();
        }

        private void BuildCharacterControllers(IContainerBuilder builder)
        {
            CharacterComponents characterComponents = new CharacterComponents(_character);
            builder.RegisterComponent<CharacterComponents>(characterComponents);

            builder.Register<CharacterMoveController>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            builder.Register<CharacterFireController>(Lifetime.Singleton).WithParameter<BulletConfig>(_playerBulletConfig).AsImplementedInterfaces().AsSelf();

            builder.Register<CharacterDeathObserver>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }

        private void BuildEnemyPool(IContainerBuilder builder)
        {
            builder.Register<EnemyPool>(Lifetime.Singleton).
                WithParameter<EnemyPoolParams>(_enemyPoolParams);
        }

        private void BuildEnemyManagerAndSpawner(IContainerBuilder builder)
        {
            EnemyPositions enemyPositions = new EnemyPositions(_spawnPositions, _attackPositions);
            builder.RegisterComponent<EnemyPositions>(enemyPositions);

            builder.Register<EnemyManager>(Lifetime.Singleton).
                WithParameter<GameObject>(_character);

            builder.Register<EnemyCooldownSpawner>(Lifetime.Singleton).
                WithParameter<float>(_enemyCooldownTime).
                AsImplementedInterfaces().AsSelf();
        }

        private void BuildLevelBackground(IContainerBuilder builder)
        {
            builder.Register<LevelBackground>(Lifetime.Singleton).
                WithParameter<LevelBackgroundParams>(_levelBackgroundParams).
                AsImplementedInterfaces().AsSelf();
        }
    }
}