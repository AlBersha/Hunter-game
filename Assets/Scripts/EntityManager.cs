using Behavior_Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [Serializable]
    public struct SpawnConfig
    {
        public EntityType entityType;
        public BehaviorAgent agent;
        public List<Behavior.BehaviorConfig> behaviorConfigs;
        public int entitiesAmount;
        public int minNeighbourRadius;
        public float detectRadius;
        public float speed;
    }
    public enum EntityType
    {
        Wolf,
        Rabbit,
        Deer,
        Hunter
    }
    [SerializeField]
    public List<SpawnConfig> spawnConfigs = new List<SpawnConfig>();
    private List<BehaviorAgent> _entities = new List<BehaviorAgent>();

    private Dictionary<EntityType, List<BehaviorAgent>> GetEntitiesInDetectRadius(BehaviorAgent currentEntity)
    {
        Dictionary<EntityType, List<BehaviorAgent>> resultingDictionary = new Dictionary<EntityType, List<BehaviorAgent>>
        {
            { EntityType.Deer, new List<BehaviorAgent>() },
            { EntityType.Hunter, new List<BehaviorAgent>() },
            { EntityType.Rabbit, new List<BehaviorAgent>() },
            { EntityType.Wolf, new List<BehaviorAgent>() }
        };

        var detectRadius = currentEntity.DetectRadius;
        var currentPosition = currentEntity.transform.position;

        Vector3 entityPosition;
        foreach (BehaviorAgent entity in _entities)
        {
            if (entity == currentEntity)
                continue;

            entityPosition = entity.transform.position;
            Vector3 directionToTarget = entityPosition - currentPosition;

            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < detectRadius)
                resultingDictionary[entity.entityType].Add(entity);
        }

        return resultingDictionary;
    }

    void Start()
    {
        foreach (var config in spawnConfigs)
        {
            Spawn(config);
        }

        void Spawn(SpawnConfig config)
        {
            for (int i = 0; i < config.entitiesAmount; i++)
            {
                var agent = Instantiate(
                    config.agent,
                    UnityEngine.Random.insideUnitCircle * config.minNeighbourRadius,
                    Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(0f, 360f)),
                    transform
                );
                agent.name = config.entityType.ToString() + i;
                agent.MaxSpeed = config.speed;
                agent.DetectRadius = config.detectRadius;
                agent.behavior = BehaviorFactory.CreateComplexBehavior(config.behaviorConfigs);
                _entities.Add(agent);
            }
        }
    }

    void Update()
    {
        foreach (BehaviorAgent entity in _entities)
        {
            var detectedNearbyEntities = GetEntitiesInDetectRadius(entity);

            Vector3 move = entity.behavior.CalculateDesiredVelocity(detectedNearbyEntities);
            move *= entity.MaxSpeed;
            entity.Move(move);
        }
    }
}