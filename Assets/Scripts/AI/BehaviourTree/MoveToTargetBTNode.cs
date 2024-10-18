using Atomic.AI;
using Game.Engine;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AI.BehaviourTree
{
    public class MoveToTargetBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!(blackboard.TryGetCharacter(out var character) &&
                blackboard.TryGetTarget(out var target) &&
                blackboard.TryGetTargetDistance(out var stopDistance)
                ))
            {
                return BTResult.FAILURE;
            }

            var direction = target.transform.position - character.transform.position;

            if (direction.sqrMagnitude <= stopDistance * stopDistance)
            {
                return BTResult.SUCCESS;
            }

            character.GetComponent<MoveComponent>().MoveStep(direction.normalized);

            return BTResult.RUNNING;
        }
    }
}