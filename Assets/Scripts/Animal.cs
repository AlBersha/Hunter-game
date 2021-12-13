using System.Collections;
using System.Collections.Generic;
using Behaviour;
using UnityEngine;

public class Animal : MonoBehaviour
{
    
    private Vector3 velocity;

    private Vector3 acceleration;

    [SerializeField]
    private float mass = 1;

    [SerializeField, Range(1, 20)]
    private float velocityLimit = 3;

    [SerializeField, Range(1, 50)]
    private float steeringForceLimit = 5;

    private Vector3 Target = new Vector3(0, 0, 1);

    private const float Epsilon = 0.01f;

    public float VelocityLimit => velocityLimit;    

    public Vector3 Velocity => velocity;

    public void ApplyForce(Vector3 force)
    {
        force /= mass;
        acceleration += force;
    }

    private void Update()
    {
        ApplyFriction();

        ApplySteeringForce();
        
        ApplyForces();

        void ApplyFriction()
        {
            var friction = -velocity.normalized * 0.5f;
            ApplyForce(friction);
        }

        void ApplySteeringForce()
        {
            var providers = GetComponents<DesiredVelocityProvider>();
            var steering = Vector3.zero;
            foreach (var provider in providers)
            {
                var desiredVelocity = provider.GetDesiredVelocity() * provider.Weight;
                steering += desiredVelocity - velocity;
                        
            }
            ApplyForce(Vector3.ClampMagnitude(steering - velocity, steeringForceLimit));

            // ApplyForce(steeringForce.normalized * steeringForceLimit);
        }

        void ApplyForces()
        {
            velocity += acceleration * Time.deltaTime;
            //limit velocity
            velocity = Vector3.ClampMagnitude(velocity, velocityLimit);
        
            //on small values object might start to blink, so we considering 
            //small velocities as zeroes
            if (velocity.magnitude < Epsilon)
            {
                velocity = Vector3.zero;
                return;
            }
        
            transform.position += velocity * Time.deltaTime;
            acceleration = Vector3.zero;
            // transform.rotation = Quaternion.LookRotation(velocity);
        }
    }
}
