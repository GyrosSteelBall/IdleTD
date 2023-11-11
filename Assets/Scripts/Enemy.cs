using UnityEngine;
using UnityEngine.Events;
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
