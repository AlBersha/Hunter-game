using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Behavior
{
    public enum BehaviorType
    {
        Cohesion,
        Avoidance,
        Alignment,
        Wander,
        Complex
    }
    [Serializable]
    public struct BehaviorConfig
    {
        public BehaviorType type;
        public BehaviorAgent agent;
        public EntityManager.EntityType targetType;
        public float weight;
    }
    public BehaviorConfig config;
    protected Behavior(BehaviorConfig config)
    {
        this.config = config;
    }
    protected Behavior()
    {
    }

    public abstract Vector3 CalculateDesiredVelocity(Dictionary<EntityManager.EntityType, List<BehaviorAgent>> detectedEntities);
}
