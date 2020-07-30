using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class BuildingAttributes : ScriptableObject
{
    public new string name;
    public GameObject tile;
    public Sprite icon;
    public BuildingType buildingType;
    public Vector2 initialQuantityOfSupplOverTime;
    public float constantOfQuantityOfSupplyOverTimeOverLevel;
    public Vector2 initialQuantityOfMagazine;
    public float constantOfMagazineSpaceChangeOverLevel;
    public Vector2 initialQuantityOfSupplyPerClick;
    public float constantOfSupplyPerClickChangeOverLevel;
    public List<SupplyAttributes> suppliesCreatingByThisBuilding = new List<SupplyAttributes>();
    public List<BuildingInhancements> buildingInhancements = new List<BuildingInhancements>();
    public BuildingInhancements initialBuildingPrice;


}
