/*****************************************************************************
// File Name :         HealthBarHandler.cs
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
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour
{
    #region Fields
    private Image healthbar;
    #endregion

    #region Functions
    // Start is called before the first frame update
    private void Awake()
    {
        healthbar = GetComponent<Image>();
    }

    public void UpdateHealthBar(float current, float max)
    {
        healthbar.fillAmount = current / max;
    }
    #endregion
}
