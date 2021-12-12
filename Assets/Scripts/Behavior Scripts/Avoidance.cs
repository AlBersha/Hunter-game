using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Behavior_Scripts
{
    [Serializable]
    public class Avoidance : TargetOrientedBehavior
    {
        public Avoidance(BehaviorConfig config, BehaviorAgent agent)
            : base(config, agent) { }
        
        public override Vector3 CalculateDesiredVelocity(Dictionary<EntityManager.EntityType, List<BehaviorAgent>> detectedEntities)
        {
            if (detectedEntities[config.targetType].Count == 0)
                return Vector3.zero;

            return detectedEntities[config.targetType].Aggregate(Vector3.zero, (current, item) => current + (agent.transform.position - item.transform.position));
        }
    }
}

