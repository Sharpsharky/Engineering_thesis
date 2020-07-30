using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BuildingInhancementInPanelButton : MonoBehaviour
{
    protected InhancementType inhancementType;
    protected int numberInSetOfChildren;
    protected ResourceCreatingBuilding buildingAssignedToThisInhancement;
    protected BuildingAttributes buildingAttributes;

    protected Transform grid;

    protected List<Price> priceForInhancement = new List<Price>();

    public List<Price> PriceForInhancement { get => priceForInhancement; set => priceForInhancement = value; }

    protected virtual void Awake() 
    {
        
    }

    protected virtual void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
        buildingAttributes = GameManager.Instance.FindBuildingAttributeByBuilidingType(buildingAssignedToThisInhancement.GetBuildingType());
        grid = transform.Find("Grid");
        UpdatePrices();
    }

    void OnClick() //Buy Inhancement
    {
        
        Debug.Log("1.1");
        tutaj nie tak
        if (SuppliesStorage.Instance.CheckIfItsPossibleToBuyThisInhancement(PriceForInhancement, FindLevelOfInhancementAccordingToInhancementType(numberInSetOfChildren))) //Is price ok?
        {
            Debug.Log("2.1");
            BuyInhancement();
            UpdateInhancementAttributes();
        }
    }

    protected virtual void BuyInhancement()
    {
            Debug.Log("Buy Inhancemttent");

    }

    public virtual void UpdateInhancementAttributes()
    {
        Debug.Log("UpdateInhancementAttributes");

    }

    protected virtual void UpdatePrices()
    {
        priceForInhancement = new List<Price>();

        for (int i = 0; i < buildingAttributes.buildingInhancements[numberInSetOfChildren].supplyPrices.Count; i++)
        {
            if (buildingAttributes.buildingInhancements[numberInSetOfChildren].supplyPrices[i].startingLevel <= FindLevelOfInhancementAccordingToInhancementType(i))
            {
                UpdateSpecificPrice(i);
            }
            else
            {
                if (grid.GetChild(i).gameObject.activeSelf == true) grid.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private int FindLevelOfInhancementAccordingToInhancementType(int numberOfInhancement)
    {
        switch (numberOfInhancement)
        {
            case 0:
                {
                    return buildingAssignedToThisInhancement.CurrentLevelOfSupplyQuantityOverClick;
                }
            case 1:
                {
                    return buildingAssignedToThisInhancement.CurrentLevelOfSupplyOverTime;
                }
            default:
                {
                    return buildingAssignedToThisInhancement.CurrentLevelOfStorageSpace;
                }
        }

    }


    public void SetBuildingAssignedToThisInhancement(ResourceCreatingBuilding newBuildingAssignedToThisInhancement)
    {
        buildingAssignedToThisInhancement = newBuildingAssignedToThisInhancement;
    }

    protected void UpdateSpecificPrice(int supplyNumber)
    {
        if (grid.GetChild(supplyNumber).gameObject.activeSelf == false) grid.GetChild(supplyNumber).gameObject.SetActive(true);

        priceForInhancement.Add(buildingAttributes.buildingInhancements[numberInSetOfChildren].supplyPrices[supplyNumber]);

        Quantity quantityToDisplay = new Quantity(buildingAttributes.buildingInhancements[numberInSetOfChildren].supplyPrices[supplyNumber].amount, buildingAttributes.buildingInhancements[numberInSetOfChildren].supplyPrices[supplyNumber].quantityType);
        quantityToDisplay.MultiplyCurrentValueByConstant(((1 + (buildingAttributes.buildingInhancements[numberInSetOfChildren].coefficientIncreasingPrice * buildingAssignedToThisInhancement.GetLevelOfInhancement(numberInSetOfChildren)))));

        grid.GetChild(supplyNumber).Find("Price").GetComponent<TMP_Text>().text = quantityToDisplay.GetAmountToDisplay().ToString() + GameManager.Instance.QuantityTypes[quantityToDisplay.GetQuantityToDisplay()];

    }

}

public enum InhancementType { SUPPLY_IN_TIME, SUPPLY_PER_CLICK, STORAGE_SPACE}
