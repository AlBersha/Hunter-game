class EntityFactory
{
    public static BehaviorAgent GetEntity(EntityManager.SpawnConfig config, string name)
    {
        BehaviorAgent agent;
        if (config.entityType == EntityManager.EntityType.Deer)
            agent = new BehaviorAgent();
        else if (config.entityType == EntityManager.EntityType.Rabbit)
            agent = new BehaviorAgent();
        else if (config.entityType == EntityManager.EntityType.Wolf)
            agent = new BehaviorAgent();
        else
            agent = new Hunter();

        agent.name = name;
        agent.entityType = config.entityType;
        agent.MaxSpeed = config.speed;
        agent.DetectRadius = config.detectRadius;
        agent.behavior = Behavior_Scripts.BehaviorFactory.CreateComplexBehavior(config.behaviorConfigs, agent);

        return agent;
    }
}