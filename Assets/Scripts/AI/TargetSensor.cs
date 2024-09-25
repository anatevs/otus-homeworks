using Atomic.AI;
using UnityEngine;

namespace Scripts.AI
{
    public class TargetSensor : IAIUpdate
    {
        [SerializeField]
        private LayerMask _targetLayer;

        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (FindClosestTarget(blackboard, out var colliders))
            {
                blackboard.SetTargetColliders(colliders);
            }
            else
            {
                blackboard.DelTargetColliders();
            }
        }

        private bool FindClosestTarget(IBlackboard blackboard, out Collider[] colliders)
        {
            if (blackboard.TryGetAttackDistance(out var attackDistance) &&
                blackboard.TryGetSensorPosition(out var sensorTransform)
                )
            {
                var sensorPos = sensorTransform.position;
                sensorPos.y = 0;

                var inDistanceColliders = Physics.OverlapSphere(sensorPos, attackDistance, _targetLayer);

                if (inDistanceColliders.Length > 0)
                {
                    //var nearestCollider = colliders[0];
                    //var sqrMagnNearest = (nearestCollider.transform.position - characterPos).sqrMagnitude;

                    //foreach (var collider in colliders)
                    //{
                    //    var currentMagn = (collider.transform.position - characterPos).sqrMagnitude;
                    //    if (currentMagn < sqrMagnNearest)
                    //    {
                    //        sqrMagnNearest = currentMagn;
                    //        nearestCollider = collider;
                    //    }
                    //}

                    //target = nearestCollider.gameObject;

                    colliders = inDistanceColliders;
                    return true;
                }
            }

            colliders = null;
            return false;
        }
    }
}