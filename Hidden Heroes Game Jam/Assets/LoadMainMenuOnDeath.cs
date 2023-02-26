/*****************************************************************************
// File Name :         LoadMainMenuOnDeath.cs
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

public class LoadMainMenuOnDeath : MonoBehaviour
{
    #region Fields

    #endregion

    #region Functions
    public void CheckIfDead(float current, float max)
    {
        if(current <= 0)
        {
            StartCoroutine(LoadMain());
        }
    }

    private IEnumerator LoadMain()
    {
        yield return new WaitForSeconds(5.0f);

        FindObjectOfType<MenuBehavior>().LoadScene(SceneManager.GetSceneByBuildIndex(0).name);
    }
    #endregion
}
