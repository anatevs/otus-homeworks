using Atomic.AI;

namespace Game.Engine
{
    public sealed class HasEnemyBlackboardCondition : IBlackboardCondition
    {
        public bool Invoke(IBlackboard blackboard)
        {
            return blackboard.HasEnemy();
        }
    }
}