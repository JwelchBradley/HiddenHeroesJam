/*****************************************************************************
// File Name :         ClubInteractable.cs
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

public class ClubInteractable : Interactable
{
    #region Functions
    private void Start()
    {
        message = "Press E to grab club";
    }

    public override void ClickEvent()
    {
        base.ClickEvent();

        PressE.SetActive(false);

        FindObjectOfType<PlayerController>().GetAxe();
        Destroy(gameObject);
    }
    #endregion
}
