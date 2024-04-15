public struct NextMoveEvent
{
    public readonly Team playingTeam;

    public NextMoveEvent(Team team)
    {
        playingTeam = team;
    }
}