using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behavior
{
    public enum BehaviorType
    {
        Cohesion,
        Avoidance,
        Alignment,
        Wander,
        AvoidEdges
    }
    [Serializable]
    public class BehaviorConfig
    {
        public BehaviorType type;
        public EntityManager.EntityType targetType;
        [Range(0f, 1f)]
        public float weight;
    }
    public BehaviorConfig config;
    public BehaviorAgent agent;
    public Behavior(BehaviorAgent agent) => this.agent = agent;
    public abstract Vector3 CalculateDesiredVelocity(Dictionary<EntityManager.EntityType, List<BehaviorAgent>> detectedEntities);
}
