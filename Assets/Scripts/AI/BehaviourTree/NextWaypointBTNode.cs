using Atomic.AI;
using Game.Engine;

namespace AI
{
    public sealed class NextWaypointBTNode : BTNode
    {
        protected override BTResult OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetWaypoints(out var waypointsGO))
            {
                return BTResult.FAILURE;
            }

            var waypoints = waypointsGO.GetComponent<Waypoints>();

            if (blackboard.TryGetTarget(out var targetGO))
            {
                if (!waypoints.TargetIsWaypoint(targetGO.transform))
                {
                    blackboard.SetTarget(waypoints.GetAndSetNearestWaypoint(targetGO.transform));
                    blackboard.SetTargetDistance(waypoints.StopDistance);

                    return BTResult.SUCCESS;
                }
            }

            blackboard.SetTarget(waypoints.GetAndSetNextWaypoint());
            blackboard.SetTargetDistance(waypoints.StopDistance);

            return BTResult.SUCCESS;
        }
    }
}