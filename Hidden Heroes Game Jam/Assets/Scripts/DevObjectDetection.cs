/*****************************************************************************
// File Name :         DevObjectDetection.cs
// Author :            #AUTHOR#
// Contact :           #CONTACT#
// Creation Date :     #DATE#
// Company :           #COMPANY#
//
// Brief Description : Description of what the script does
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevObjectDetection : MonoBehaviour
{
    #region Fields
    private RaycastHit hit;
    private Ray ray;
    int layerMask = 1;
    private GameObject currentObject;
    public GameObject PressE;
    private int LayerNoCollison;
    private int LayerNormal;
    private bool isOn;

    public Material solid;
    public Material semiclear;

    #endregion

    #region Functions

    private void Start()
    {
        LayerNoCollison = LayerMask.NameToLayer("NoPlayerCollisions");
        LayerNormal = LayerMask.NameToLayer("Default");
        isOn = true;
    }

    private void Update()
    {


        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 7.0f))
        {
            if (hit.collider.gameObject.tag == "DevObj")
            {
                PressE.SetActive(true);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    if (isOn)
                    {
                        hit.collider.gameObject.layer = LayerNoCollison;
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material = semiclear;
                        isOn = false;
                    }
                    else
                    {
                        hit.collider.gameObject.layer = LayerNormal;
                        hit.collider.gameObject.GetComponent<MeshRenderer>().material = solid;
                        isOn = true;
                    }
                }
            }
            else
            {
                PressE.SetActive(false);
            }
        }
        else
        {
             PressE.SetActive(false);
        }
    }



    #endregion
}
