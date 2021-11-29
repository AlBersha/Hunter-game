using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Behavior_Scripts{
    [CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
    public class Composite : FlockingBehavior
    {
        public List<FlockingBehavior> behaviors;
        public List<float> weights;
        
        public override Vector2 CalculateMove(FlockingAgent agent, List<Transform> context, Flock flock)
        {
            if (weights.Count != behaviors.Count)
            {
                Debug.LogError("Data mismatch in " + name, this);
                return Vector2.zero;
            }

            var move = Vector2.zero;

            for (var i = 0; i < behaviors.Count; i++)
            {
                var partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];

                if (partialMove == Vector2.zero) continue;
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }

            return move;
        }
    }
}
