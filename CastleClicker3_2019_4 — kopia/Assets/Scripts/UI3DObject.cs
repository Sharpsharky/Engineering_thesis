using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI3DObject : MonoBehaviour
{
    protected Vector3 positionOfTileAttached = new Vector3(0, 0, 0);
    protected float heightOfButtonAboveTile = 0;
    protected Vector2 offsetXZFromTile = new Vector2(0, 0);
    protected Camera cameraToLookAt;
    protected Image thisImage;

    protected GameObject tileTheButtonIsAssignedTo;

    public GameObject TileTheButtonIsAssignedTo { get => tileTheButtonIsAssignedTo; set => tileTheButtonIsAssignedTo = value; }

    protected void Start()
    {
        thisImage = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(OnClick);
        cameraToLookAt = GameManager.Instance.MainCamera;
    }


    protected void Update()
    {
        MantainPosition();
        ChangeAlpha();
    }

    protected void MantainPosition()
    {
        transform.position = new Vector3(positionOfTileAttached.x + offsetXZFromTile.x, positionOfTileAttached.y + heightOfButtonAboveTile, positionOfTileAttached.z + offsetXZFromTile.y);
        transform.rotation = cameraToLookAt.transform.rotation;
    }

    protected virtual void OnClick()
    {
        GameManager.Instance.CurrentlyPickedTile = positionOfTileAttached;
        Debug.Log("LOLOLOLO" + tileTheButtonIsAssignedTo);
        GameManager.Instance.CurrentlyPickedTileGameObject = tileTheButtonIsAssignedTo;
        GameManager.Instance.CurrentlyPickedBuildingButton = gameObject;
        GameManager.Instance.TurnOnOff3DSpaceInteraction(false);
        Debug.Log(tileTheButtonIsAssignedTo.name);

    }

    protected void ChangeAlpha()
    {
        if (Vector3.Distance(cameraToLookAt.transform.position, transform.position) > 18 && thisImage.color.a == 1)
        {
            Color tempColor = thisImage.color;
            tempColor.a = 0.5f;
            thisImage.color = tempColor;
        }
        else if (thisImage.color.a == 0.5f && Vector3.Distance(cameraToLookAt.transform.position, transform.position) <= 18)
        {

            Color tempColor = thisImage.color;
            tempColor.a = 1;
            thisImage.color = tempColor;

        }
    }


    public void AttachButtonToTile(Vector3 positionOfTile, float heightOfBuildingButton, Vector2 offsetXZ)
    {
        heightOfButtonAboveTile = heightOfBuildingButton;
        positionOfTileAttached = positionOfTile;
        offsetXZFromTile = offsetXZ;

    }

}
