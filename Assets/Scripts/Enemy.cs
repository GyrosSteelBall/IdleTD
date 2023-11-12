using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [Header("Dependencies")]
    [SerializeField] private Slider healthBarSlider;
    [Header("Attributes")]
    public float speed;
    public float health;
    public float maxHealth; // Set this value to the enemy's maximum health

    private Path path;
    private int currentWaypointIndex = 0;
    public int goldValue;
    private float distanceTraveled; // Total distance traveled along the path

    [Header("Damage Feedback")]
    [SerializeField] private SpriteRenderer enemySpriteRenderer;

    private static Material defaultMaterial;
    private static Material blinkMaterial;
    private Coroutine damageCoroutine;

    [Header("Animations")]
    [SerializeField] private Animator animator; // Assign this in the Inspector
    [SerializeField] private string runAnimationName = "Run"; // Animator trigger for the running animation
    [SerializeField] private string deathAnimationName = "Die"; // Animator trigger for the death animation
    [SerializeField] private float deathAnimationDuration = 1.6f; // Duration in seconds
    public bool IsDead { get; private set; } = false;

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
        if (!IsDead)
        {
            Move();
            UpdateAnimationDirection();
        }
    }

    void Move()
    {
        if (currentWaypointIndex < path.waypoints.Length)
        {
            Transform targetWaypoint = path.waypoints[currentWaypointIndex];
            Vector3 previousPosition = transform.position;
            Vector3 direction = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime) - transform.position;

            if (direction != Vector3.zero)
            {
                // For 2D sprites in a 2D game
                enemySpriteRenderer.flipX = direction.x < 0;
            }

            transform.position += direction;

            // Add the distance moved this frame to the total distance traveled
            distanceTraveled += Vector3.Distance(previousPosition, transform.position);

            if (transform.position == targetWaypoint.position)
            {
                currentWaypointIndex++;
            }

            // Play running animation if not already playing
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName(runAnimationName))
            {
                animator.SetTrigger(runAnimationName);
            }
        }
    }

    void UpdateAnimationDirection()
    {
        // Depending on your game, you may need more complex logic to determine the facing direction (e.g., 8-way movement)
    }

    public void TakeDamage(float damage)
    {
        if (!IsDead)
        {
            health -= damage;
            healthBarSlider.value = health; // Update the slider value whenever the enemy takes damage
            Blink();
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void Blink()
    {
        if (defaultMaterial == null) defaultMaterial = enemySpriteRenderer.sharedMaterial;
        if (blinkMaterial == null) blinkMaterial = new Material(Shader.Find("GUI/Text Shader"));

        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }
        damageCoroutine = StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        enemySpriteRenderer.material = blinkMaterial;

        yield return new WaitForSeconds(0.1f);

        enemySpriteRenderer.material = defaultMaterial;
    }

    void Die()
    {
        IsDead = true; // Stop the enemy from moving or performing actions
        // Play the death animation
        animator.SetTrigger(deathAnimationName);
        DropResources(); // Drop resources before cleanup

        // Start the coroutine to wait before removal
        StartCoroutine(WaitBeforeRemoval(deathAnimationDuration));
    }

    IEnumerator WaitBeforeRemoval(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Call the cleanup method after the delay
        RemoveEnemy();
    }

    public void RemoveEnemy()
    {
        // Logic to handle the enemy's cleanup after death
        Destroy(gameObject);
    }

    void DropResources()
    {
        GameManager.instance.gold += goldValue;
    }
}