using Atomic.AI;
using Conveyor;
using System.Collections;
using UnityEngine;

namespace AI
{
    public sealed class ConveyorLoadIsNotEmptyBlackboardCondition : IBlackboardCondition
    {
        public bool Invoke(IBlackboard blackboard)
        {
            if (!blackboard.TryGetConveyor(out var conveyorGO))
            {
                return false;
            }

            var conveyor = conveyorGO.GetComponent<ConveyorComponent>();

            return !conveyor.LoadIsEmpty;
        }
    }
}