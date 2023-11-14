using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacementManager : MonoBehaviour
{
    private Unit selectedUnitPrefab;
    private Unit currentUnitInstance; // Stores the currently placed unit
    private Unit tempUnitInstance; // Temporary unit instance for placement
    private bool isPlacingUnit = false;

    public LayerMask validTerrainLayerMask;
    public Camera mainCamera;
    public Color validPlacementColor = new Color(1, 1, 1, 0.5f); // Transparent white
    public Color invalidPlacementColor = new Color(1, 0, 0, 0.5f); // Transparent red


    void Update()
    {
        if (isPlacingUnit)
        {
            UpdatePlacementPosition();

            if (Input.GetMouseButtonDown(0) && TerrainValidation.CanPlaceUnit(tempUnitInstance.transform.position, validTerrainLayerMask))
            {
                PlaceUnit();
            }

            // Right-click to cancel placement
            if (Input.GetMouseButtonDown(1)) // 1 is the button index for right-click
            {
                CancelPlacement();
            }
        }
    }

    public void StartPlacingUnit(Unit unitPrefab)
    {
        // If the player chooses a new unit to place without placing the old one,
        // destroy the old temporary instance.
        if (tempUnitInstance != null)
        {
            Destroy(tempUnitInstance.gameObject);
        }

        // Assign the newly chosen unit prefab to selectedUnitPrefab and create a new temporary instance.
        selectedUnitPrefab = unitPrefab;
        tempUnitInstance = Instantiate(selectedUnitPrefab);

        isPlacingUnit = true;

        // Disable attacking and enable the range indicator with the initial color
        tempUnitInstance.EnableAttacking(false);
        tempUnitInstance.ShowRange(true);
        tempUnitInstance.rangeIndicator.GetComponent<SpriteRenderer>().color = invalidPlacementColor;
    }

    private void UpdatePlacementPosition()
    {
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        tempUnitInstance.transform.position = new Vector3(worldPosition.x, worldPosition.y, tempUnitInstance.transform.position.z);

        // Check if the placement is valid and update the indicator color accordingly
        bool canPlaceUnit = TerrainValidation.CanPlaceUnit(tempUnitInstance.transform.position, validTerrainLayerMask);
        tempUnitInstance.ShowRange(true);
        tempUnitInstance.rangeIndicator.GetComponent<SpriteRenderer>().color = canPlaceUnit ? validPlacementColor : invalidPlacementColor;
    }

    private void PlaceUnit()
    {
        if (!TerrainValidation.CanPlaceUnit(tempUnitInstance.transform.position, validTerrainLayerMask))
            return; // Early exit if the place is not valid, do not proceed to place the unit

        // If the placement is valid, finalize the unit as no longer temporary.
        currentUnitInstance = tempUnitInstance;
        currentUnitInstance.EnableAttacking(true); // Now the unit can attack
        currentUnitInstance.ShowRange(false); // Hide the range after placing, unless you want it always visible

        // Pay for the unit if there is a cost associated
        // GameManager.instance.SpendGold(currentUnitInstance.cost);
        // You would create a SpendGold method in your GameManager to handle economy.

        tempUnitInstance = null; // Clear the previous temporary instance reference
        isPlacingUnit = false;
    }

    // Call this to cancel placement and destroy temporary unit instance
    public void CancelPlacement()
    {
        if (tempUnitInstance != null)
        {
            Destroy(tempUnitInstance.gameObject);
        }
        tempUnitInstance = null;
        isPlacingUnit = false;
        UIManager.instance.DeselectUnit();
    }

}
