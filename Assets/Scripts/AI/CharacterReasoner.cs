using Atomic.AI;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class CharacterReasoner : IAIUpdate
    {
        public void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (blackboard.HasTargetObject())
            {
                blackboard.DelPatrolTag();
                blackboard.SetFollowTag();
                blackboard.SetAttackTag();
            }
            else
            {
                blackboard.SetPatrolTag();
                blackboard.DelFollowTag();
                blackboard.DelAttackTag();
            }
        }
    }
}