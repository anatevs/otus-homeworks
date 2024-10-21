using Atomic.AI;
using ResourcesStorage;
using UnityEngine;

namespace AI
{
    public sealed class IsResourceStorageFullBlackboardCondition : IBlackboardCondition
    {
        [SerializeField]
        private string _storageID;

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

            if (!characterResources.TryGetResourceStorage(_storageID, out var resourceStorage))
            {
                Debug.Log($"no ResourceStorage {_storageID} in {character.name} CharacterResources!");
                return false;
            }

            return resourceStorage.IsFull;
        }
    }
}