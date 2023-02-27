/*****************************************************************************
// File Name :         PlayCrunch.cs
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

public class PlayCrunch : MonoBehaviour
{
    #region Fields
    AudioSource audioSource;
    #endregion

    #region Functions
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCrunchSound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
    #endregion
}
