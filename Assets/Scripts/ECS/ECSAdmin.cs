using Scellecs.Morpeh;
using UnityEngine;
using VContainer;

public sealed class ECSAdmin : MonoBehaviour
{
    [SerializeField]
    FinishGameWindow _finishWindow;

    private World _world;
    private SystemsGroup _systemsGroup;

    private TeamService _teamService;
    private PrefabsStorage _prefabStorage;

    [Inject]
    public void Construct
        (
        TeamService teamService,
        PrefabsStorage prefabStorage
        )
    {
        _teamService = teamService;
        _prefabStorage = prefabStorage;
    }

    private void Awake()
    {
        _world = World.Default;
        _systemsGroup = _world.CreateSystemsGroup();

        _systemsGroup.AddInitializer(new PrefabsAndPoolsInitializer(_prefabStorage));
        _systemsGroup.AddInitializer(new TeamServiceInitializer(_teamService));


        _systemsGroup.AddSystem(new TargetDefineSystem(_teamService));
        _systemsGroup.AddSystem(new DirectToTargetSystem());

        _systemsGroup.AddSystem(new RotationSystem());
        _systemsGroup.AddSystem(new MovementSystem());
        _systemsGroup.AddSystem(new AttackDistanceSystem());

        _systemsGroup.AddSystem(new FireRequestSystem());
        _systemsGroup.AddSystem(new FireDelaySystem());

        _systemsGroup.AddSystem(new SpawnProjectileSystem());
        _systemsGroup.AddSystem(new SpawnSystem(_teamService));

        _systemsGroup.AddSystem(new ChangeHealthSystem());
        _systemsGroup.AddSystem(new HealthSystem());

        _systemsGroup.AddSystem(new ProjectileLifetime());

        _systemsGroup.AddSystem(new UnspawnSystem(_teamService));

        _systemsGroup.AddSystem(new TransformViewSystem());

        _systemsGroup.AddSystem(new AnimationStatesSystem_Mob());
        _systemsGroup.AddSystem(new AnimationTakeDamageSystem());

        _systemsGroup.AddSystem(new ClearEventsSystem());

        _systemsGroup.AddSystem(new FinishGameSystem(_finishWindow));


        _world.AddSystemsGroup(order: 0, _systemsGroup);
    }
}