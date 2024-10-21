using Atomic.AI;
using Game.Engine;

namespace AI
{
    public sealed class HarvestBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetHarvest(out var harvestGO))
            {
                return BTResult.FAILURE;
            }

            var harvest = harvestGO.GetComponent<HarvestComponent>();

            if (harvest.IsHarvesting)
            {
                return BTResult.RUNNING;
            }

            if (!harvest.StartHarvest())
            {
                return BTResult.FAILURE;
            }

            return BTResult.SUCCESS;
        }
    }
}