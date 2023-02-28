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
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public abstract class Interactable : MonoBehaviour
{
    #region Fields
    protected static GameObject PressE;
    private Outline outline;

    protected string message = "Press E to debug";

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
            PressE = FindObjectOfType<DebugUIHandler>(true).gameObject;

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
        PressE.gameObject.GetComponent<TextMeshProUGUI>().text = message;

        if (PressE != null) PressE.SetActive(true);
    }

    public virtual void UnHoverEvent()
    {
        outline.enabled = false;

        if (PressE != null) PressE.SetActive(false);
    }
    #endregion
}
