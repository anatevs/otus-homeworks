using Atomic.AI;
using ResourcesStorage;
using UnityEngine;

namespace AI
{
    public sealed class IsResourceStorageFullBlackboardCondition : IBlackboardCondition
    {
        [SerializeField]
        private ResourceID _resourceID;

        public bool Invoke(IBlackboard blackboard)
        {
            if (!blackboard.TryGetCharacter(out var character))
            {
                Debug.Log($"no character in blackboard!");
                return false;
            }

            if (!character.TryGetComponent<ResourcesContainer>(out var characterResources))
            {
                Debug.Log($"no CharacterResources at {character.name}!");
                return false;
            }

            if (!characterResources.TryGetResourceStorage(_resourceID.ID, out var resourceStorage))
            {
                Debug.Log($"no ResourceStorage {_resourceID.ID} in {character.name} CharacterResources!");
                return false;
            }

            return resourceStorage.IsFull;
        }
    }
}