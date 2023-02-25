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

public class DevObjectDetection : MonoBehaviour
{
    #region Fields
    private RaycastHit hit;
    int layerMask = 1;
    private GameObject currentObject;
    #endregion

    #region Functions

    private void Start()
    {

    }

    private void Update()
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.gameObject.tag == "DevObj")
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.yellow);
        }
    }



    #endregion
}
