using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCreator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> emptyTiles = new List<GameObject>();
    [SerializeField]
    private GameObject palace;
    public GameObject testTile;
    [SerializeField]
    private GameObject roadTile;
    [SerializeField]
    private float widthOfTile, heightOfBuildingButton;
    [SerializeField]
    private GameObject buildingButton;
    [SerializeField]
    private Transform parentOfBuildingButtons;

    [SerializeField]
    [Header("Roads")]
    private List<GameObject> straightRoadTiles = new List<GameObject>();
    [SerializeField]
    private List<GameObject> turnRoadTiles = new List<GameObject>();
    [SerializeField]
    private List<GameObject> threeWaysRoadTiles = new List<GameObject>();
    [SerializeField]
    private List<GameObject> crossingRoadTiles = new List<GameObject>();



    public Dictionary<Vector3,GameObject> listOfRoadTiles = new Dictionary<Vector3, GameObject>();
    public Dictionary<Vector3,GameObject> listOfEmptyTiles = new Dictionary<Vector3, GameObject>();
    public Dictionary<Vector3,GameObject> listOfBuildingsTiles = new Dictionary<Vector3, GameObject>();

    public static TileCreator Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Debug.LogError("Too many Instances!");
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        CreateEmptyTilesAroundNewBuiltBuilding(new Vector3(0,0,0), palace); //Castle
    }

    public GameObject CreateEmptyTilesAroundNewBuiltBuilding(Vector3 positionOfNewBuilding, GameObject building)
    {
        CheckIfTheresObjectOnBuildingSpaceAndDestroyIt(positionOfNewBuilding);

        GameObject newBuilding = Instantiate(building, positionOfNewBuilding, Quaternion.identity);


        listOfBuildingsTiles.Add(positionOfNewBuilding, building);
        CreateEmptyTileForRoad(new Vector3(positionOfNewBuilding.x + widthOfTile, 0, positionOfNewBuilding.z));
        CreateEmptyTileForRoad(new Vector3(positionOfNewBuilding.x - widthOfTile, 0, positionOfNewBuilding.z));
        CreateEmptyTileForRoad(new Vector3(positionOfNewBuilding.x, 0, positionOfNewBuilding.z + widthOfTile));
        CreateEmptyTileForRoad(new Vector3(positionOfNewBuilding.x, 0, positionOfNewBuilding.z - widthOfTile));

        GameObject newInhancementButton = AttachInhancementButtonToNewBuilding(newBuilding);
        newInhancementButton.GetComponent<UI3DObject>().TileTheButtonIsAssignedTo = newBuilding;

        return newBuilding;
    }


    private void CheckIfTheresObjectOnBuildingSpaceAndDestroyIt(Vector3 positionOfNewBuilding)
    {
        if (listOfEmptyTiles.ContainsKey(positionOfNewBuilding))
        {
            GameObject tileToDestroy = listOfEmptyTiles[positionOfNewBuilding];
            listOfEmptyTiles.Remove(positionOfNewBuilding);
            tileToDestroy.SetActive(false);
           // Destroy(tileToDestroy);
        }
    }


    private void CreateEmptyTileForRoad(Vector3 emptyTilePosition)
    {
        emptyTilePosition = RoundVector(emptyTilePosition);

        CheckIfThereAreRoadsToBuildAndBuildThem(emptyTilePosition);

        if (!listOfRoadTiles.ContainsKey(emptyTilePosition) && !listOfEmptyTiles.ContainsKey(emptyTilePosition))
        {

            GameObject newEmptyTile = Instantiate(emptyTiles[Random.Range(0, emptyTiles.Count)], emptyTilePosition, Quaternion.identity);
            listOfEmptyTiles.Add(emptyTilePosition, newEmptyTile);
        }
        
        CheckIfPossibleAndCreateBuildibleTile(new Vector3(emptyTilePosition.x + widthOfTile,0, emptyTilePosition.z));
        CheckIfPossibleAndCreateBuildibleTile(new Vector3(emptyTilePosition.x - widthOfTile,0, emptyTilePosition.z));
        CheckIfPossibleAndCreateBuildibleTile(new Vector3(emptyTilePosition.x,0, emptyTilePosition.z + widthOfTile));
        CheckIfPossibleAndCreateBuildibleTile(new Vector3(emptyTilePosition.x,0, emptyTilePosition.z - widthOfTile));

    }

    private void CheckIfPossibleAndCreateBuildibleTile(Vector3 emptyBuildableTilePosition) //Buildible empty tile
    {
        emptyBuildableTilePosition = RoundVector(emptyBuildableTilePosition);


       if(!listOfEmptyTiles.ContainsKey(emptyBuildableTilePosition) && !listOfBuildingsTiles.ContainsKey(emptyBuildableTilePosition))
        {
            GameObject newEmptyTile = CreateNewBuildableTile(emptyBuildableTilePosition);
            listOfEmptyTiles.Add(emptyBuildableTilePosition, newEmptyTile);

        }
    }

    GameObject CreateNewBuildableTile(Vector3 emptyBuildableTile)
    {
        GameObject newEmptyTile = Instantiate(emptyTiles[Random.Range(0, emptyTiles.Count)], emptyBuildableTile, Quaternion.identity);

        buildingButton = Instantiate(buildingButton); //Adding UI button
        buildingButton.transform.SetParent(parentOfBuildingButtons);
        buildingButton.GetComponent<BuildingButton>().AttachButtonToTile(emptyBuildableTile, heightOfBuildingButton, new Vector2(0,0));
        buildingButton.GetComponent<UI3DObject>().TileTheButtonIsAssignedTo = newEmptyTile;

        return newEmptyTile;
    }

    private void CheckIfThereAreRoadsToBuildAndBuildThem(Vector3 positionOfEmptyTile)
    {
        bool left = false, right = false, up = false, down = false; //sitesOfRoads

        int numberOfNeigbourBuildings = 0;    

        if (listOfBuildingsTiles.ContainsKey(RoundVector(new Vector3(positionOfEmptyTile.x + widthOfTile, 0, positionOfEmptyTile.z)))) { numberOfNeigbourBuildings++; down = true; /*Debug.Log(new Vector3(positionOfEmptyTile.x + widthOfTile, 0, positionOfEmptyTile.z));*/ }
        if (listOfBuildingsTiles.ContainsKey(RoundVector(new Vector3(positionOfEmptyTile.x, 0, positionOfEmptyTile.z + widthOfTile)))) { numberOfNeigbourBuildings++; right = true; /* Debug.Log(new Vector3(positionOfEmptyTile.x, 0, positionOfEmptyTile.z + widthOfTile));*/ }
        if (listOfBuildingsTiles.ContainsKey(RoundVector(new Vector3(positionOfEmptyTile.x - widthOfTile, 0, positionOfEmptyTile.z)))) { numberOfNeigbourBuildings++; up = true; /*Debug.Log(new Vector3(positionOfEmptyTile.x - widthOfTile, 0, positionOfEmptyTile.z));*/ }
        if (listOfBuildingsTiles.ContainsKey(RoundVector(new Vector3(positionOfEmptyTile.x, 0, positionOfEmptyTile.z - widthOfTile)))) { numberOfNeigbourBuildings++; left = true; /*Debug.Log(new Vector3(positionOfEmptyTile.x, 0, positionOfEmptyTile.z - widthOfTile));*/ }

        if (numberOfNeigbourBuildings > 1)
        {
            RoadType newRoad = PickCorrectRoadTile(left,right,up,down);
            CreateRoad(positionOfEmptyTile, newRoad.road, newRoad.rotation);
        }

    }



    private void CreateRoad(Vector3 positionOfRoad, GameObject tile, float rotationOfTile)
    {
        if (listOfRoadTiles.ContainsKey(RoundVector(positionOfRoad))) 
        {//Destroy Road

            Destroy(listOfRoadTiles[RoundVector(positionOfRoad)]);
            //listOfEmptyTiles.Remove(positionOfRoad);
            listOfRoadTiles.Remove(positionOfRoad);
            Debug.Log("Destroy tile " + positionOfRoad);
        }
        else
        {//Destroy Empty Tile
            Destroy(listOfEmptyTiles[RoundVector(positionOfRoad)]);
            Debug.Log("Destroy tile ");
            listOfEmptyTiles.Remove(positionOfRoad);
        }
            Debug.Log("put" + tile.name);

        GameObject newRoad = Instantiate(tile, positionOfRoad, Quaternion.identity);
        Vector3 tempRotation = newRoad.transform.Find("GameObject").eulerAngles;
        tempRotation.y = rotationOfTile;
        newRoad.transform.Find("GameObject").eulerAngles = tempRotation;
        if(listOfRoadTiles.ContainsKey(positionOfRoad) == false) listOfRoadTiles.Add(positionOfRoad, newRoad);

    }


    private Vector3 RoundVector(Vector3 vectorToRound)
    {
        float x = Mathf.Round(vectorToRound.x * 100) / 100.0f;
        float y = Mathf.Round(vectorToRound.y * 100) / 100.0f;
        float z = Mathf.Round(vectorToRound.z * 100) / 100.0f;

        return new Vector3(x, y, z);

    }


    RoadType PickCorrectRoadTile(bool left, bool right, bool up, bool down)
    {
        RoadType newRoad;
        newRoad.road = null;
        newRoad.rotation = 0;
        RoadTypeName roadTypeName;

        if (left == true && right == true && up == false && down == false) // -- 
        {
            Debug.Log("--");
            roadTypeName = RoadTypeName.STRAIGHT;
            newRoad.rotation = 90;
        }
        else if(left == false && right == false && up == true && down == true) // |
        {
            Debug.Log("|");

            roadTypeName = RoadTypeName.STRAIGHT;
            newRoad.rotation = 0;          
        }
        else if (left == true && right == true && up == true && down == true) // +
        {
            roadTypeName = RoadTypeName.CROSSING;
            newRoad.rotation = 0;
        }
        else if (left == true && right == true && up == true && down == false) // _|_
        {
            roadTypeName = RoadTypeName.THREE_WAY;
            newRoad.rotation = -90;
        }
        else if (left == true && right == false && up == true && down == true) // -|
        {
            roadTypeName = RoadTypeName.THREE_WAY;
            newRoad.rotation = 180; 
        }
        else if (left == false && right == true && up == true && down == true) // |-
        {
            roadTypeName = RoadTypeName.THREE_WAY;
            newRoad.rotation = 0; 
        }
        else if (left == true && right == true && up == false && down == true) // -,-
        {
            roadTypeName = RoadTypeName.THREE_WAY;
            newRoad.rotation = 90; 
        }
        else if (left == false && right == true && up == false && down == true) // ,-
        {
            roadTypeName = RoadTypeName.TURN;
            newRoad.rotation = 0;
        }
        else if (left == true && right == false && up == false && down == true) // -,
        {
            roadTypeName = RoadTypeName.TURN;
            newRoad.rotation = 90;
        }
        else if (left == true && right == false && up == true && down == false) // -'
        {
            roadTypeName = RoadTypeName.TURN;
            newRoad.rotation = 180;
        }
        else  //if (left == false && right == true && up == true && down == false) '-
        {
            roadTypeName = RoadTypeName.TURN;
            newRoad.rotation = -90;
        }
        newRoad.road = GetRandomRoadTypeTile(roadTypeName);
        return newRoad;
    }

    GameObject GetRandomRoadTypeTile(RoadTypeName type)
    {

        if(type == RoadTypeName.CROSSING)
        {
            return crossingRoadTiles[Random.Range(0, crossingRoadTiles.Count)];
        }
        else if(type == RoadTypeName.TURN)
        {
            return turnRoadTiles[Random.Range(0, turnRoadTiles.Count)];

        }
        else if (type == RoadTypeName.STRAIGHT)
        {
            return straightRoadTiles[Random.Range(0, straightRoadTiles.Count)];

        }
        else 
        {
            return threeWaysRoadTiles[Random.Range(0, threeWaysRoadTiles.Count)];  
        }

    }


    private GameObject AttachInhancementButtonToNewBuilding(GameObject newBuilding)
    {
        GameObject newInhancementButton = Instantiate(GameManager.Instance.BuildingInhancementButton, newBuilding.transform.position, Quaternion.identity);

        newInhancementButton.transform.SetParent(GameManager.Instance.BuildingInhancementButtonParent);
        newInhancementButton.GetComponent<BuildingInhancementButton>().AttachButtonToTile(newBuilding.transform.position, GameManager.Instance.HeightOfBuildingInhancementButton, GameManager.Instance.OffsetOfBuildingInhancementButton);

        return newInhancementButton;
    }

}

public struct RoadType
{
    public GameObject road;
    public float rotation;
}

public enum RoadTypeName { TURN, STRAIGHT, CROSSING, THREE_WAY}

