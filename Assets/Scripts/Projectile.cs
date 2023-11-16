using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;

    protected Enemy target;
    protected float damage;

    // Unity's MonoBehaviour start method is virtual; let's make use of this in derived classes
    protected virtual void Start()
    {
        MoveToTarget();
    }

    // Added FixedUpdate for physics-related movement
    protected virtual void FixedUpdate()
    {
        MoveToTarget();
    }

    public void Initialize(Enemy target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    protected void MoveToTarget()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        // Utilize a distance check instead of position equality because of floating point precision issues
        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            HitTarget();
        }
    }

    // This method is already virtual, which is good for inheritance
    protected virtual void HitTarget()
    {
        target.TakeDamage(damage);
        DestroyProjectile();
    }

    protected void DestroyProjectile()
    {
        // If you have a delegate or Unity event set up to respond to destruction, invoke it here.
        Destroy(gameObject);
    }
}