using System;
using UnityEngine;

public class HunterManager : MonoBehaviour
{
    public BehaviorAgent hunter;
    public BehaviorAgent attackTarget;
    public EntityManager entityManager;

    void Start()
    {
        Vector2 planeSpawnPosition = UnityEngine.Random.insideUnitCircle * 100;

        Vector3 spawnPosition = Vector3.zero;
        spawnPosition.x = planeSpawnPosition.x;
        spawnPosition.y = planeSpawnPosition.y;
        spawnPosition.z = EntityManager.z_position;

        hunter = Instantiate(
            hunter,
            spawnPosition,
            Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(0f, 360f)),
            transform
        );

        hunter.MaxSpeed = 3f;
        hunter.DetectRadius = 200;
    }

    void FixedUpdate()
    {
        hunter.Move(GetKeyboardInput(), GetMouseInput());

        if (HasAttackedEntity())
        {
            hunter.Attack(attackTarget);
        }
    }

    private bool HasAttackedEntity()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var entitiesDict = entityManager.GetEntitiesInDetectRadius(hunter);
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
            movement += Vector3.up * hunter.MaxSpeed;

        if (Input.GetKey(KeyCode.S))
            movement += Vector3.down * hunter.MaxSpeed;

        if (Input.GetKey(KeyCode.A))
            movement += Vector3.left * hunter.MaxSpeed;

        if (Input.GetKey(KeyCode.D))
            movement += Vector3.right * hunter.MaxSpeed;

        return movement;
    }

    private Vector3 GetMouseInput()
    {
        return Input.mousePosition - Camera.main.WorldToScreenPoint(hunter.transform.position);
    }
}
