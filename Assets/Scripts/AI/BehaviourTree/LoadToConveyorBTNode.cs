using Atomic.AI;
using Conveyor;
using ResourcesStorage;
using UnityEngine;

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

            var conveyor = conveyorGO.GetComponent<ConveyorComponent>();
            var resourceID = conveyor.LoadID;

            var characterStorages = characterGO.GetComponent<ResourcesContainer>();
            var characterStorage = characterStorages.GetResourceStorage(resourceID);

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