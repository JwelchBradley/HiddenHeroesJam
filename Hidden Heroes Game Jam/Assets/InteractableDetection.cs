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
    #endregion

    #region Functions
    private void Update()
    {
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 7.0f))
        {
            if (hit.collider.gameObject.CompareTag(interactableTag))
            {
                if(hoverObject != hit.collider.gameObject)
                {
                    if(hoverObject != null)
                    {
                        hoverObject.UnHoverEvent();
                    }

                    hoverObject = hit.collider.gameObject.GetComponent<Interactable>();
                    hoverObject.HoverEvent();
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hoverObject.ClickEvent();
                }
            }
        }
        else
        {
            if(hoverObject != null)
            {
                hoverObject.UnHoverEvent();
            }

            hoverObject = null;
        }
    }
    #endregion
}
