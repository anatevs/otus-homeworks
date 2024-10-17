using Atomic.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class NextWaypointBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!(blackboard.TryGetCharacter(out var character) &&
                blackboard.TryGetWaypoints(out var waypoints)
                ))
            {
                return BTResult.FAILURE;
            }


            return BTResult.SUCCESS;
        }
    }
}