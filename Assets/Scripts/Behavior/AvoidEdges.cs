using System;
using System.Collections.Generic;
using Behaviour;

namespace Behavior
{
    using UnityEngine;

    public class AvoidEdges : DesiredVelocityProvider
    {
        private float edge = 0.05f;

        [SerializeField]
        private List<Transform> Boundaries = new List<Transform>() { };

        public override Vector3 GetDesiredVelocity()
        {
            var cam = Camera.main;
            var maxSpeed = Animal.VelocityLimit;
            var v = Animal.Velocity;
            // if (cam == null)
            // {
            //     return v;
            // }

            var point = transform.position;// cam.WorldToViewportPoint(transform.position);

            foreach (var edge in Boundaries)
            {
                if (Mathf.Abs(point.x - (edge.position).x) < 1.0)
                {
                    return new Vector3(-maxSpeed, 0, 0);
                }
                // if (point.x < cam.WorldToViewportPoint(edge.position).x)
                // {
                //     return new Vector3(maxSpeed, 0, 0);
                // }
                if (Mathf.Abs(point.y - (edge.position).y) < 1.0)
                {
                    return new Vector3(0, -maxSpeed, 0 );
                }
                // if (point.y < cam.WorldToViewportPoint(edge.position).y)
                // {
                //     return new Vector3(0, maxSpeed, 0);
                // }
            }
            
            // if (point.x > 1 - edge)
            // {
            //     return new Vector3(-maxSpeed, 0, 0);
            //     
            // }
            // if (point.x < edge)
            // {
            //     return new Vector3(maxSpeed, 0, 0);
            // }
            // if (point.y > 1 - edge)
            // {
            //     return new Vector3(0, -maxSpeed, 0);
            // }
            // if (point.y < edge)
            // {
            //     return new Vector3(0, maxSpeed, 0);
            // }

            return v;
        }
    }
}