using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Behavior_Scripts
{
    [Serializable]
    public class Cohesion : TargetOrientedBehavior
    {
        public Cohesion(BehaviorConfig config, BehaviorAgent agent)
            : base(config, agent) { }
        
        private Vector3 GetCenterOfMass(List<BehaviorAgent> detectedEntities)
        {
            return detectedEntities.Aggregate(Vector3.zero, (current, entity) => current + entity.transform.position) / detectedEntities.Count;
        }

        public override Vector3 CalculateDesiredVelocity(Dictionary<EntityManager.EntityType, List<BehaviorAgent>> detectedEntities)
        {
            if (detectedEntities[config.targetType].Count == 0)
                return Vector3.zero;

            return GetCenterOfMass(detectedEntities[config.targetType]) - agent.transform.position;
        }
    }
}

