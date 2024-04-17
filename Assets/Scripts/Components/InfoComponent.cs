public readonly struct InfoComponent : IComponent
{
    public readonly Team team;
    public readonly int id;

    public InfoComponent(Team initTeam, int initID)
    {
        team = initTeam;
        id = initID;
    }
}