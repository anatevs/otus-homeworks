using Atomic.AI;

namespace Game.Engine.AI
{
    public class HasBulletsAICondition : IBlackboardCondition
    {
        public bool Invoke(IBlackboard blackboard)
        {
            var rangeWeapon = blackboard
                .GetCharacter()
                .GetComponent<WeaponInventoryComponent>()
                .FindWeapon<RangeWeapon>();

            if (rangeWeapon != null)
            {
                return rangeWeapon.CanFire();
            }

            return false;
        }
    }
}