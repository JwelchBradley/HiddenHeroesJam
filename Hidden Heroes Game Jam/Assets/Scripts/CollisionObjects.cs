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
    public GameObject triggerObject;
    #endregion

    #region Functions
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Club")
        {
            Destroy(triggerObject);
        }
    }

    #endregion
}
