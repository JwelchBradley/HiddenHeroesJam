/*****************************************************************************
// File Name :         MechLevelTimer.cs
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

public class MechLevelTimer : MonoBehaviour
{
    #region Fields
    [SerializeField] private int levelTime = 90;
    #endregion

    #region Functions
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(DelayLevelEnd());
    }

    private IEnumerator DelayLevelEnd()
    {
        var t = levelTime;

        while(t > 0)
        {
            yield return new WaitForSeconds(1);
            t -= 1;
        }

        FindObjectOfType<MenuBehavior>().LoadScene("Evilition");
    }
    #endregion
}
