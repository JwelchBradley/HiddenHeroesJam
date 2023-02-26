/*****************************************************************************
// File Name :         DeathScreen.cs
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

public class DeathScreen : MonoBehaviour
{
    #region Fields

    #endregion

    #region Functions
    // Start is called before the first frame update
    private void Awake()
    {
        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.HealthChangeEvent.AddListener(DieEvent);
        }
        else
        {
            FindObjectOfType<Damageable>().HealthChangeEvent.AddListener(DieEvent);
        }

        gameObject.SetActive(false);
    }

    private void DieEvent(float current, float max)
    {
        if(current == 0)
        {
            gameObject.SetActive(true);
        }
    }
    #endregion
}
