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
            if (FindClosestTarget(blackboard, out var target))
            {
                blackboard.SetTargetObject(target);
            }
            else
            {
                blackboard.DelTargetObject();
            }
        }

        private bool FindClosestTarget(IBlackboard blackboard, out GameObject target)
        {
            if (blackboard.TryGetCharacter(out var character) &&
                blackboard.TryGetAttackDistance(out var attackDistance)
                )
            {
                var characterPos = character.transform.position;
                characterPos.y = 0;

                var colliders = Physics.OverlapSphere(characterPos, attackDistance, _targetLayer);

                if (colliders.Length > 0)
                {
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

                    target = nearestCollider.gameObject;
                    return true;
                }
            }

            target = null;
            return false;
        }
    }
}