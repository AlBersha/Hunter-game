using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Behavior_Scripts
{
    [Serializable]
    public class Alignment : Behavior
    {
        public Alignment(BehaviorConfig config) : base(config) { }
        public override Vector3 CalculateDesiredVelocity(Dictionary<EntityManager.EntityType, List<BehaviorAgent>> detectedEntities)
        {
            if (detectedEntities[config.targetType].Count == 0)
                return config.agent.transform.up;

            var alignmentMove = detectedEntities[config.targetType].Aggregate(Vector3.zero, (current, item) => current + item.transform.up);
            alignmentMove /= detectedEntities[config.targetType].Count;

            return alignmentMove;
        }
    }
}

