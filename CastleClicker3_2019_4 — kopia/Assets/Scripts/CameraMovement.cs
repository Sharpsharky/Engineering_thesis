using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float groundY = 0;
    [SerializeField]
    private float minimmumZoomY = 15;
    [SerializeField]
    private float maximumZoomY = 5;
    [SerializeField]
    private float zoomingSpeed = 10;

    private Vector3 touchStart;
    private Camera thisCamera;

    private void Awake()
    {
        thisCamera = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = GetWorldPosition(groundY);
        }
        

        if(Input.touchCount== 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float previousMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - previousMagnitude;

            Zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - GetWorldPosition(groundY);

            thisCamera.transform.position += direction;
        }


        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    private Vector3 GetWorldPosition(float y)
    {
        Ray mousePos = thisCamera.ScreenPointToRay(Input.mousePosition);
        Plane ground = new Plane(Vector3.up, new Vector3(0, y, 0));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
    }

    private void Zoom(float increment)
    {
        Vector3 thisPosition = transform.position;
        thisPosition += transform.forward * increment * zoomingSpeed;
        
        if (thisPosition.y > maximumZoomY && thisPosition.y < minimmumZoomY)
        {
            transform.position += transform.forward * increment * zoomingSpeed;
        }
        
    }

}
