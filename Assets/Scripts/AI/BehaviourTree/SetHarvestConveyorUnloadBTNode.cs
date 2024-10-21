using Atomic.AI;
using Conveyor;
using System.Collections;
using UnityEngine;

namespace AI
{
    public class SetHarvestConveyorUnloadBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetConveyor(out var conveyorGO))
            {
                return BTResult.FAILURE;
            }

            var conveyor = conveyorGO.GetComponent<ConveyorComponent>();
            var harvest = conveyor.UnloadHarvest;

            blackboard.SetHarvest(harvest.gameObject);
            return BTResult.SUCCESS;
        }
    }
}