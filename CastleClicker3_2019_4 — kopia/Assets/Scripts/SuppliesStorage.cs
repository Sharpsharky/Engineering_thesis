using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SuppliesStorage : MonoBehaviour
{

    private Dictionary<SupplyType, Quantity> suppliesAndTheirAmounts = new Dictionary<SupplyType, Quantity>();
    [SerializeField]
    private Transform parentOfSupplyUIs;


    public static SuppliesStorage Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Debug.LogError("Too many Instances!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AddSuppliesAtStart();
        ReloadAllSupplies();
        PutImagesToCorrectUIHolders();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            suppliesAndTheirAmounts[SupplyType.WOOD].AddValue(new Quantity(100, 0));
            ReloadSupply(SupplyType.WOOD);


        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            suppliesAndTheirAmounts[SupplyType.WOOD].AddValue(new Quantity(500, 0));
            ReloadSupply(SupplyType.WOOD);


        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            suppliesAndTheirAmounts[SupplyType.WOOD].AddValue(new Quantity(1, 1));
            ReloadSupply(SupplyType.WOOD);


        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            //suppliesAndTheirAmounts[SupplyType.WOOD].SubstractValue(100, 0);
            suppliesAndTheirAmounts[SupplyType.WOOD].SubstructThisValueByGivenQuantity(new Quantity(100, 0));
            ReloadSupply(SupplyType.WOOD);


        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            //suppliesAndTheirAmounts[SupplyType.WOOD].SubstractValue(500, 0);
            suppliesAndTheirAmounts[SupplyType.WOOD].SubstructThisValueByGivenQuantity(new Quantity(500, 0));
            ReloadSupply(SupplyType.WOOD);


        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            //suppliesAndTheirAmounts[SupplyType.WOOD].SubstractValue(1, 1);
            suppliesAndTheirAmounts[SupplyType.WOOD].SubstructThisValueByGivenQuantity(new Quantity(1, 1));
            ReloadSupply(SupplyType.WOOD);


        }


    }
    private void AddSuppliesAtStart()
    {
        suppliesAndTheirAmounts.Add(SupplyType.STONE, new Quantity(0, 0));
        suppliesAndTheirAmounts.Add(SupplyType.WOOD, new Quantity(0, 0));
        suppliesAndTheirAmounts.Add(SupplyType.FOOD, new Quantity(0, 0));
        suppliesAndTheirAmounts.Add(SupplyType.GOLD, new Quantity(0, 0));
        suppliesAndTheirAmounts.Add(SupplyType.MARBLE, new Quantity(0, 0));
        suppliesAndTheirAmounts.Add(SupplyType.SULFUR, new Quantity(0, 0));

    }

    private void PutImagesToCorrectUIHolders()
    {
        ChangeIconToSupply(SupplyType.STONE);
        ChangeIconToSupply(SupplyType.WOOD);
        ChangeIconToSupply(SupplyType.FOOD);
        ChangeIconToSupply(SupplyType.GOLD);
        ChangeIconToSupply(SupplyType.MARBLE);
        ChangeIconToSupply(SupplyType.SULFUR);
    }

    private void ChangeIconToSupply(SupplyType supplyType)
    {
        parentOfSupplyUIs.GetChild(GameManager.Instance.FindSupplyAttributeBySupplyType(supplyType).numberInUI).Find("Image").GetComponent<Image>().sprite = GameManager.Instance.FindSupplyAttributeBySupplyType(supplyType).icon;

    }

    private void ReloadAllSupplies()
    {
        ReloadSupply(SupplyType.STONE);
        ReloadSupply(SupplyType.WOOD);
        ReloadSupply(SupplyType.FOOD);
        ReloadSupply(SupplyType.GOLD);
        ReloadSupply(SupplyType.MARBLE);
        ReloadSupply(SupplyType.SULFUR);
    }

    private void ReloadSupply(SupplyType supplyType)
    {

        float amountToDisplay = suppliesAndTheirAmounts[supplyType].GetAmountToDisplay();
        string quantityToDisplay = GameManager.Instance.QuantityTypes[suppliesAndTheirAmounts[supplyType].GetQuantityToDisplay()];

        parentOfSupplyUIs.GetChild(GameManager.Instance.FindSupplyAttributeBySupplyType(supplyType).numberInUI).Find("Text (TMP)").GetComponent<TMP_Text>().text = amountToDisplay.ToString() + quantityToDisplay;
    }

    public void AddSupplyAmount(SupplyType supplyType, Quantity addedAmount)
    {
        suppliesAndTheirAmounts[supplyType].AddValue(addedAmount);
        ReloadSupply(supplyType);
    }


    public bool CheckIfItsPossibleToBuyThisBuilding(List<Price> priceForItem, BuildingType buildingType)
    {


        foreach (Price price in priceForItem)
        {//0<0
            if (price.startingLevel <= GameManager.Instance.GetAmountOfBuildingsOfType(buildingType))
            {

                if (CheckIfCurrentQuantityIsEnough(price) == false)
                {
                    return false;
                }
            }
        }
        //Debug.Log(price.quantityType + ":" + suppliesAndTheirAmounts[price.supplyType].CurrentValue[price.quantityType] + "> Q " + price.quantityType + ":" + price.amount);
        Debug.Log("--------------true???");
        //ReloadAllSupplies();
        return true;
    }

    public bool CheckIfItsPossibleToBuyThisInhancement(List<Price> priceForItem, int currentLevelOfInhacement)
    {
        foreach (Price price in priceForItem)
        {
            if (CheckIfCurrentQuantityIsEnough(price, currentLevelOfInhacement) == false)
            {
                return false;
            }
        }
        return true;
    }


    public bool CheckIfItsPossibleToBuyThisItem(List<Price> priceForItem)
    {
        foreach (Price price in priceForItem)
        {
            if (CheckIfCurrentQuantityIsEnough(price) == false)
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckIfCurrentQuantityIsEnough(Price price)
    {
        Quantity priceQuantity = new Quantity(price.amount, price.quantityType);
        if (!suppliesAndTheirAmounts[price.supplyType].CheckIfThisIsBiggerOrEqualThan(priceQuantity))//0>price
        {
            Debug.Log(price.supplyType);
            return true;
        }
        else
        {
            Debug.Log(price.supplyType);
            return false;
        }
    }

    private bool CheckIfCurrentQuantityIsEnough(Price price, int currentLevelOfProduct)
    {
        Quantity priceQuantity = new Quantity(price.amount, price.quantityType);
        priceQuantity.MultiplyCurrentValueByConstant(currentLevelOfProduct);
        if (!suppliesAndTheirAmounts[price.supplyType].CheckIfThisIsBiggerOrEqualThan(priceQuantity))//0>price
        {
            Debug.Log(price.supplyType);
            return true;
        }
        else
        {
            Debug.Log(price.supplyType);
            return false;
        }
    }


    public void BuyBuilding(List<Price> priceForItem, BuildingType buildingType)
    {
        foreach (Price price in priceForItem)
        {
            if(price.startingLevel <= GameManager.Instance.GetAmountOfBuildingsOfType(buildingType))//0>price
            {
                //suppliesAndTheirAmounts[price.supplyType].SubstructQuantity(new Quantity(price.amount, price.quantityType));
            //suppliesAndTheirAmounts[SupplyType.WOOD].SubstructQuantity(new Quantity(100, 0));
                suppliesAndTheirAmounts[price.supplyType].SubstructThisValueByGivenQuantity(new Quantity(price.amount, price.quantityType));
                ReloadSupply(price.supplyType);
            }
        }
    }


}

