using Atomic.AI;
using UnityEngine;

namespace Game.Engine
{
    public class UnloadToBarnBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!(blackboard.TryGetCharacter(out var character) &&
                blackboard.TryGetBarn(out var barn)))
            {
                return BTResult.FAILURE;
            }

            if (!(character.TryGetComponent<ResourceStorageComponent>(out var characterStorage) &&
                barn.TryGetComponent<ResourceStorageComponent>(out var barnStorage)))
            {
                return BTResult.FAILURE;
            }

            if (barnStorage.FreeSlots == 0)
            {
                return BTResult.FAILURE;
            }

            if (!barnStorage.CanAddResources(characterStorage.Current))
            {
                characterStorage.RemoveResources(barnStorage.FreeSlots);

                barnStorage.AddResources(barnStorage.FreeSlots);
            }
            else
            {
                barnStorage.AddResources(characterStorage.ExtractAllResources());
            }

            return BTResult.SUCCESS;
        }
    }
}