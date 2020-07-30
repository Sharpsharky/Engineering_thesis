using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingButton : UI3DObject
{

    protected override void OnClick()
    {
        UIManager.Instance.OpenCloseBuildingpanel();
        tileTheButtonIsAssignedTo.transform.Find("GameObject").Find("TileHighlight").gameObject.SetActive(true);
        base.OnClick();
    }

}
