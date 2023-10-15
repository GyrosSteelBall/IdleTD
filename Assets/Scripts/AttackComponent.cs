using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    private int attackDamage;
    private float attackSpeed;
    private int attackRange;
    private string attackType;
    private float criticalChance;

    public void SetAttackDamage(int damage)
    {
        attackDamage = damage;
    }

    public int GetAttackDamage()
    {
        return attackDamage;
    }

    public void SetAttackSpeed(float speed)
    {
        attackSpeed = speed;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public void SetAttackRange(int range)
    {
        attackRange = range;
    }

    public int GetAttackRange()
    {
        return attackRange;
    }

    public void SetAttackType(string type)
    {
        attackType = type;
    }

    public string GetAttackType()
    {
        return attackType;
    }

    public void SetCriticalChance(float chance)
    {
        criticalChance = chance;
    }

    public float GetCriticalChance()
    {
        return criticalChance;
    }

    public void ConfigureAttack(string type, int damage, float speed, int range, float critChance)
    {
        SetAttackType(type);
        SetAttackDamage(damage);
        SetAttackSpeed(speed);
        SetAttackRange(range);
        SetCriticalChance(critChance);
    }
}
