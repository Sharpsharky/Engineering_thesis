using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    private Vector3 currentlyPickedTile;
    private GameObject currentlyPickedBuildingButton;
    private GameObject currentlyPickedTileGameObject;
    public static GameManager Instance;

    [SerializeField]
    private List<BuildingAttributes> buildings = new List<BuildingAttributes>();
    [SerializeField]
    private List<SupplyAttributes> supplies = new List<SupplyAttributes>();
    [SerializeField]
    private List<string> quantityTypes = new List<string>();
    [SerializeField]
    private CameraMovement cameraMovement;

    [Header("Tiles Movement")]
    [SerializeField]
    private float speedOfTileClicking = 1;
    [SerializeField]
    private float speedOfTileGoingUp = 1;
    [SerializeField]
    private float maxTilePositionYWhenClicked = -0.3f;

    [SerializeField]
    private GameObject buildingInhancementButton;
    [SerializeField]
    private Transform buildingInhancementButtonParent;
    [SerializeField] [Range(0,10)]
    private float heightOfBuildingInhancementButton = 2;
    [SerializeField]
    private Vector2 offsetOfBuildingInhancementButton = new Vector2(0,0);

    [SerializeField]
    private List<string> namesOfInhancementsInSupplyCollectingInhancements = new List<string>();

    private bool areTilesClickable = true;

    private Dictionary<BuildingType, int> numberOfBuildingsWeHave = new Dictionary<BuildingType, int>();

    #region Heremetization
    public Camera MainCamera { get => mainCamera; set => mainCamera = value; }
    public Vector3 CurrentlyPickedTile { get => currentlyPickedTile; set => currentlyPickedTile = value; }
    public List<BuildingAttributes> Buildings { get => buildings; set => buildings = value; }
    public GameObject CurrentlyPickedBuildingButton { get => currentlyPickedBuildingButton; set => currentlyPickedBuildingButton = value; }
    public List<string> QuantityTypes { get => quantityTypes; set => quantityTypes = value; }
    public bool AreTilesClickable { get => areTilesClickable; set => areTilesClickable = value; }
    public float SpeedOfTileClicking { get => speedOfTileClicking; set => speedOfTileClicking = value; }
    public float MaxTilePositionYWhenClicked { get => maxTilePositionYWhenClicked; set => maxTilePositionYWhenClicked = value; }
    public float SpeedOfTileGoingUp { get => speedOfTileGoingUp; set => speedOfTileGoingUp = value; }
    public GameObject BuildingInhancementButton { get => buildingInhancementButton; set => buildingInhancementButton = value; }
    public Transform BuildingInhancementButtonParent { get => buildingInhancementButtonParent; set => buildingInhancementButtonParent = value; }
    public float HeightOfBuildingInhancementButton { get => heightOfBuildingInhancementButton; set => heightOfBuildingInhancementButton = value; }
    public Vector2 OffsetOfBuildingInhancementButton { get => offsetOfBuildingInhancementButton; set => offsetOfBuildingInhancementButton = value; }
    public GameObject CurrentlyPickedTileGameObject { get => currentlyPickedTileGameObject; set => currentlyPickedTileGameObject = value; }
    public List<string> NamesOfInhancementsInSupplyCollectingInhancements { get => namesOfInhancementsInSupplyCollectingInhancements; set => namesOfInhancementsInSupplyCollectingInhancements = value; }
    #endregion

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Debug.LogError("Too many Instances!");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        /*
        if(numberOfBuildingsWeHave.ContainsKey(BuildingType.LAMBERJACKS_HUT))
        Debug.Log(numberOfBuildingsWeHave[BuildingType.LAMBERJACKS_HUT]);

        if (numberOfBuildingsWeHave.ContainsKey(BuildingType.BARRACKS))
            Debug.Log(numberOfBuildingsWeHave[BuildingType.BARRACKS]);

        if (numberOfBuildingsWeHave.ContainsKey(BuildingType.FARM))
            Debug.Log(numberOfBuildingsWeHave[BuildingType.FARM]);
            */

    }

    public GameObject FindBuildingByBuilidingType(BuildingType buildingType)
    {
        foreach (BuildingAttributes buildingAttribute in buildings)
        {
            if (buildingAttribute.buildingType == buildingType)
            {
                return buildingAttribute.tile;
            }
        }
        return null;
    }

    public BuildingAttributes FindBuildingAttributeByBuilidingType(BuildingType buildingType)
    {
        foreach(BuildingAttributes buildingAttribute in buildings)
        {
            if(buildingAttribute.buildingType == buildingType)
            {
                return buildingAttribute;
            }
        }
        return null;
    }

    public SupplyAttributes FindSupplyAttributeBySupplyType(SupplyType givenSupplyType)
    {
        foreach(SupplyAttributes supplyAttribute in supplies)
        {
            if(supplyAttribute.supplyType == givenSupplyType)
            {
                return supplyAttribute;
            }
        }
        return null;
    }

    public int GetAmountOfBuildingsOfType(BuildingType buildingType)
    {
        if (!numberOfBuildingsWeHave.ContainsKey(buildingType))
        {
            return 0;
        }
        else
        {
            return numberOfBuildingsWeHave[buildingType];
        }
    }

    public void AddBuildingsOfType(BuildingType buildingType)
    {
        if (!numberOfBuildingsWeHave.ContainsKey(buildingType))
        {
            numberOfBuildingsWeHave.Add(buildingType, 1);
        }
        else
        {
            numberOfBuildingsWeHave[buildingType]++;
        }
    }

    public void TurnOnOff3DSpaceInteraction(bool statement)
    {
        cameraMovement.enabled = statement;
        areTilesClickable = statement;
    }


}
