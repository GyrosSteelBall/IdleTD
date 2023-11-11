using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    protected Enemy target;
    public float damage { get; private set; }

    void Update()
    {
        Move();
    }

    public void Initialize(Enemy target, float damage)
    {
        this.target = target;
        SetDamage(damage); // Set the damage when initializing the projectile
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    void Move()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            if (transform.position == target.transform.position)
            {
                // Logic to handle reaching the target, like playing an impact effect
                HitTarget();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    virtual public void HitTarget()
    {
        target.TakeDamage(damage);
        Destroy(gameObject);
    }
}
