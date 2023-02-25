/*****************************************************************************
// File Name :         Interactable.cs
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

[RequireComponent(typeof(Outline))]
public abstract class Interactable : MonoBehaviour
{
    #region Fields
    private static GameObject PressE;
    private Outline outline;
    #endregion

    #region Functions
    protected void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;

        if (PressE == null)
        {
            PressE = GameObject.Find("Press E");

            if (PressE != null) PressE.SetActive(false);
        }
    }

    public abstract void ClickEvent();

    public virtual void HoverEvent()
    {
        outline.enabled = true;

        if (PressE != null) PressE.SetActive(true);
    }

    public virtual void UnHoverEvent()
    {
        outline.enabled = false;

        if (PressE != null) PressE.SetActive(false);
    }
    #endregion
}
