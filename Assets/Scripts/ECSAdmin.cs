using Scellecs.Morpeh;
using UnityEngine;
using VContainer;

public class ECSAdmin : MonoBehaviour
{
    private World _world;
    private SystemsGroup _systemsGroup;

    private TowardsTargetSystem<TeamRed> _towardsRed;


    private TeamService<TeamRed> _redTeamService;


    [Inject]
    public void Construct(TowardsTargetSystem<TeamRed> towardsRed,
        TeamService<TeamRed> redTeamService)
    {
        _towardsRed = towardsRed;

        _redTeamService = redTeamService;
    }

    private void Awake()
    {
        _world = World.Default;

        _systemsGroup = _world.CreateSystemsGroup();

        _systemsGroup.AddSystem(new HealthSystem());

        _systemsGroup.AddSystem(new TeamsServicesSystem(_redTeamService));

        _systemsGroup.AddSystem(_towardsRed);
        //_systemsGroup.AddSystem(new DirectToTargetSystem());
        _systemsGroup.AddSystem(new MovementSystem());



        _systemsGroup.AddSystem(new TransformViewSystem());


        _world.AddSystemsGroup(order: 0, _systemsGroup);
    }

    //private void Update()
    //{
        //_world.Update(Time.deltaTime);
    //}
}