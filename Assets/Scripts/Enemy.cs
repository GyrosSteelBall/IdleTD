using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Path path;
    public float speed;
    public float health;
    public float maxHealth; // Set this value to the enemy's maximum health
    public Slider healthBarSlider; // Reference to the health bar slider

    private int currentWaypointIndex = 0;
    public int goldValue;
    private float distanceTraveled; // Total distance traveled along the path

    // Property to access distance traveled
    public float DistanceAlongPath
    {
        get { return distanceTraveled; }
    }

    private void Start()
    {
        // Find the Path script in the scene and assign it as the enemy’s path
        path = FindObjectOfType<Path>();
        if (path == null)
        {
            Debug.LogError("No Path script found in the scene.");
        }

        // Initialize the health bar
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = health;

        distanceTraveled = 0f; // Starting with zero distance
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
            Vector3 previousPosition = transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

            // Add the distance moved this frame to the total distance traveled
            distanceTraveled += Vector3.Distance(previousPosition, transform.position);

            if (transform.position == targetWaypoint.position)
            {
                currentWaypointIndex++;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBarSlider.value = health; // Update the slider value whenever the enemy takes damage
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Logic to handle the enemy's death, such as playing an animation or effect
        DropResources(); // Drop resources before dying
        Destroy(gameObject);
    }

    void DropResources()
    {
        GameManager.instance.gold += goldValue;
    }
}