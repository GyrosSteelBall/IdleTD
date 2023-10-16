using UnityEngine;
using System;

public class DefenseComponent : MonoBehaviour
{
    public event Action<int, int> OnArmorChanged;
    public event Action<int, int> OnMagicResistanceChanged;
    public event Action<float, float> OnDamageReductionChanged;

    [SerializeField]
    private int armor;
    [SerializeField]
    private int magicResistance;
    [SerializeField]
    private float damageReduction;  // Expressed as a percentage

    public int CalculatePhysicalDamageTaken(int incomingDamage)
    {
        float damageMultiplier = 100f / (100f + armor);
        float damageAfterArmor = incomingDamage * damageMultiplier;
        float damageAfterFlatReduction = damageAfterArmor * (1f - damageReduction / 100f);
        return (int)damageAfterFlatReduction;
    }

    public int CalculateMagicDamageTaken(int incomingDamage)
    {
        float damageMultiplier = 100f / (100f + magicResistance);
        float damageAfterMR = incomingDamage * damageMultiplier;
        float damageAfterFlatReduction = damageAfterMR * (1f - damageReduction / 100f);
        return (int)damageAfterFlatReduction;
    }

    public void SetArmor(int newArmor)
    {
        int oldArmor = armor;
        armor = Mathf.Max(0, newArmor);  // Clamp to 0 to prevent negative values
        OnArmorChanged?.Invoke(oldArmor, armor);
    }

    public void SetMagicResistance(int newMagicResistance)
    {
        int oldMagicResistance = magicResistance;
        magicResistance = Mathf.Max(0, newMagicResistance);  // Clamp to 0 to prevent negative values
        OnMagicResistanceChanged?.Invoke(oldMagicResistance, magicResistance);
    }

    public void SetDamageReduction(float newDamageReduction)
    {
        float oldDamageReduction = damageReduction;
        damageReduction = Mathf.Clamp(newDamageReduction, 0f, 100f);  // Clamp to [0, 100] to ensure a valid percentage
        OnDamageReductionChanged?.Invoke(oldDamageReduction, damageReduction);
    }

    public int GetArmor()
    {
        return armor;
    }

    public int GetMagicResistance()
    {
        return magicResistance;
    }

    public float GetDamageReduction()
    {
        return damageReduction;
    }
}
