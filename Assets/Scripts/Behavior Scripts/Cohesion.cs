using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Behavior_Scripts
{
    [Serializable]
    public class Cohesion : Behavior
    {
        public Cohesion(BehaviorConfig config) : base(config) { }
        public override Vector3 CalculateDesiredVelocity(Dictionary<EntityManager.EntityType, List<BehaviorAgent>> detectedEntities)
        {
            if (detectedEntities[config.targetType].Count == 0)
                return Vector3.zero;

            var cohesionMove = detectedEntities[config.targetType].Aggregate(Vector3.zero, (current, item) => current + item.transform.position);
            cohesionMove /= detectedEntities[config.targetType].Count;

            //offset from agent position
            cohesionMove -= config.agent.transform.position;

            return cohesionMove;
        }
    }
}

