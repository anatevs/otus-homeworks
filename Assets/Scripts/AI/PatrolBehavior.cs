using Assets.Scripts.AI;
using Atomic.AI;
using Sample;

namespace Scripts.AI
{
    public class PatrolBehavior : IAIUpdate
    {
        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (blackboard.TryGetCharacter(out var character) &&
                blackboard.TryGetPatrolPoints(out var patrolPoints))
            {
                var direction = patrolPoints
                    .GetComponent<PatrolPoints>()
                    .GetPointDirection(character.transform.position);

                character.GetComponent<MoveComponent>().SetDirection(direction);
            }
        }
    }
}