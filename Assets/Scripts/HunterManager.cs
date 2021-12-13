using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HunterManager : MonoBehaviour
{
    public BehaviorAgent hunter;

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
    }

    void FixedUpdate()
    {
        hunter.Move(GetKeyboardInput(), GetMouseInput());
        hunter.Attack();
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
