/*****************************************************************************
// File Name :         NextLevel.cs
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

public class NextLevel : MonoBehaviour
{
    #region Fields
    public string levelName;
    #endregion

    #region Functions

    private void OnCollisionEnter(Collision collision)
    {
        if (levelName == "DavidsHouse")
        {
            SceneManager.LoadScene("DavidsHouse");
        }
        else if (levelName == "GoldenHarbor")
        {
            SceneManager.LoadScene("GoldenHarbor");
        }
        else if (levelName == "MechLevel")
        {
            SceneManager.LoadScene("MechLevel");
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
