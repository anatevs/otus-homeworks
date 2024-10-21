using Atomic.AI;
using ResourcesStorage;
using UnityEngine;

namespace AI
{
    public sealed class SetHarvestingID : BTNode
    {
        [SerializeField]
        private ResourceID _resourceID;

        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            blackboard.SetHarvestingID(_resourceID.ID);

            return BTResult.SUCCESS;
        }
    }
}