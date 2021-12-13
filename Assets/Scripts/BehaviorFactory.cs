using System;
using System.Collections.Generic;
using UnityEngine;

namespace Behavior_Scripts
{
    public class BehaviorFactory
    {
        class AvoidEdges : Behavior
        {
            public AvoidEdges(BehaviorAgent agent) : base(agent)
            {
                config = new BehaviorConfig();
                config.weight = 5f;
            }
            public override Vector3 CalculateDesiredVelocity(Dictionary<EntityManager.EntityType, List<BehaviorAgent>> detectedEntities)
            {
                var maxSpeed = agent.MaxSpeed;
                Vector3 pos = Camera.main.WorldToViewportPoint(agent.transform.position);

                if (pos.x < 0.0)
                    return new Vector3(maxSpeed, 0, 0);
                if (1.0 < pos.x)
                    return new Vector3(-maxSpeed, 0, 0);
                if (pos.y < 0.0)
                    return new Vector3(0, maxSpeed, 0);
                if (1.0 < pos.y)
                    return new Vector3(0, -maxSpeed, 0);

                return agent.Velocity;
            }
        }

        public static TargetOrientedBehavior CreateTargetOrientedBehavior(Behavior.BehaviorConfig config, BehaviorAgent agent)
        {
            if (config.type == Behavior.BehaviorType.Cohesion)
                return new Cohesion(config, agent);
            else if (config.type == Behavior.BehaviorType.Alignment)
                return new Alignment(config, agent);
            else if (config.type == Behavior.BehaviorType.Avoidance)
                return new Avoidance(config, agent);
            else
                return new Wander(config, agent);
        }

        public static ComplexBehavior CreateComplexBehavior(List<Behavior.BehaviorConfig> behaviorConfigs, BehaviorAgent agent)
        {
            return new ComplexBehavior(behaviorConfigs, agent);
        }
        public static Behavior CreateAvoidEdgesBehavior(BehaviorAgent agent)
        {
            return new AvoidEdges(agent);
        }
    }
}
