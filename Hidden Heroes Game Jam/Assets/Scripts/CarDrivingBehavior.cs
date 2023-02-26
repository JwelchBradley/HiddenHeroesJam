/*****************************************************************************
// File Name :         CarDrivingBehavior.cs
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
using UnityEngine.SceneManagement;

public class CarDrivingBehavior : MonoBehaviour
{
    #region Fields

    #endregion

    #region Functions
    // Start is called before the first frame update
    private void Awake()
    {
        Invoke("SceneChange", 10);
    }


    private void SceneChange()
    {
        SceneManager.LoadScene("MechLevel");
    }
    #endregion
}
