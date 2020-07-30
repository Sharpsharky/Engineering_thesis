using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingInhancementButton : UI3DObject
{

    protected override void OnClick()
    {
        UIManager.Instance.TurnOnOffBuildingInhancementPanel();
        Debug.Log("tileTheButtonIsAssignedTo " + tileTheButtonIsAssignedTo);
        SetAttributesToBuildingInhancemntPanel(tileTheButtonIsAssignedTo);

        base.OnClick();

    }

    private void SetAttributesToBuildingInhancemntPanel(GameObject tile)
    {
        ResourceCreatingBuilding thisBuilding = tile.GetComponent<ResourceCreatingBuilding>();
        BuildingType buildingType = tile.GetComponent<ResourceCreatingBuilding>().GetBuildingType();
        Transform buildingPanel = UIManager.Instance.BuildingInhancementPanel.transform;

        Debug.Log(GameManager.Instance.FindBuildingAttributeByBuilidingType(buildingType).name);

        SetTitle(buildingPanel, buildingType); //TITLE

        SetInhancementsOfPanel(buildingPanel, buildingType, thisBuilding);//INHANCEMENTS



        //STORAGE BAR AND INSCRIPTIONS

        //SPEED

        //CLICK VALUE
    }


    private void SetTitle(Transform buildingPanel, BuildingType buildingType)
    {
        buildingPanel.transform.Find("Title").GetComponent<TMP_Text>().text = GameManager.Instance.FindBuildingAttributeByBuilidingType(buildingType).name;
    }

    private void SetInhancementsOfPanel(Transform buildingPanel, BuildingType buildingType, ResourceCreatingBuilding thisBuilding)
    {
        Transform inhancementsSet = buildingPanel.transform.Find("LeftPanel").GetChild(0).GetChild(0);

        BuildingAttributes buildingAttributes = GameManager.Instance.FindBuildingAttributeByBuilidingType(buildingType);

        for (int i = 0; i < buildingAttributes.buildingInhancements.Count; i++)
        {
            Transform currentInhancement = inhancementsSet.GetChild(i);
            currentInhancement.Find("Title").GetComponent<TMP_Text>().text = GameManager.Instance.NamesOfInhancementsInSupplyCollectingInhancements[i];
            currentInhancement.GetComponent<BuildingInhancementInPanelButton>().SetBuildingAssignedToThisInhancement(thisBuilding);


            

            //currentInhancement.GetComponent<BuildingInhancementInPanelButton>().UpdateInhancementAttributes();

            for (int j = 0; j < buildingAttributes.buildingInhancements[i].supplyPrices.Count; j++)
            {
                if (buildingAttributes.buildingInhancements[i].supplyPrices[j].startingLevel <= thisBuilding.CurrentLevelOfSupplyQuantityOverClick)
                {
                    Transform supply = inhancementsSet.GetChild(i).Find("Grid").GetChild(j);

                    Debug.Log("Supply " + supply);
                    supply.gameObject.SetActive(true);
                    SupplyAttributes attributes = GameManager.Instance.FindSupplyAttributeBySupplyType(buildingAttributes.buildingInhancements[i].supplyPrices[j].supplyType);
                    supply.Find("Image").GetComponent<Image>().sprite = attributes.icon;
                    /*
                    Quantity quantityToDisplay = new Quantity(buildingAttributes.buildingInhancements[i].supplyPrices[j].amount, buildingAttributes.buildingInhancements[i].supplyPrices[j].quantityType);
                    quantityToDisplay.MultiplyCurrentValueByConstant(((1 + (buildingAttributes.buildingInhancements[i].coefficientIncreasingPrice * thisBuilding.GetLevelOfInhancement(i)))));

                    supply.Find("Price").GetComponent<TMP_Text>().text = quantityToDisplay.GetAmountToDisplay().ToString() + GameManager.Instance.QuantityTypes[quantityToDisplay.GetQuantityToDisplay()];
                    */
                }
            }
        }
    }

}