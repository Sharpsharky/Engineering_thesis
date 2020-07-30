using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BuildingType{ PALACE, FARM, QUARRY, LAMBERJACKS_HUT, BARRACKS, GOLD_MINE, SULFUR_MINE, MARBLE_MINE}

public class Building : MonoBehaviour, IBuildable
{
    [SerializeField]
    protected BuildingAttributes buildingAtribute;

    protected string nameOfBuilding;
    protected Sprite image;
    protected BuildingType buildingType;


    public void AssignBuildingAttribute(BuildingAttributes newBuildingAtribute)
    {
        buildingAtribute = newBuildingAtribute;
        AssignAttributesToBuilding();
    }

    private void AssignAttributesToBuilding()
    {
        nameOfBuilding = buildingAtribute.name;
        image = buildingAtribute.icon;
        buildingType = buildingAtribute.buildingType;
    }

    public BuildingType GetBuildingType() { return buildingType; }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LevelUp()
    {      
    }

}
