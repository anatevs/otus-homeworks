using Scellecs.Morpeh;
using UnityEngine;
using VContainer;

public class ECSAdmin : MonoBehaviour
{
    private World _world;
    private SystemsGroup _systemsGroup;

    private TeamService<TeamRed> _redTeamService;
    private TeamService<TeamBlue> _blueTeamService;
    private PrefabsStorage _prefabStorage;

    [Inject]
    public void Construct
        (
        TeamService<TeamRed> redTeamService,
        TeamService<TeamBlue> blueTeamService,
        PrefabsStorage prefabStorage
        )
    {
        _redTeamService = redTeamService;
        _blueTeamService = blueTeamService;
        _prefabStorage = prefabStorage;
    }

    private void Awake()
    {
        _world = World.Default;
        _systemsGroup = _world.CreateSystemsGroup();

        _systemsGroup.AddInitializer(new TeamsServicesInitializer(_redTeamService, _blueTeamService));
        _systemsGroup.AddInitializer(new PrefabsAndPoolsInitializer(_prefabStorage));


        _systemsGroup.AddSystem(new HealthSystem());

        _systemsGroup.AddSystem(new TargetDefineSystem<TeamBlue, TeamRed>(new TeamBlue(), _redTeamService));
        _systemsGroup.AddSystem(new DirectToTargetSystem());
        _systemsGroup.AddSystem(new RotationSystem());
        _systemsGroup.AddSystem(new MovementSystem());
        _systemsGroup.AddSystem(new AttackDistanceSystem());

        _systemsGroup.AddSystem(new FireRequestSystem());

        _systemsGroup.AddSystem(new SpawnSystem());

        _systemsGroup.AddSystem(new TransformViewSystem());


        _world.AddSystemsGroup(order: 0, _systemsGroup);

    }
}