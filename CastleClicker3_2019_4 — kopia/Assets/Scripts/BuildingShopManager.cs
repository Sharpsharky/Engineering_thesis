using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingShopManager : MonoBehaviour
{
    [SerializeField]
    private Transform parentOfShop;
    [SerializeField]
    private GameObject shopItem;
    [SerializeField]
    private GameObject supplyPriceItem;

    private Dictionary<BuildingType, GameObject> buildingUIGameObject = new Dictionary<BuildingType, GameObject>();

    static public BuildingShopManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Debug.LogError("Too many instances!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GenerateItemsInShopAtStart();
    }

    private void GenerateItemsInShopAtStart()
    {
        
        for(int i = 0; i < parentOfShop.childCount; i++)
        {
            GameObject uiObjectOfBuilding = parentOfShop.transform.GetChild(i).gameObject;
            buildingUIGameObject.Add(GameManager.Instance.Buildings[i].buildingType, uiObjectOfBuilding);
            SetAttributes(uiObjectOfBuilding, GameManager.Instance.Buildings[i]);
        }


        UIManager.Instance.OpenCloseBuildingpanel();
    }

    private void SetAttributes(GameObject newItem, BuildingAttributes building)
    {
        //newItem.transform.parent = parentOfShop;
        //newItem.transform.SetParent(parentOfShop);

        newItem.GetComponent<BuildingShopItem>().BuildingType = building.buildingType;

        newItem.transform.Find("Image").GetComponent<Image>().sprite = building.icon;
        newItem.transform.Find("Title").GetComponent<TMP_Text>().text = building.name;

        ChangeThePrice(building, newItem);
    }

    public void ChangeThePrice(BuildingAttributes building, GameObject newItem = null)
    {
        if (buildingUIGameObject.ContainsKey(building.buildingType))
        {
            newItem = buildingUIGameObject[building.buildingType];
        }

        foreach(Transform child in newItem.transform.Find("Grid"))
        {
            child.gameObject.SetActive(false);
        }


        newItem.GetComponent<BuildingShopItem>().ChangePriceForBuilding(building.initialBuildingPrice.supplyPrices, building.buildingInhancements[0].coefficientIncreasingPrice);

        int counterOfPrices = 0;

        foreach(Price price in building.initialBuildingPrice.supplyPrices)
        {

            if (price.startingLevel <= GameManager.Instance.GetAmountOfBuildingsOfType(building.buildingType))
            {
                GameObject supplyUI = newItem.transform.Find("Grid").GetChild(counterOfPrices).gameObject;
                supplyUI.SetActive(true);


                supplyUI.transform.Find("Image").GetComponent<Image>().sprite = GameManager.Instance.FindSupplyAttributeBySupplyType(price.supplyType).icon;
                Quantity quantityToDisplay = new Quantity(price.amount, price.quantityType);

                quantityToDisplay.MultiplyCurrentValueByConstant(((GameManager.Instance.GetAmountOfBuildingsOfType(building.buildingType) * building.buildingInhancements[0].coefficientIncreasingPrice) + 1));
                supplyUI.transform.Find("Price").GetComponent<TMP_Text>().text = quantityToDisplay.GetAmountToDisplay().ToString() + GameManager.Instance.QuantityTypes[quantityToDisplay.GetQuantityToDisplay()];
                counterOfPrices++;
            }
        }

    }

    public void ChangePriceOfBuildingByBildingType(BuildingType buildingType)
    {

    }


    private void ChangePrice()
    {

    }


}


