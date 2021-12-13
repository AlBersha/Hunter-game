using Behavior_Scripts;
using System;
using UnityEngine;

public class BehaviorAgent : MonoBehaviour
{
    [SerializeField]
    public EntityManager.EntityType entityType;
    [SerializeField]
    public float MaxSpeed { set; get; }
    [NonSerialized]
    public ComplexBehavior behavior;
    [SerializeField]
    public float DetectRadius { set; get; }
    public float SqrDetectRadius => DetectRadius * DetectRadius;
    public Vector3 Velocity { set; get; }
    public Vector3 Rotation { set; get; }
    public float Angle { set; get; }

    protected void RotateTo(Vector3 target)
    {
        Rotation = target;
        Angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Angle + 90));
    }

    protected void MoveTo(Vector3 target)
    {
        transform.position += target * Time.deltaTime;
        Velocity = target;
    }

    public void Move(Vector3 velocity, Vector3 direction)
    {
        MoveTo(velocity);
        RotateTo(direction);
    }
}
