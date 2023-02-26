/*****************************************************************************
// File Name :         ClampFOVToMain.cs
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

public class ClampFOVToMain : MonoBehaviour
{
    #region Fields
    Camera main;
    Camera thisCamera;
    #endregion

    #region Functions
    // Start is called before the first frame update
    private void Awake()
    {
        main = Camera.main;
        thisCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        thisCamera.fieldOfView = main.fieldOfView;
    }
    #endregion
}
