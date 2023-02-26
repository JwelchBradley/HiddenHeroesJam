using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifcationManager : MonoBehaviour
{
    public GameObject DiscordHUD;
    public NarrativeBehaviour Manager;
    [SerializeField] private AudioClip discordPing;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enabled)
        {
            if (other.gameObject.tag == "Player")
            {
                audioSource.PlayOneShot(discordPing);

                Manager.AddCounter();
                DiscordHUD.gameObject.SetActive(true);

                enabled = false;
            }
        }
    }

}
