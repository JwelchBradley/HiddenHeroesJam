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

public abstract class Interactable : MonoBehaviour
{
    #region Fields
    
    #endregion

    #region Functions
    public abstract void ClickEvent();

    public virtual void HoverEvent()
    {

    }

    public virtual void UnHoverEvent()
    {

    }
    #endregion
}
