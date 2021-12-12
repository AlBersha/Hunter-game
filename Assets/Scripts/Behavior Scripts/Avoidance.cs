using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Behavior_Scripts
{
    [Serializable]
    public class Avoidance : Behavior
    {
        public Avoidance(BehaviorConfig config) : base(config) { }
        public override Vector3 CalculateDesiredVelocity(Dictionary<EntityManager.EntityType, List<BehaviorAgent>> detectedEntities)
        {
            if (detectedEntities[config.targetType].Count == 0)
                return Vector3.zero;

            Vector3 avoidanceMove = Vector3.zero;
            foreach (var entity in detectedEntities[config.targetType].Where(item => Vector3.SqrMagnitude(item.transform.position - config.agent.transform.position) < config.agent.DetectRadius * config.weight))
            {
                avoidanceMove += config.agent.transform.position - entity.transform.position;
            }

            return avoidanceMove;
        }
    }
}

