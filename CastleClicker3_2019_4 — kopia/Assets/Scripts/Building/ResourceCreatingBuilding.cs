using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCreatingBuilding : Building
{

    private float maxClickingPosition;
    private float speedOfTileClicking, maxTilePositionYWhenClicked;

    private bool isTileGoingDown = false, isTileGoingUp = false;

    private Quantity currentSupplyQuantityOverClick;
    private Quantity currentSupplyOverTime;
    private Quantity currentStorageSpace;

    private int currentLevelOfSupplyQuantityOverClick = 0;
    private int currentLevelOfSupplyOverTime = 0;
    private int currentLevelOfStorageSpace = 0;

    public Quantity CurrentSupplyQuantityOverClick { get => currentSupplyQuantityOverClick; set => currentSupplyQuantityOverClick = value; }
    public Quantity CurrentSupplyOverTime { get => currentSupplyOverTime; set => currentSupplyOverTime = value; }
    public Quantity CurrentStorageSpace { get => currentStorageSpace; set => currentStorageSpace = value; }
    public int CurrentLevelOfSupplyQuantityOverClick { get => currentLevelOfSupplyQuantityOverClick; set => currentLevelOfSupplyQuantityOverClick = value; }
    public int CurrentLevelOfSupplyOverTime { get => currentLevelOfSupplyOverTime; set => currentLevelOfSupplyOverTime = value; }
    public int CurrentLevelOfStorageSpace { get => currentLevelOfStorageSpace; set => currentLevelOfStorageSpace = value; }

    private void Start()
    {
        speedOfTileClicking = GameManager.Instance.SpeedOfTileClicking;
        maxTilePositionYWhenClicked = GameManager.Instance.MaxTilePositionYWhenClicked;

        AssignAllStartingProperties();
    }

    public void OnClick()
    {
        DrawSupplyToGive();
        MoveTile();
        SuppliesStorage.Instance.AddSupplyAmount(DrawSupplyToGive(), new Quantity(100, 0)); //Trzeba znalezc jakis sposob na branie quantity w zaleznosci od poziomu
        Debug.Log("Clicked: " + buildingAtribute.name);
    }
    
    private SupplyType DrawSupplyToGive()
    {
        int indexOfSupplies = Random.Range(0, buildingAtribute.suppliesCreatingByThisBuilding.Count);
        SupplyType supplyToReturn = buildingAtribute.suppliesCreatingByThisBuilding[indexOfSupplies].supplyType;

        return supplyToReturn;
    }

    private void MoveTile()
    {
        isTileGoingDown = true;
    }

    private void Update()
    {
        if (isTileGoingDown)
        {
            Debug.Log("isTileGoingDown");
            isTileGoingUp = false;
            Vector3 positionOfTile = transform.position;
            positionOfTile.y -= GameManager.Instance.SpeedOfTileClicking * Time.deltaTime;
            transform.position = positionOfTile;
            if(positionOfTile.y <= GameManager.Instance.MaxTilePositionYWhenClicked)
            {
                isTileGoingDown = false;
                isTileGoingUp = true;
            }
        }
        else if (isTileGoingUp)
        {
            Debug.Log("isTileGoingUp");

            Vector3 positionOfTile = transform.position;
            positionOfTile.y += GameManager.Instance.SpeedOfTileGoingUp * Time.deltaTime;
            transform.position = positionOfTile;

            if (positionOfTile.y >= 0)
            {
                isTileGoingUp = false;
                positionOfTile.y = 0;
                transform.position = positionOfTile;
            }
        }
    }

    public void AssignAllStartingProperties()
    {
        Vector2 startingSupplyByClick = GameManager.Instance.FindBuildingAttributeByBuilidingType(buildingType).initialQuantityOfMagazine;
        Vector2 startingSupplyOverTime = GameManager.Instance.FindBuildingAttributeByBuilidingType(buildingType).initialQuantityOfSupplOverTime;
        Vector2 startingSupplyStorageSpace = GameManager.Instance.FindBuildingAttributeByBuilidingType(buildingType).initialQuantityOfMagazine;

        CurrentSupplyQuantityOverClick = new Quantity((int)startingSupplyByClick.x, (int)startingSupplyByClick.y);
        CurrentSupplyOverTime = new Quantity((int)startingSupplyOverTime.x, (int)startingSupplyOverTime.y);
        CurrentStorageSpace = new Quantity((int)startingSupplyStorageSpace.x, (int)startingSupplyStorageSpace.y);
    }

    public int GetLevelOfInhancement(int numberOfInhancement)
    {
        switch (numberOfInhancement)
        {
            case 0:
                {
                    return currentLevelOfSupplyQuantityOverClick;
                }
            case 1:
                {
                    return currentLevelOfSupplyOverTime;
                }
            case 2:
                {
                    return currentLevelOfStorageSpace;
                }
            default:
                {
                    return 0;
                }
}
    }


}
