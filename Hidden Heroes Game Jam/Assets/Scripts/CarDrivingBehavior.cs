/*****************************************************************************
// File Name :         CarDrivingBehavior.cs
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

public class CarDrivingBehavior : MonoBehaviour
{
    #region Fields
    public AudioSource audioSource;
    public AudioClip mechLine;
    #endregion

    #region Functions
    // Start is called before the first frame update
    private void Awake()
    {
        //audioSource.GetComponent<AudioSource>();
        audioSource.PlayOneShot(mechLine);
        Invoke("SceneChange", 4.5f);
    }


    private void SceneChange()
    {
        SceneManager.LoadScene("MechLevel");
    }
    #endregion
}
