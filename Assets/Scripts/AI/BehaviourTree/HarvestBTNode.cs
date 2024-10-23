using Atomic.AI;
using Game.Engine;
using UnityEngine;

namespace AI
{
    public sealed class HarvestBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetHarvest(out var harvestGO))
            {
                blackboard.DelHarvestingID();
                return BTResult.FAILURE;
            }

            var harvest = harvestGO.GetComponent<HarvestComponent>();

            if (harvest.IsHarvesting)
            {
                return BTResult.RUNNING;
            }

            if (!blackboard.TryGetHarvestingID(out var id))
            {
                if (!harvest.StartHarvest())
                {
                    blackboard.DelHarvestingID();
                    return BTResult.FAILURE;
                }
            }
            else
            {
                if (!harvest.StartHarvest(id))
                {
                    blackboard.DelHarvestingID();
                    return BTResult.FAILURE;
                }
            }

            blackboard.DelHarvestingID();

            return BTResult.SUCCESS;
        }
    }
}