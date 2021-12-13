using Behavior_Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [Serializable]
    public class SpawnConfig
    {
        public EntityType entityType;
        public BehaviorAgent agent;
        public List<Behavior.BehaviorConfig> behaviorConfigs = new List<Behavior.BehaviorConfig>();
        public int entitiesAmount;
        public int minNeighbourRadius;
        public float detectRadius;
        public float speed;
        public float rotationSpeed;
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
    [SerializeField]
    public BehaviorAgent hunter;
    [NonSerialized]
    public const int z_position = 0;

    private Dictionary<EntityType, List<BehaviorAgent>> GetEntitiesInDetectRadius(BehaviorAgent currentEntity)
    {
        Dictionary<EntityType, List<BehaviorAgent>> resultingDictionary = new Dictionary<EntityType, List<BehaviorAgent>>
        {
            { EntityType.Deer, new List<BehaviorAgent>() },
            { EntityType.Hunter, new List<BehaviorAgent>() },
            { EntityType.Rabbit, new List<BehaviorAgent>() },
            { EntityType.Wolf, new List<BehaviorAgent>() }
        };

        var currentPosition = currentEntity.transform.position;

        Vector3 entityPosition;
        Vector3 directionToTarget;
        foreach (BehaviorAgent entity in _entities)
        {
            if (entity == currentEntity)
                continue;

            entityPosition = entity.transform.position;
            directionToTarget = entityPosition - currentPosition;

            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < currentEntity.SqrDetectRadius)
                resultingDictionary[entity.entityType].Add(entity);
        }

        directionToTarget = hunter.transform.position - currentPosition;
        if (directionToTarget.sqrMagnitude < currentEntity.SqrDetectRadius)
            resultingDictionary[EntityType.Hunter].Add(hunter);

        return resultingDictionary;
    }

    private void Spawn(SpawnConfig config)
    {
        Vector2 planeSpawnPosition;
        Vector3 spawnPosition = Vector3.zero;
        for (int i = 0; i < config.entitiesAmount; i++)
        {
            planeSpawnPosition = UnityEngine.Random.insideUnitCircle * config.minNeighbourRadius;
            spawnPosition.x = planeSpawnPosition.x;
            spawnPosition.y = planeSpawnPosition.y;
            spawnPosition.z = z_position;

            var agent = Instantiate(
                config.agent,
                spawnPosition,
                Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(0f, 360f)),
                transform
            );

            agent.name = config.entityType.ToString() + i;
            agent.MaxSpeed = config.speed;
            agent.DetectRadius = config.detectRadius;

            agent.behavior = BehaviorFactory.CreateComplexBehavior(config.behaviorConfigs, agent);
            _entities.Add(agent);
        }
    }

    void Start()
    {
        foreach (var config in spawnConfigs)
            Spawn(config);
    }

    void Update()
    {
        foreach (BehaviorAgent entity in _entities)
        {
            var detectedNearbyEntities = GetEntitiesInDetectRadius(entity);

            Vector3 move = entity.behavior.CalculateDesiredVelocity(detectedNearbyEntities);
            move *= entity.MaxSpeed;
            move.z = z_position;

            entity.Move(move, move);
        }
    }
}