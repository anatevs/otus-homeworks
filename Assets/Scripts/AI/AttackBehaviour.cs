using Atomic.AI;
using Sample;
using UnityEngine;

namespace Scripts.AI
{
    public class AttackBehaviour : IAIUpdate
    {
        [SerializeField]
        private float _shootPeriod_sec = 1;

        private float _counter;

        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (blackboard.HasAttackTag() &&
                blackboard.TryGetCharacter(out var character) &&
                blackboard.TryGetTargetObject(out var target))
            {
                var direction = (target.transform.position - character.transform.position).normalized;

                character.GetComponent<RotationComponent>().Rotate(direction);

                _counter += deltaTime;
                if (_counter > _shootPeriod_sec)
                {
                    _counter = 0;
                    character.GetComponent<ShootComponent>().Shoot();
                }
            }
        }
    }
}