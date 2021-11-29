using System.Collections.Generic;
using UnityEngine;

public abstract class FlockingBehavior : ScriptableObject
{
   public abstract Vector2 CalculateMove(FlockingAgent agent, List<Transform> context, Flock flock);
}
