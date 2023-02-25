/*****************************************************************************
// File Name :         LevelSelect.cs
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
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    #region Fields

    #endregion

    #region Functions

 
    public void buttonClick(string levelName)
    {
        if (levelName == "DavidsHouse")
        {
            SceneManager.LoadScene("DavidsHouse");
        }
        else if (levelName == "GoldenHarbor")
        {
            SceneManager.LoadScene("GoldenHarbor");
        }
        else if (levelName == "MechFlyover")
        {
            SceneManager.LoadScene("MechFlyover");
        }
        else if (levelName == "Evilition")
        {
            SceneManager.LoadScene("Evilition");
        }
        else
        {
            SceneManager.LoadScene("FinalBoss");
        }
    }
    #endregion
}
