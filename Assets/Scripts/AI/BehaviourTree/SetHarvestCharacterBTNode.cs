using Atomic.AI;
using Game.Engine;

namespace AI
{
    public sealed class SetHarvestCharacterBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetCharacter(out var character))
            {
                return BTResult.FAILURE;
            }

            var harvest = character.GetComponent<HarvestComponent>();

            blackboard.SetHarvest(harvest.gameObject);
            return BTResult.SUCCESS;
        }
    }
}