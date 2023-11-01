using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public Path path;
    public float speed;
    public float health;

    private int currentWaypointIndex = 0;

    private void Start()
    {
        // Find the Path script in the scene and assign it as the enemy’s path
        path = FindObjectOfType<Path>();
        if (path == null)
        {
            Debug.LogError("No Path script found in the scene.");
        }
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (currentWaypointIndex < path.waypoints.Length)
        {
            Transform targetWaypoint = path.waypoints[currentWaypointIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

            if (transform.position == targetWaypoint.position)
            {
                currentWaypointIndex++;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Logic to handle the enemy's death, such as playing an animation or effect
        Destroy(gameObject);
    }
}
