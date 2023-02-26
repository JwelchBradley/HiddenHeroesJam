/*****************************************************************************
// File Name :         DisableOnStart.cs
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

public class DisableOnStart : MonoBehaviour
{
    #region Fields

    #endregion

    #region Functions
    private void Start()
    {
        gameObject.SetActive(false);
    }
    #endregion
}
