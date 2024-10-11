using Atomic.AI;
using UnityEngine;

namespace Game.Engine
{
    public class IsObjectResourceStorageFullCondition : IBlackboardCondition
    {
        [SerializeField, BlackboardKey]
        private int _objectKey;

        public bool Invoke(IBlackboard blackboard)
        {
            var obj = (GameObject)blackboard.GetObject(_objectKey);
            return obj.GetComponent<ResourceStorageComponent>().IsFull();
        }
    }
}