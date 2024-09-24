using Atomic.AI;
using Sample;
using UnityEngine;

namespace Scripts.AI
{
    public class FollowBehavior : IAIUpdate
    {
        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (blackboard.TryGetCharacter(out var character) &&
                blackboard.TryGetTargetObject(out var target) &&
                blackboard.TryGetStoppingDistance(out var stoppingDistance))
            {
                FollowTarget(character, target, stoppingDistance);
            }
        }

        private void FollowTarget(GameObject character, GameObject target, float stoppingDistance)
        {
            Vector3 characterPosition = character.transform.position;
            Vector3 targetPosition = target.transform.position;

            Vector3 direction = targetPosition - characterPosition;
            direction.y = 0;

            Vector3 moveDirection = Vector3.zero;

            if (direction.sqrMagnitude > stoppingDistance * stoppingDistance)
            {
                moveDirection = direction.normalized;
            }
            character.GetComponent<MoveComponent>().SetDirection(moveDirection);
        }
    }
}