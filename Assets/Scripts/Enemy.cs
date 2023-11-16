using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Slider healthBarSlider;

    [Header("Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private int goldValue;

    private Path path;
    private int currentWaypointIndex = 0;
    private float distanceTraveled;

    [Header("Damage Feedback")]
    [SerializeField] private SpriteRenderer enemySpriteRenderer;

    private static Material defaultMaterial;
    private static Material blinkMaterial;
    private Coroutine damageCoroutine;

    [Header("Animations")]
    [SerializeField] private Animator animator;
    private static readonly int RunAnimationHash = Animator.StringToHash("Run");
    private static readonly int DeathAnimationHash = Animator.StringToHash("Die");
    private const float DeathAnimationDuration = 1.6f;

    public bool IsDead { get; private set; }
    public float DistanceAlongPath => distanceTraveled;

    private void Awake()
    {
        defaultMaterial = enemySpriteRenderer.sharedMaterial;
        blinkMaterial = new Material(Shader.Find("GUI/Text Shader"));
    }

    private void Start()
    {
        path = FindObjectOfType<Path>();
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = health;

        if (path == null)
        {
            Debug.LogError("No Path script found in the scene.");
            enabled = false; // Disable script if no path
        }
    }

    void Update()
    {
        if (!IsDead)
        {
            Move();
        }
    }

    private void Move()
    {
        if (currentWaypointIndex >= path.waypoints.Length)
        {
            return;
        }

        var targetWaypoint = path.waypoints[currentWaypointIndex];
        var previousPosition = transform.position;
        transform.position = Vector3.MoveTowards(previousPosition, targetWaypoint.position, speed * Time.deltaTime);

        distanceTraveled += Vector3.Distance(previousPosition, transform.position);

        if (transform.position == targetWaypoint.position)
        {
            currentWaypointIndex++;
        }

        UpdateAnimation(RunAnimationHash);
        UpdateSpriteDirection(previousPosition);
    }

    private void UpdateSpriteDirection(Vector3 previousPosition)
    {
        float deltaX = transform.position.x - previousPosition.x;
        enemySpriteRenderer.flipX = deltaX < 0;
    }

    private void UpdateAnimation(int animationHash)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash != animationHash)
        {
            animator.SetTrigger(animationHash);
        }
    }

    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        health -= damage;
        healthBarSlider.value = health;
        Blink();

        if (health <= 0) Die();
    }

    public void Blink()
    {
        if (damageCoroutine != null) StopCoroutine(damageCoroutine);
        damageCoroutine = StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        enemySpriteRenderer.material = blinkMaterial;
        yield return new WaitForSeconds(0.1f);
        enemySpriteRenderer.material = defaultMaterial;
    }

    private void Die()
    {
        IsDead = true;
        UpdateAnimation(DeathAnimationHash);
        DropResources();
        StartCoroutine(WaitBeforeRemoval(DeathAnimationDuration));
    }

    private IEnumerator WaitBeforeRemoval(float delay)
    {
        yield return new WaitForSeconds(delay);
        RemoveEnemy();
    }

    private void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    private void DropResources()
    {
        GameManager.Instance.AddGold(goldValue); // Assuming AddGold is a method that safely adds gold to the GameManager.
    }
}