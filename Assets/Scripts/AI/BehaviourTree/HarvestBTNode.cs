using Atomic.AI;
using Game.Engine;
using UnityEngine;

namespace Assets.Scripts.AI.BehaviourTree
{
    public class HarvestBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetCharacter(out var character))
            {
                return BTResult.FAILURE;
            }

            var harvest = character.GetComponent<HarvestComponent>();

            if (harvest.IsHarvesting)
            {
                return BTResult.RUNNING;
            }

            if (!harvest.StartHarvest())
            {
                return BTResult.FAILURE;
            }

            return BTResult.SUCCESS;
        }
    }
}