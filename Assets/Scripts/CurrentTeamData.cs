using UnityEngine;

public class CurrentTeamData
{
    public Team Player
    {
        get => _player;
        private set
        {
            _player = value;
            SetEnemy();
        }
    }

    public Team Enemy
    {
        get => _enemy;
    }

    private Team _player;
    private Team _enemy;

    public CurrentTeamData(Team playerTeam)
    {
        Player = playerTeam;
    }

    public void SwitchTeams()
    {
        Player = _enemy;
    }

    private void SetEnemy()
    {
        _enemy = GetOpposite(_player);
    }

    private Team GetOpposite(Team team)
    {
        return (Team)(((int)team + 1) % 2);
    }
}