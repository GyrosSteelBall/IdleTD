using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private EnemyConfigSO enemyConfig;
    [SerializeField] private DamageFeedbackConfigSO damageFeedbackConfig;

    [Header("Damage Feedback")]
    [SerializeField] private SpriteRenderer enemySpriteRenderer;
    private Coroutine damageCoroutine;

    [Header("Animations")]
    [SerializeField] private Animator animator;

    private Path path;
    private int currentWaypointIndex = 0;
    private float distanceTraveled;
    private float health;
    private static readonly int RunAnimationHash = Animator.StringToHash("Run");
    private static readonly int DeathAnimationHash = Animator.StringToHash("Die");
    private const float DeathAnimationDuration = 1.6f;
    private Material defaultMaterial;

    public bool IsDead { get; private set; }
    public float DistanceAlongPath => distanceTraveled;

    private void Awake()
    {
        if (enemySpriteRenderer != null)
        {
            defaultMaterial = enemySpriteRenderer.sharedMaterial;
        }

        health = enemyConfig != null ? enemyConfig.maxHealth : 100;

        if (healthBarSlider != null)
        {
            healthBarSlider.maxValue = health;
            healthBarSlider.value = health;
        }

        if (damageFeedbackConfig != null && damageFeedbackConfig.blinkMaterial == null)
        {
            damageFeedbackConfig.blinkMaterial = new Material(Shader.Find("GUI/Text Shader"));
        }
    }

    private void Start()
    {
        path = FindObjectOfType<Path>();
        if (path == null)
        {
            Debug.LogError("No Path script found in the scene.");
            enabled = false;
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
        transform.position = Vector3.MoveTowards(previousPosition, targetWaypoint.position, enemyConfig.speed * Time.deltaTime);

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
        damageCoroutine = StartCoroutine(BlinkCoroutine(damageFeedbackConfig.blinkDuration));
    }

    private IEnumerator BlinkCoroutine(float duration)
    {
        enemySpriteRenderer.material = damageFeedbackConfig.blinkMaterial;
        yield return new WaitForSeconds(duration);
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
        GameManager.Instance.AddGold(enemyConfig.goldValue); // Assuming AddGold is a method that safely adds gold to the GameManager.
    }
}