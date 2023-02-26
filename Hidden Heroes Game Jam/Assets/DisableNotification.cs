/*****************************************************************************
// File Name :         DisableNotification.cs
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

public class DisableNotification : MonoBehaviour
{
    #region Fields
    [SerializeField] private GameObject DiscordNotification;

    private bool hasSetInactive = false;
    #endregion

    #region Functions
    private void OnEnable()
    {
        if (hasSetInactive)
        {
            DiscordNotification.SetActive(false);
        }
        else
        {
            hasSetInactive = true;
        }
    }

    private void Start()
    {
        DiscordNotification.SetActive(false);
    }
    #endregion
}
