using Scellecs.Morpeh;
using UnityEngine;

public class TeamServiceInitializer : IInitializer
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;
    private readonly TeamService _teamService;

    public TeamServiceInitializer(TeamService teamService)
    {
        _teamService = teamService;
    }

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<Position>()
            .With<Team>()
            .Without<Inactive>()
            .Build();

        _teamService.Init();

        foreach (Entity entity in _filter)
        {
            _teamService.AddToTeam(entity);
        }
    }

    public void Dispose()
    {
    }
}