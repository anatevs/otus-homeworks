using Atomic.AI;
using UnityEngine;

namespace AI
{
    public sealed class SetTargetBTNode : BTNode
    {
        [SerializeField]
        private Transform _target;

        [SerializeField]
        private float _stopDistance;

        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            blackboard.SetTarget(_target);

            blackboard.SetTargetDistance(_stopDistance);

            return BTResult.SUCCESS;
        }
    }
}