/*****************************************************************************
// File Name :         InteractableDetection.cs
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

public class InteractableDetection : MonoBehaviour
{
    #region Fields
    private const string interactableTag = "Interactable";

    protected Interactable hoverObject;
    private Transform mainCamera;
    #endregion

    #region Functions
    private void Awake()
    {
        mainCamera = Camera.main.transform;
    }

    private void Update()
    {
        if(Physics.Raycast(mainCamera.position, mainCamera.forward, out RaycastHit hit, 7.0f))
        {
            if (hit.collider.gameObject.CompareTag(interactableTag))
            {
                if(hoverObject == null)
                {
                    HoverEvent(hit);

                }
                else if (hoverObject.gameObject != hit.collider.gameObject)
                {
                    HoverEvent(hit);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hoverObject.ClickEvent();
                }
            }
            else
            {
                Unhover();
            }
        }
        else
        {
            Unhover();

            hoverObject = null;
        }
    }

    private void HoverEvent(RaycastHit hit)
    {
        Unhover();

        hoverObject = hit.collider.gameObject.GetComponent<Interactable>();
        hoverObject.HoverEvent();
    }

    private void Unhover()
    {
        if (hoverObject != null)
        {
            hoverObject.UnHoverEvent();
            hoverObject = null;
        }
    }
    #endregion
}
