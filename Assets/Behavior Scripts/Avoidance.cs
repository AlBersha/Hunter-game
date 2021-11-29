using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Behavior_Scripts
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
    public class Avoidance : FlockingBehavior
    {
        public override Vector2 CalculateMove(FlockingAgent agent, List<Transform> context, Flock flock)
        {
            if (context.Count == 0)
            {
                return Vector2.zero;
            }

            var avoid = 0;
            var avoidanceMove = Vector2.zero; 
            foreach (var item in context.Where(item => Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.squareAvoidanceRadius))
            {
                avoid++;
                avoidanceMove = (Vector2)(agent.transform.position - item.position);
            }

            if (avoid > 0)
            {
                avoidanceMove /= context.Count;
            }
    
            return avoidanceMove;
        }
    }
}

