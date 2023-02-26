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

    [SerializeField] private List<GameObject> setActiveObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> setInactiveObjects = new List<GameObject>();
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

    public virtual void ClickEvent()
    {
        foreach(GameObject obj in setActiveObjects)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in setInactiveObjects)
        {
            obj.SetActive(false);
        }
    }

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
