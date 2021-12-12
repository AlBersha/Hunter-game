using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Behavior
{
    public Wander(BehaviorConfig config) : base(config) { }
    [SerializeField, Range(0.5f, 5)]
    private float circleDistance = 1;

    [SerializeField, Range(0.5f, 5)]
    private float circleRadius = 2;

    [SerializeField, Range(1, 80)]
    private int angleChangeStep = 15;

    private int angle = 0;

    public override Vector3 CalculateDesiredVelocity(Dictionary<EntityManager.EntityType, List<BehaviorAgent>> detectedEntities)
    {
        var rnd = Random.value;
        if (rnd < 0.5)
        {
            angle += angleChangeStep;
        }
        else if (rnd < 1)
        {
            angle -= angleChangeStep;
        }

        var futurePos = config.agent.transform.position + config.agent.transform.up * circleDistance;
        var vector = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * circleRadius;

        return (futurePos + vector - config.agent.transform.position).normalized * config.agent.MaxSpeed;
    }
}