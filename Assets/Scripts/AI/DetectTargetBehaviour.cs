using Atomic.AI;
using UnityEngine;

namespace Scripts.AI
{
    public class DetectTargetBehaviour : IAIUpdate
    {
        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (blackboard.TryGetCharacter(out var character) &&
                blackboard.TryGetTargetObject(out var target) &&
                blackboard.TryGetAttackDistance(out var attackDistance))
            {
                var distanceVector = target.transform.position - character.transform.position;

                if (distanceVector.sqrMagnitude <= attackDistance * attackDistance)
                {
                    Debug.Log("target detected!");
                }
            }
        }
    }
}