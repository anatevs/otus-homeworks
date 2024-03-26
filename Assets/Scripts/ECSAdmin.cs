using Scellecs.Morpeh;
using UnityEngine;
using VContainer;

public class ECSAdmin : MonoBehaviour
{
    private World _world;
    private SystemsGroup _systemsGroup;

    private World _eventsWorld;
    private SystemsGroup _events;

    private TeamService<TeamRed> _redTeamService;
    private TeamService<TeamBlue> _blueTeamService;

    [Inject]
    public void Construct
        (
        World eventsWorld,
        TeamService<TeamRed> redTeamService,
        TeamService<TeamBlue> blueTeamService
        )
    {
        _eventsWorld = eventsWorld;
        _redTeamService = redTeamService;
        _blueTeamService = blueTeamService;
    }

    private void Awake()
    {
        _world = World.Default;
        _systemsGroup = _world.CreateSystemsGroup();

        _events = _eventsWorld.CreateSystemsGroup();


        _systemsGroup.AddInitializer(new TeamsServicesInitializer(_redTeamService, _blueTeamService));


        _systemsGroup.AddSystem(new HealthSystem());

        _systemsGroup.AddSystem(new TargetDefineSystem<TeamBlue, TeamRed>(new TeamBlue(), _redTeamService));
        _systemsGroup.AddSystem(new DirectToTargetSystem());
        _systemsGroup.AddSystem(new RotationSystem());
        _systemsGroup.AddSystem(new MovementSystem());
        _systemsGroup.AddSystem(new AttackDistanceSystem());

        _systemsGroup.AddSystem(new FireRequestSystem());

        _systemsGroup.AddSystem(new TransformViewSystem());


        _world.AddSystemsGroup(order: 0, _systemsGroup);
    }

    //private void Update()
    //{
        //_world.Update(Time.deltaTime);
    //}
}