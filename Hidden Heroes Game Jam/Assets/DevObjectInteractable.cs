/*****************************************************************************
// File Name :         DevObjectInteractable.cs
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

public class DevObjectInteractable : Interactable
{
    #region Fields
    private RaycastHit hit;
    private Ray ray;
    int layerMask = 1;
    private GameObject currentObject;
    private GameObject PressE;
    private int LayerNoCollison;
    private int LayerNormal;
    private bool isOn;

    public Material solid;
    public Material semiclear;

    private MeshRenderer meshRenderer;
    #endregion

    #region Functions
    // Start is called before the first frame update
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        LayerNoCollison = LayerMask.NameToLayer("NoPlayerCollisions");
        LayerNormal = LayerMask.NameToLayer("Default");
        isOn = true;
    }

    public override void ClickEvent()
    {
        if (isOn)
        {
            gameObject.layer = LayerNoCollison;
            meshRenderer.material = semiclear;
            isOn = false;
        }
        else
        {
            gameObject.layer = LayerNormal;
            meshRenderer.material = solid;
            isOn = true;
        }
    }
    #endregion
}
