/*****************************************************************************
// File Name :         CollisionObjects.cs
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

public class CollisionObjects : MonoBehaviour
{
    #region Fields
    public GameObject[] destroys;

    public GameObject[] spawns;
    #endregion

    #region Functions
    public void Triggered()
    {
        if (enabled)
        {
            foreach (GameObject spawn in spawns)
            {
                spawn.SetActive(true);
            }

            foreach (GameObject destroy in destroys)
            {
                Destroy(destroy);
            }

            enabled = false;
        }
    }

    #endregion
}
