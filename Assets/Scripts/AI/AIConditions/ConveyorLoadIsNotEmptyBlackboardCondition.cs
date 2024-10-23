using Atomic.AI;
using Game.Engine;
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

            var conveyor = conveyorGO.GetComponent<Conveyor>();

            return !conveyor.LoadIsEmpty;
        }
    }
}