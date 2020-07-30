using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicking3DSpace : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = GameManager.Instance.MainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    if (GameManager.Instance.AreTilesClickable == true)
                    {
                        if (hit.transform.tag == "ResourceCreatingBuilding")
                        {

                            hit.transform.GetComponent<ResourceCreatingBuilding>().OnClick();

                        }//ELSE IF hit.transform.tag == "UIOpeningBuilding" 
                    }
                }
            }
        }
    }
}
