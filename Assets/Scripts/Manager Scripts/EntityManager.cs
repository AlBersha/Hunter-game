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
    private List<BehaviorAgent> _deadEntities = new List<BehaviorAgent>();
    [SerializeField]
    public BehaviorAgent hunter;
    [NonSerialized]
    public const int z_position = 0;

    public Dictionary<EntityType, List<BehaviorAgent>> GetEntitiesInDetectRadius(BehaviorAgent currentEntity)
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

        if (currentEntity == hunter)
            return resultingDictionary;

        directionToTarget = hunter.transform.position - currentPosition;
        if (directionToTarget.sqrMagnitude < currentEntity.SqrDetectRadius)
            resultingDictionary[EntityType.Hunter].Add(hunter);

        return resultingDictionary;
    }

    private void Spawn(SpawnConfig config)
    {
        for (int i = 0; i < config.entitiesAmount; i++)
        {
            var agent = Instantiate(
                config.agent,
                GetRandomSpawnPosition(),
                Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(0f, 360f)),
                transform
            );

            agent.name = config.entityType.ToString() + i;
            agent.MaxSpeed = config.speed;
            agent.Health = 15;
            agent.DetectRadius = config.detectRadius;

            agent.behavior = BehaviorFactory.CreateComplexBehavior(config.behaviorConfigs, agent);
            _entities.Add(agent);
        }

        Vector3 GetRandomSpawnPosition()
        {
            Vector2 planeSpawnPosition = UnityEngine.Random.insideUnitCircle * config.minNeighbourRadius;

            Vector3 spawnPosition;
            spawnPosition.x = planeSpawnPosition.x;
            spawnPosition.y = planeSpawnPosition.y;
            spawnPosition.z = z_position;


            return spawnPosition;
        }
    }

    void Start()
    {
        foreach (var config in spawnConfigs)
            Spawn(config);
    }

    void FixedUpdate()
    {
        ProcessMovement();

        ProcessDeadEntities();

        void ProcessMovement()
        {
            foreach (BehaviorAgent entity in _entities)
            {
                var detectedNearbyEntities = GetEntitiesInDetectRadius(entity);

                Vector3 move = entity.behavior.CalculateDesiredVelocity(detectedNearbyEntities);
                move *= entity.MaxSpeed;
                move.z = z_position;

                entity.Move(move, move);

                CheckPossibleAttackTargets(entity, detectedNearbyEntities);

                if (HasFallenOfTheLand(entity.transform.position) || !entity.IsAlive())
                    _deadEntities.Add(entity);
            }
        }
    }

    private void CheckPossibleAttackTargets(BehaviorAgent currentEntity, Dictionary<EntityType, List<BehaviorAgent>> detectedNearbyEntities)
    {
        const int hitRadius = 5;

        if (currentEntity.entityType == EntityType.Wolf)
        {
            foreach (var dictElement in detectedNearbyEntities)
            {
                foreach (var entity in dictElement.Value)
                {
                    if (IsEntityHit(currentEntity, entity))
                    {
                        currentEntity.Attack(entity);
                        return;
                    }
                }
            }
        }

        bool IsEntityHit(BehaviorAgent currentEntity, BehaviorAgent targetEntity)
        {
            var targetEntityPosition = targetEntity.transform.position;
            var currentEntityPosition = currentEntity.transform.position;
            if (currentEntityPosition.x >= targetEntityPosition.x - hitRadius
                && currentEntityPosition.x <= targetEntityPosition.x + hitRadius
                && currentEntityPosition.y >= targetEntityPosition.y - hitRadius
                && currentEntityPosition.y <= targetEntityPosition.y + hitRadius)
                return true;
            return false;
        }
    }


    private void ProcessDeadEntities()
    {
        foreach (BehaviorAgent deadEntity in _deadEntities)
        {
            _entities.Remove(deadEntity);
            Destroy(deadEntity);
        }

        _deadEntities.Clear();
    }

    private bool HasFallenOfTheLand(Vector3 entityPosition)
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(entityPosition);
        return (pos.x < 0.0
            || 1.0 < pos.x
            || pos.y < 0.0
            || 1.0 < pos.y);
    }
}