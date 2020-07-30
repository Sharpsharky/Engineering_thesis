using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject buildingButtonsSet;
    [SerializeField]
    private GameObject buildingPanel;
    [SerializeField]
    private GameObject buildingInhancementPanel;


    static public UIManager Instance;

    public GameObject BuildingPanel { get => buildingPanel; set => buildingPanel = value; }
    public GameObject BuildingInhancementPanel { get => buildingInhancementPanel; set => buildingInhancementPanel = value; }

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Debug.LogError("Too many Instances!");
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        TurnOnOffBuildingInhancementPanel();
    }

    public void TurnOnOffBuildingInhancementPanel()
    {
        BuildingInhancementPanel.SetActive(!BuildingInhancementPanel.activeSelf);
        TurnOnOff3DButtons();
        GameManager.Instance.TurnOnOff3DSpaceInteraction(!BuildingPanel.activeSelf);

       

    }

    public void TurnOnOffBuildingButtons()
    {
        buildingButtonsSet.SetActive(!buildingButtonsSet.activeSelf);

        
    }

    public void OpenCloseBuildingpanel()
    {
        BuildingPanel.SetActive(!BuildingPanel.activeSelf);
        TurnOnOff3DButtons();
        GameManager.Instance.TurnOnOff3DSpaceInteraction(!BuildingPanel.activeSelf);
        if (BuildingPanel.activeSelf == false)
        {
            Debug.Log("1");
            if (GameManager.Instance.CurrentlyPickedTileGameObject != null)
            {
                GameManager.Instance.CurrentlyPickedTileGameObject.transform.Find("GameObject").Find("TileHighlight").gameObject.SetActive(false);
                Debug.Log("2");

            }
        }
    }

    private void TurnOnOff3DButtons()
    {
        Debug.Log("TurnOnOff3DButtons");

        buildingButtonsSet.SetActive(!GameManager.Instance.BuildingInhancementButtonParent.gameObject.activeSelf);
        GameManager.Instance.BuildingInhancementButtonParent.gameObject.SetActive(!GameManager.Instance.BuildingInhancementButtonParent.gameObject.activeSelf);
    }



}
