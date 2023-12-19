using UnityEngine;

public class UnitFactory : MonoBehaviour
{
    //constructor
    public UnitFactory()
    {
    }

    public GameObject CreateUnit(UnitDataSO unitData, Vector3 spawnPosition)
    {
        //instantiate the unit prefab
        GameObject unitGameObject = Instantiate(unitData.unitPrefab, spawnPosition, Quaternion.identity);
        Unit unit = new Unit(unitData);
        //attach above scripts to the unit
        unitGameObject.AddComponent<Unit>();
        UnitController unitController = unitGameObject.AddComponent<UnitController>();
        unitController.Initialize(unit);
        UnitAnimator unitAnimator = unitGameObject.AddComponent<UnitAnimator>();
        unitAnimator.Initialize(unitController);
        //return the unit
        return unitGameObject;
    }
}