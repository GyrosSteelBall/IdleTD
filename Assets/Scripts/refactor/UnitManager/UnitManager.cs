using System.Collections.Generic;
using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{
    private UnitFactory UnitFactory = new UnitFactory();
    public List<UnitDataSO> UnitDataSOs = new List<UnitDataSO>();
    public List<GameObject> Units = new List<GameObject>();

    void OnEnable()
    {
        AddUnit(UnitDataSOs[0]);
    }

    public void AddUnit(UnitDataSO unitData)
    {
        //hard coded for now
        Units.Add(UnitFactory.CreateUnit(unitData, new Vector3(-4.63f, -3.05f, -1f)));
        //Second unit for testing
        Units.Add(UnitFactory.CreateUnit(unitData, new Vector3(-4.63f, -1.05f, -1f)));
    }

    public void RemoveUnit(GameObject unit)
    {
        Units.Remove(unit);
    }
}