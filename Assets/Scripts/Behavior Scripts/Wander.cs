using Behavior_Scripts;
using System.Collections.Generic;
using UnityEngine;

public class Wander : TargetOrientedBehavior
{
    public Wander(BehaviorConfig config, BehaviorAgent agent) : base(config, agent) { }
    private float circleDistance = 1;
    private float circleRadius = 2;
    private int angleChangeStep = 30;

    public override Vector3 CalculateDesiredVelocity(Dictionary<EntityManager.EntityType, List<BehaviorAgent>> detectedEntities)
    {
        float rand = Random.value;
        int randomAngle = (int)(rand * angleChangeStep);
        float angle = rand >= 0.5f ? agent.Angle + randomAngle : agent.Angle - randomAngle;

        var futurePos = agent.transform.position + agent.Velocity * circleDistance;
        var vector = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * circleRadius;

        return (futurePos + vector - agent.transform.position) * agent.MaxSpeed;
    }
}