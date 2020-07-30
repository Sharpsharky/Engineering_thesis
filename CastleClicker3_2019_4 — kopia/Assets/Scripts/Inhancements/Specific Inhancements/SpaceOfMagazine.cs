using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpaceOfMagazine : BuildingInhancementInPanelButton
{
    protected override void Awake()
    {
        base.Awake();
        inhancementType = InhancementType.STORAGE_SPACE;
        numberInSetOfChildren = 2;
    }

    protected override void Start()
    {
        base.Start();

    }

    protected override void BuyInhancement()
    {
        base.BuyInhancement();
        Debug.Log("SpaceOfMagazine");

        buildingAssignedToThisInhancement.CurrentLevelOfStorageSpace++;
        buildingAssignedToThisInhancement.CurrentStorageSpace.MultiplyCurrentValueByConstant((int)GameManager.Instance.FindBuildingAttributeByBuilidingType(buildingAssignedToThisInhancement.GetBuildingType()).constantOfMagazineSpaceChangeOverLevel);

    }

    public override void UpdateInhancementAttributes()
    {
        base.UpdateInhancementAttributes();
        transform.Find("Level BackGround").GetChild(0).GetComponent<TMP_Text>().text = buildingAssignedToThisInhancement.CurrentLevelOfStorageSpace.ToString();
        UpdatePrices();
    }

}
