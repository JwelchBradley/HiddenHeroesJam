/*****************************************************************************
// File Name :         ReloadLevelOnDeath.cs
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
using UnityEngine.SceneManagement;

public class ReloadLevelOnDeath : MonoBehaviour
{
    #region Fields
    private bool hasLoaded = false;
    #endregion

    #region Functions
    public void CheckForDeath(float current, float max)
    {
        if(current == 0 && !hasLoaded)
        {
            hasLoaded = true;
            FindObjectOfType<MenuBehavior>().LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    #endregion
}
