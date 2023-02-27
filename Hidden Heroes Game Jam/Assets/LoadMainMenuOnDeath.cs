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
    private bool isInfinalPhase = false;
    private bool hasStartedLoad = false;
    private ZavidDabzug zabid;
    #endregion

    #region Functions
    private void Update()
    {

        if (zabid == null)
        {
            zabid = FindObjectOfType<ZavidDabzug>();

            if(isInfinalPhase && !hasStartedLoad)
            StartCoroutine(LoadMain());
        }
        else
        {
            if (zabid.phase == 4) isInfinalPhase = true;
            print("Phase: " + zabid.phase);
        }
    }

    private IEnumerator LoadMain()
    {
        hasStartedLoad = true;
        yield return new WaitForSeconds(9.0f);
        FindObjectOfType<MenuBehavior>().LoadScene("Main Menu");

    }
    #endregion
}
