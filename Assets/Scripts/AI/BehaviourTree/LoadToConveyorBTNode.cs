using Atomic.AI;
using Game.Engine;

namespace AI
{
    public sealed class LoadToConveyorBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!(blackboard.TryGetCharacter(out var characterGO) &&
                blackboard.TryGetConveyor(out var conveyorGO)))
            {
                return BTResult.FAILURE;
            }

            var conveyor = conveyorGO.GetComponent<Conveyor>();
            var resourceID = conveyor.LoadID;

            var storages = characterGO.GetComponent<ResourcesContainer>();
            var characterStorage = storages.GetResourceStorage(resourceID);

            if (!conveyor.TryLoadConveyor(resourceID, characterStorage.Count, out var enabledValue))
            {
                if (enabledValue == 0)
                {
                    return BTResult.FAILURE;
                }

                characterStorage.RemoveResource(enabledValue);
                conveyor.LoadToConveyor(enabledValue);
                return BTResult.SUCCESS;
            }

            characterStorage.RemoveResource(characterStorage.Count);
            return BTResult.SUCCESS;
        }
    }
}