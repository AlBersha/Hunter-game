using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Behavior_Scripts
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
    public class Cohesion : FlockingBehavior
    {
        public override Vector2 CalculateMove(FlockingAgent agent, List<Transform> context, Flock flock)
        {
            if (context.Count == 0)
            {
                return Vector2.zero;
            }

            var cohesionMove = context.Aggregate(Vector2.zero, (current, item) => current + (Vector2)item.position);
            cohesionMove /= context.Count;
    
            //offset from agent position
            cohesionMove -= (Vector2)agent.transform.position;

            return cohesionMove;
        }
    }
}

