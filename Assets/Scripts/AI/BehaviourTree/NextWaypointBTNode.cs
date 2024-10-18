using Atomic.AI;
using GameObjectComponents;

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

            blackboard.SetTarget(waypoints.GetNextWaypoint());
            blackboard.SetTargetDistance(waypoints.StopDistance);

            return BTResult.SUCCESS;
        }
    }
}