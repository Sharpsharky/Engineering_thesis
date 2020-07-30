using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutomaticSpeed : BuildingInhancementInPanelButton
{

    [SerializeField]
    private TMP_Text speedTitle;


    protected override void Awake()
    {
        base.Awake();
        inhancementType = InhancementType.SUPPLY_IN_TIME;
        numberInSetOfChildren = 1;
    }

    protected override void Start()
    {
        base.Start();


    }



    protected override void BuyInhancement()
    {
        base.BuyInhancement();
        Debug.Log("AutomaticSpeed");

        buildingAssignedToThisInhancement.CurrentLevelOfSupplyOverTime++;
        buildingAssignedToThisInhancement.CurrentSupplyOverTime.MultiplyCurrentValueByConstant((int)GameManager.Instance.FindBuildingAttributeByBuilidingType(buildingAssignedToThisInhancement.GetBuildingType()).constantOfQuantityOfSupplyOverTimeOverLevel);
    }

    public override void UpdateInhancementAttributes()
    {
        base.UpdateInhancementAttributes();
        transform.Find("Level BackGround").GetChild(0).GetComponent<TMP_Text>().text = buildingAssignedToThisInhancement.CurrentLevelOfSupplyOverTime.ToString();
        //speedTitle.text = "Speed: " + 
        UpdatePrices();
    }


}
