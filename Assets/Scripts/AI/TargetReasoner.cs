using Atomic.AI;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class TargetReasoner : IAIUpdate
    {
        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (blackboard.TryGetTargetColliders(out var colliders))
            {
                if (blackboard.TryGetSensorPosition(out var sensorTransform))
                {
                    var characterPos = sensorTransform.position;

                    var target = FindClosestTarget(colliders, characterPos);

                    blackboard.SetTargetObject(target);
                }
            }
            else
            {
                blackboard.DelTargetObject();
            }
        }

        private GameObject FindClosestTarget(Collider[] colliders, Vector3 characterPos)
        {
            characterPos.y = 0;

            var nearestCollider = colliders[0];
            var sqrMagnNearest = (nearestCollider.transform.position - characterPos).sqrMagnitude;

            foreach (var collider in colliders)
            {
                var currentMagn = (collider.transform.position - characterPos).sqrMagnitude;
                if (currentMagn < sqrMagnNearest)
                {
                    sqrMagnNearest = currentMagn;
                    nearestCollider = collider;
                }
            }

            return nearestCollider.gameObject;
        }
    }
}