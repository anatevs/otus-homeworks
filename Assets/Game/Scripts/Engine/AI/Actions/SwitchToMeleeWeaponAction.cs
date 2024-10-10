using Atomic.AI;

namespace Game.Engine.AI
{
    public class SwitchToMeleeWeaponAction : IBlackboardAction
    {
        public void Invoke(IBlackboard blackboard)
        {
            blackboard
                .GetCharacter()
                .GetComponent<SwitchWeaponComponent>()
                .SwitchWeaponTo<MeleeWeapon>();
        }
    }
}