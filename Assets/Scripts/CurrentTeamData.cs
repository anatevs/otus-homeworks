public class CurrentTeamData
{
    public Team Player
    {
        get => _player;
        set
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

    private void SetEnemy()
    {
        _enemy = (Team)(((int)Player + 1) % 2);
    }
}