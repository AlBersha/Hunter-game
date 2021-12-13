using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Behavior_Scripts
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
    public class Alignment : FlockingBehavior
    {
        public override Vector2 CalculateMove(FlockingAgent agent, List<Transform> context, Flock flock)
        {
            if (context.Count == 0)
                return agent.transform.up;

            var alignmentMove = context.Aggregate(Vector2.zero, (current, item) => current + (Vector2)item.transform.up);
            alignmentMove /= context.Count;

            return alignmentMove;
        }
    }
}

