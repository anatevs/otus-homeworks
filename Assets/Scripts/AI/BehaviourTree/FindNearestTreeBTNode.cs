using Atomic.AI;
using Game.Engine;
using UnityEngine;

namespace AI
{
    public sealed class FindNearestTreeBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!(blackboard.TryGetCharacter(out var character) &&
                blackboard.TryGetTreesService(out var treesGO)
                ))
            {
                Debug.Log($"no character or treeService in blackboard!");
                return BTResult.FAILURE;
            }

            var trees = treesGO.GetComponent<TreeService>();
            if (!trees.FindClosest(character.transform.position, out var closestTree))
            {
                Debug.Log($"no TreeService at {treesGO.name}!");
                return BTResult.FAILURE;
            }

            blackboard.SetTarget(closestTree.transform);
            blackboard.SetTargetDistance(closestTree.GetComponent<Game.Content.Tree>().TreeStopDistance);
            return BTResult.SUCCESS;
        }
    }
}