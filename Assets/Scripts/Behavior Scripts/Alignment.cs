using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Behavior_Scripts
{
    [Serializable]
    public class Alignment : TargetOrientedBehavior
    {
        public Alignment(BehaviorConfig config, BehaviorAgent agent)
            : base(config, agent) { }
        
        public override Vector3 CalculateDesiredVelocity(Dictionary<EntityManager.EntityType, List<BehaviorAgent>> detectedEntities)
        {
            if (detectedEntities[config.targetType].Count == 0)
                return agent.Velocity;

            return detectedEntities[config.targetType].Aggregate(Vector3.zero, (current, item) => current + item.Velocity);
        }
    }
}

