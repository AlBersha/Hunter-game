using System;
using System.Collections.Generic;
using UnityEngine;

namespace Behavior_Scripts
{
    [Serializable]
    public class ComplexBehavior : Behavior
    {
        [SerializeField]
        public List<Behavior> behaviors = new List<Behavior>();
        public ComplexBehavior(List<BehaviorConfig> behaviorConfigs, BehaviorAgent agent)
            : base(agent)
        {
            behaviors.Add(BehaviorFactory.CreateAvoidEdgesBehavior(agent));

            foreach (var behaviorConfig in behaviorConfigs)
                behaviors.Add(BehaviorFactory.CreateTargetOrientedBehavior(behaviorConfig, agent));
        }

        public override Vector3 CalculateDesiredVelocity(Dictionary<EntityManager.EntityType, List<BehaviorAgent>> context)
        {
            var complexMove = Vector3.zero;

            Vector3 partialMove = Vector3.zero;
            for (var i = 0; i < behaviors.Count; i++)
            {
                partialMove = behaviors[i].CalculateDesiredVelocity(context);
                if (partialMove.sqrMagnitude < 0.0025f)
                    continue;

                partialMove *= behaviors[i].config.weight;
                complexMove += partialMove;
            }

            return Vector3.ClampMagnitude(complexMove, agent.MaxSpeed);
        }
    }
}
