using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TerrainValidation
{
    public static bool CanPlaceUnit(Vector2 position, LayerMask validTerrainLayerMask)
    {
        Collider2D collider = Physics2D.OverlapPoint(position, validTerrainLayerMask);
        return collider != null; // Or any other logic for determining valid placement
    }
}
