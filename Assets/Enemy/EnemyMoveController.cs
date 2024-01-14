namespace ShootEmUp
{
    public sealed class EnemyMoveController :
        IFixedUpdate,
        IPausedFixedUpdate,
        IStartGame,
        IPauseGame,
        IResumeGame
    {
        public bool Enabled { get; private set; }

        private EnemyMoveAgent _enemyMoveAgent;

        public EnemyMoveController(EnemyMoveAgent enemyMoveAgent)
        {
            _enemyMoveAgent = enemyMoveAgent;
        }

        public void OnFixedUpdate()
        {
            _enemyMoveAgent.FixedUpdateMove();
        }
        public void OnPausedFixedUpdate()
        {
            return;
        }

        public void OnStart()
        {
            Enabled = true;
        }

        public void OnPause()
        {
            Enabled = false;
        }

        public void OnResume()
        {
            Enabled = true;
        }
    }
}