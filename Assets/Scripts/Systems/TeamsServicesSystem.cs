using Scellecs.Morpeh;
using UnityEngine;

public class TeamsServicesSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;
    private Stash<Target> _targetStash;
    private Stash<UnderAttackTag> _underAttackStash;

    private TeamService<TeamRed> _redTeamService;

    public TeamsServicesSystem(TeamService<TeamRed> redTeamService)
    {
        _redTeamService = redTeamService;
    }

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<Position>()
            .With<Team>()
            .With<IsActive>()
            .Build();

        foreach (Entity entity in _filter)
        {
            if (entity.GetComponent<Team>().value == TeamType.Red)
            {
                _redTeamService.AddToTeam(entity);
            }
        }
    }

    public void OnUpdate(float deltaTime)
    {
        //foreach (Entity entity in _filter)
        //{
            
        //}
    }

    public void Dispose()
    {
    }
}
