using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickingSpeed : BuildingInhancementInPanelButton
{
    protected override void Awake()
    {
        base.Awake();
        inhancementType = InhancementType.SUPPLY_PER_CLICK;
        numberInSetOfChildren = 0;
    }


    protected override void Start()
    {
        base.Start();
    }
    protected override void BuyInhancement()
    {
        base.BuyInhancement();
        Debug.Log("ClickingSpeed");

        buildingAssignedToThisInhancement.CurrentLevelOfSupplyQuantityOverClick++;
        Debug.Log("1");
        buildingAssignedToThisInhancement.CurrentSupplyQuantityOverClick.MultiplyCurrentValueByConstant((int)GameManager.Instance.FindBuildingAttributeByBuilidingType(buildingAssignedToThisInhancement.GetBuildingType()).constantOfSupplyPerClickChangeOverLevel);
        Debug.Log("2");

    }

    public override void UpdateInhancementAttributes()
    {
        base.UpdateInhancementAttributes();
        Debug.Log(buildingAssignedToThisInhancement.CurrentLevelOfSupplyQuantityOverClick.ToString());
        transform.Find("Level BackGround").GetChild(0).GetComponent<TMP_Text>().text = buildingAssignedToThisInhancement.CurrentLevelOfSupplyQuantityOverClick.ToString();
        UpdatePrices();
    }


}
