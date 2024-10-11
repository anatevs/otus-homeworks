using Atomic.AI;
using Game;
using UnityEngine;

namespace Game.Engine
{
    public class FindNearestTreeBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!(blackboard.TryGetCharacter(out var character) &&
                blackboard.TryGetTreesServ(out var treesGO)
                //blackboard.TryGetTreeService(out var treeService)
                ))
            {
                return BTResult.FAILURE;
            }

            var treeService = treesGO.GetComponent<TreeService>();

            if (!treeService.FindClosest(character.transform.position, out var closestResource))
            {
                return BTResult.FAILURE;
            }

            blackboard.SetTarget(closestResource);
            return BTResult.SUCCESS;
        }
    }
}