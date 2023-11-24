using System.Collections.Generic;

public static class ElementalEffectivenessLookup
{
    private static readonly Dictionary<(ElementType, ElementType), float> effectivenessTable = new Dictionary<(ElementType, ElementType), float>
    {
        // Fire is strong against Earth but weak against Water
        { (ElementType.Fire, ElementType.Earth), 1.5f },
        { (ElementType.Fire, ElementType.Water), 0.5f },

        // Water is strong against Fire but weak against Earth
        { (ElementType.Water, ElementType.Fire), 1.5f },
        { (ElementType.Water, ElementType.Earth), 0.5f },

        // Earth is strong against Lightning but weak against Fire
        { (ElementType.Earth, ElementType.Lightning), 1.5f },
        { (ElementType.Earth, ElementType.Fire), 0.5f },

        // Lightning is strong against Water but weak against Earth
        { (ElementType.Lightning, ElementType.Water), 1.5f },
        { (ElementType.Lightning, ElementType.Earth), 0.5f },

        // Light and Dark are typically strong against each other in gacha games
        { (ElementType.Light, ElementType.Dark), 1.5f },
        { (ElementType.Dark, ElementType.Light), 1.5f },

        // Add additional matchups as needed...
    };

    // Provide a method to access the effectiveness values
    public static float GetEffectiveness(ElementType attacker, ElementType defender)
    {
        // If an effectiveness entry exists for the matchup, return it
        if (effectivenessTable.TryGetValue((attacker, defender), out float effectiveness))
        {
            return effectiveness;
        }

        // If there's no specific entry, assume normal effectiveness (1.0 = no bonus or penalty)
        return 1.0f;
    }
}