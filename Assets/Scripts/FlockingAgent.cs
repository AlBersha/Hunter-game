using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockingAgent : MonoBehaviour
{
    public Collider2D AgentCollider { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        AgentCollider = GetComponent<Collider2D>();
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
