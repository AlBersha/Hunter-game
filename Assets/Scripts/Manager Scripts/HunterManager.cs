using System;
using UnityEngine;

public class HunterManager : MonoBehaviour
{
    public BehaviorAgent attackTarget;
    public EntityManager entityManager;

    void Start()
    {
        Vector2 planeSpawnPosition = UnityEngine.Random.insideUnitCircle * 100;

        Vector3 spawnPosition = Vector3.zero;
        spawnPosition.x = planeSpawnPosition.x;
        spawnPosition.y = planeSpawnPosition.y;
        spawnPosition.z = EntityManager.z_position;

        entityManager.hunter = Instantiate(
            entityManager.hunter,
            spawnPosition,
            Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(0f, 360f)),
            transform
        );

        entityManager.hunter.MaxSpeed = 3f;
        entityManager.hunter.Health = 50;
        entityManager.hunter.DetectRadius = 200;
    }

    void FixedUpdate()
    {
        if (entityManager.hunter.IsAlive())
        {
            entityManager.hunter.Move(GetKeyboardInput(), GetMouseInput());

            if (HasAttackedEntity())
            {
                entityManager.hunter.Attack(attackTarget);
            }
        }
    }

    private bool HasAttackedEntity()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var entitiesDict = entityManager.GetEntitiesInDetectRadius(entityManager.hunter);
            foreach (var dictElement in entitiesDict)
            {
                foreach (var entity in dictElement.Value)
                {
                    if (IsEntityHit(entity))
                    {
                        attackTarget = entity;
                        return true;
                    }
                }
            }
        }
        return false;

        bool IsEntityHit(BehaviorAgent entity)
        {
            var entityPosition = Camera.main.WorldToScreenPoint(entity.transform.position);
            if (entityPosition.x >= Input.mousePosition.x - 10
                && entityPosition.x <= Input.mousePosition.x + 10
                && entityPosition.y >= Input.mousePosition.y - 10
                && entityPosition.y <= Input.mousePosition.y + 10)
                return true;
            return false;
        }
    }

    private Vector3 GetKeyboardInput()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            movement += Vector3.up * entityManager.hunter.MaxSpeed;

        if (Input.GetKey(KeyCode.S))
            movement += Vector3.down * entityManager.hunter.MaxSpeed;

        if (Input.GetKey(KeyCode.A))
            movement += Vector3.left * entityManager.hunter.MaxSpeed;

        if (Input.GetKey(KeyCode.D))
            movement += Vector3.right * entityManager.hunter.MaxSpeed;

        return movement;
    }

    private Vector3 GetMouseInput()
    {
        return Input.mousePosition - Camera.main.WorldToScreenPoint(entityManager.hunter.transform.position);
    }
}