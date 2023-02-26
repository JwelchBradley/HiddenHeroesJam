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
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenuOnDeath : MonoBehaviour
{
    #region Fields
    private int deathIndex = 0;
    #endregion

    #region Functions
    public void CheckIfDead(float current, float max)
    {
        print("Current Health: " + current);

        if(current == 0 && ++deathIndex == 8)
        {
            LoadMain();
        }
    }

    private async void LoadMain()
    {
        await Task.Delay(5000);
        FindObjectOfType<MenuBehavior>().LoadScene(SceneManager.GetSceneByBuildIndex(0).name);

    }
    #endregion
}
