using Atomic.AI;
using System.Collections;
using UnityEngine;

namespace Game.Engine.AI
{
    public class RangeAttackState : IState
    {
        public string Name => "Range attack state";

        public void OnEnter(IBlackboard blackboard)
        {
            var character = blackboard.GetCharacter();

            character.GetComponent<MoveComponent>().Stop();

            character
                .GetComponent<SwitchWeaponComponent>()
                .SwitchWeaponTo<RangeWeapon>();
        }

        public void OnExit(IBlackboard blackboard)
        {
            blackboard.GetCharacter().GetComponent<MoveComponent>().Stop();
        }

        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            GameObject character = blackboard.GetCharacter();

            if (!blackboard.TryGetTarget(out GameObject target))
            {
                character.GetComponent<MoveComponent>().Stop();
                return;
            }

            if (!target.TryGetComponent(out HealthComponent healthComponent) || healthComponent.IsNotAlive())
            {
                character.GetComponent<MoveComponent>().Stop();
                return;
            }

            Vector2 currentPosition = character.transform.position;
            Vector2 targetPosition = target.transform.position;
            Vector2 distanceVector = targetPosition - currentPosition;

            float targetDirection = Mathf.Sign(distanceVector.x);

            character.GetComponent<MoveComponent>().Stop();
            character.GetComponent<LookComponent>().CurrentDirection = targetDirection;
            character.GetComponent<AttackComponent>().Attack();
        }
    }
}