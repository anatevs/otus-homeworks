namespace ShootEmUp
{
    public sealed class EnemyAttackController :
        IFixedUpdate,
        IPausedFixedUpdate
    {
        private EnemyAttackAgent _enemyAttackAgent;

        public EnemyAttackController(EnemyAttackAgent enemyAttackAgent)
        {
            _enemyAttackAgent = enemyAttackAgent;
        }

        public void OnFixedUpdate()
        {
            _enemyAttackAgent.FixedUpdateAttack();
        }

        public void OnPausedFixedUpdate()
        {
            return;
        }
    }
}
