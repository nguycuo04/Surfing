using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ButterflyBoid : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    public float neighborRadius = 5f;
    public float separationRadius = 2f;
    private Vector2 flutterOffset;
    public LayerMask obstacleMask; // Layer to identify obstacles
    public float obstacleAvoidanceRadius = 2f; // Radius for detecting obstacles
    public float obstacleAvoidanceWeight = 10f; // Weight for obstacle avoidance force
    [HideInInspector] public Vector2 velocity;

    private ButterflyManager manager;

    void Start()
    {
        velocity = Random.insideUnitCircle.normalized * speed;
        manager = FindObjectOfType<ButterflyManager>();
        flutterOffset = Random.insideUnitCircle * 0.5f;
    }

    void Update()
    {
        MoveBoid();
    }

    void MoveBoid()
    {
        Vector2 separation = Separate();
        Vector2 alignment = Align();
        Vector2 cohesion = Cohere();
        Vector2 obstacleAvoidance = AvoidObstacles();

        // Combine the forces
        Vector2 acceleration = separation + alignment + cohesion + obstacleAvoidance;

        // Apply boundary force
        Vector2 boundaryForce = StayWithinBounds(manager.areaCenter, manager.areaSize);
        acceleration += boundaryForce;

        velocity += acceleration * Time.deltaTime;
        velocity = Vector2.ClampMagnitude(velocity, speed);

        transform.position += (Vector3)velocity * Time.deltaTime;

        // Face the movement direction
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    Vector2 AvoidObstacles()
    {
        Collider2D[] obstacles = Physics2D.OverlapCircleAll(transform.position, obstacleAvoidanceRadius, obstacleMask);
        Vector2 avoidanceForce = Vector2.zero;

        foreach (Collider2D obstacle in obstacles)
        {
            Vector2 diff = (Vector2)(transform.position - obstacle.transform.position);
            float distance = diff.magnitude;

            // Calculate repulsion force inversely proportional to distance
            avoidanceForce += diff.normalized / distance;
        }

        return avoidanceForce.normalized * obstacleAvoidanceWeight;
    }
    Vector2 StayWithinBounds(Vector2 center, Vector2 size)
    {
        Vector2 force = Vector2.zero;
        Vector2 minBounds = center - size / 2;
        Vector2 maxBounds = center + size / 2;

        if (transform.position.x < minBounds.x)
            force.x = 1;
        else if (transform.position.x > maxBounds.x)
            force.x = -1;

        if (transform.position.y < minBounds.y)
            force.y = 1;
        else if (transform.position.y > maxBounds.y)
            force.y = -1;

        return force.normalized * speed; // Apply speed to smoothly bring boids back
    }

    Vector2 Separate()
    {
        Vector2 force = Vector2.zero;
        int count = 0;

        foreach (var other in manager.butterflies)
        {
            if (other == this) continue;

            float distance = Vector2.Distance(transform.position, other.transform.position);
            if (distance < separationRadius)
            {
                Vector2 diff = (Vector2)(transform.position - other.transform.position);
                diff /= distance;
                force += diff;
                count++;
            }
        }

        return count > 0 ? (force / count).normalized : Vector2.zero;
    }

    Vector2 Align()
    {
        Vector2 averageVelocity = Vector2.zero;
        int count = 0;

        foreach (var other in manager.butterflies)
        {
            if (other == this) continue;

            float distance = Vector2.Distance(transform.position, other.transform.position);
            if (distance < neighborRadius)
            {
                averageVelocity += other.velocity;
                count++;
            }
        }

        return count > 0 ? (averageVelocity / count).normalized : Vector2.zero;
    }

    Vector2 Cohere()
    {
        Vector2 centerOfMass = Vector2.zero;
        int count = 0;

        foreach (var other in manager.butterflies)
        {
            if (other == this) continue;

            float distance = Vector2.Distance(transform.position, other.transform.position);
            if (distance < neighborRadius)
            {
                centerOfMass += (Vector2)other.transform.position;
                count++;
            }
        }

        if (count > 0)
        {
            centerOfMass /= count;
            return ((centerOfMass - (Vector2)transform.position).normalized);
        }

        return Vector2.zero;
    }
}

