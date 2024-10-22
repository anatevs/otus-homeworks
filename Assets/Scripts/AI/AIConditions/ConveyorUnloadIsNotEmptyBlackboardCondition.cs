using Atomic.AI;
using Game.Engine;

namespace AI
{
    public sealed class ConveyorUnloadIsNotEmptyBlackboardCondition : IBlackboardCondition
    {
        public bool Invoke(IBlackboard blackboard)
        {
            if (!blackboard.TryGetConveyor(out var conveyorGO))
            {
                return false;
            }

            var conveyor = conveyorGO.GetComponent<Conveyor>();

            return !conveyor.UnloadIsEmpty;
        }
    }
}