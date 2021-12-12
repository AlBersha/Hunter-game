using Behavior_Scripts;
using System;
using UnityEngine;

public class BehaviorAgent : MonoBehaviour
{
    [SerializeField]
    public EntityManager.EntityType entityType;
    public float MaxSpeed { set; get; }
    [NonSerialized]
    public Complex behavior;
    public float DetectRadius { set; get; }

    public void Move(Vector3 velocity)
    {
        transform.up = velocity;
        transform.position += velocity * Time.deltaTime;
    }
}
