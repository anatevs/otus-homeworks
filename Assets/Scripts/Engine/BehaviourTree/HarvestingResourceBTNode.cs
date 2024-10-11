using Atomic.AI;
using System.Collections;
using UnityEngine;

namespace Game.Engine
{
    public class HarvestingResourceBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!(blackboard.TryGetCharacter(out var character) &&
                blackboard.TryGetTarget(out var target)
                ))
            {
                return BTResult.FAILURE;
            }

            var harvest = character.GetComponent<HarvestComponent>();

            if (harvest.IsHarvesting)
            {
                return BTResult.RUNNING;
            }
            else
            {
                if (!harvest.StartHarvest())
                {
                    return BTResult.FAILURE;
                }
            }

            return BTResult.SUCCESS;
        }
    }
}