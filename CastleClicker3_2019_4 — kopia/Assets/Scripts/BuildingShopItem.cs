using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingShopItem : MonoBehaviour
{

    public BuildingType BuildingType { get => buildingType; set => buildingType = value; }
    public List<Price> PriceForBuilding { get => priceForBuilding; set => priceForBuilding = value; }

    private List<Price> priceForBuilding = new List<Price>();

    private BuildingType buildingType;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick() //Buy Building
    {
        if (SuppliesStorage.Instance.CheckIfItsPossibleToBuyThisBuilding(PriceForBuilding, buildingType)) //Is price ok?
        {
            SuppliesStorage.Instance.BuyBuilding(PriceForBuilding, BuildingType);
            GameObject newCreatedBuilding = TileCreator.Instance.CreateEmptyTilesAroundNewBuiltBuilding(GameManager.Instance.CurrentlyPickedTile, GameManager.Instance.FindBuildingByBuilidingType(BuildingType));
            newCreatedBuilding.GetComponent<Building>().AssignBuildingAttribute(GameManager.Instance.FindBuildingAttributeByBuilidingType(buildingType));
            UIManager.Instance.OpenCloseBuildingpanel();
            GameManager.Instance.AddBuildingsOfType(BuildingType);
            BuildingShopManager.Instance.ChangeThePrice(GameManager.Instance.FindBuildingAttributeByBuilidingType(buildingType));
            Destroy(GameManager.Instance.CurrentlyPickedBuildingButton);

        }
    }

    public void ChangePriceForBuilding(List<Price> prices, int multiplyCoefficient)
    {
        PriceForBuilding = new List<Price>();
        for(int i = 0; i < prices.Count; i++)
        {
            Price newPrice = prices[i];
            newPrice.amount *= (GameManager.Instance.GetAmountOfBuildingsOfType(BuildingType) * multiplyCoefficient) + 1;
            PriceForBuilding.Add(newPrice);

        }
    }




}
