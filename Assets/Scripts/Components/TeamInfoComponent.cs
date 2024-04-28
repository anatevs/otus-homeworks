public readonly struct TeamInfoComponent : IComponent
{
    public readonly Team team;
    public readonly int id;

    public TeamInfoComponent(Team initTeam, int initID)
    {
        team = initTeam;
        id = initID;
    }
}