using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockingAgent agentPrefab;
    private List<FlockingAgent> _agents = new List<FlockingAgent>();
    public FlockingBehavior behavior;

    [Range(3, 1000)] public int staringCount = 250;
    private const float AgentDensity = 0.08f;

    [Range(1f, 100f)] public float driveFactor = 10f;
    [Range(1f, 100f)] public float maxSpeed = 5f;
    [Range(1f, 10f)] public float neighborRadius = 1.5f;
    [Range(0f, 1f)] public float avoidanceRadiusMultiplier = .5f;

    private float _squareMaxSpeed;
    private float _squareNeighborRadius;
    public float squareAvoidanceRadius { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        _squareMaxSpeed = maxSpeed * maxSpeed;
        _squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = _squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < staringCount; i++)
        {
            var agent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * staringCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
            );
            agent.name = "Rabbit" + i;
            _agents.Add(agent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var agent in _agents)
        {
            var context = GetNearbyObjects(agent);
            
            var move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            
            if (move.sqrMagnitude > _squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockingAgent agent)
    {
        var contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius); 

        return (from collider in contextColliders where collider != agent.AgentCollider select collider.transform).ToList();
    }
}
