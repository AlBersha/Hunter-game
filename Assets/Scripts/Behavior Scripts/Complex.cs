using System;
using System.Collections.Generic;
using UnityEngine;

namespace Behavior_Scripts
{
    [Serializable]
    public class Complex : Behavior
    {
        [SerializeField]
        public List<Behavior> behaviors = new List<Behavior>();
        public Complex(List<BehaviorConfig> behaviorConfigs) : base()
        {
            foreach (var behaviorConfig in behaviorConfigs)
                behaviors.Add(BehaviorFactory.CreateBehavior(behaviorConfig));
        }

        public override Vector3 CalculateDesiredVelocity(Dictionary<EntityManager.EntityType, List<BehaviorAgent>> context)
        {
            var move = Vector3.zero;

            for (var i = 0; i < behaviors.Count; i++)
            {
                var currentBehaviorWeight = behaviors[i].config.weight;
                var partialMove = behaviors[i].CalculateDesiredVelocity(context) * currentBehaviorWeight;

                if (partialMove == Vector3.zero) continue;
                if (partialMove.sqrMagnitude > currentBehaviorWeight * currentBehaviorWeight)
                {
                    partialMove.Normalize();
                    partialMove *= currentBehaviorWeight;
                }

                move += partialMove;
            }

            return move;
        }
    }
}
