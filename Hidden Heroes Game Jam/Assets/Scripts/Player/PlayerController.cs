using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Damageable
{
    #region Fields
    private Weapon weapon;

    private GameObject Discord;
    bool DiscordOn = false;
    #endregion

    #region Functions
    // Start is called before the first frame update
    private void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        WeaponInput();
        OpenDiscord();
    }

    private void WeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            weapon.WeaponDown();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            weapon.AltWeaponDown();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            weapon.WeaponUp();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            weapon.AltWeaponUp();
        }
    }

    private void OpenDiscord()
    {
        if (Input.GetKeyDown(KeyCode.D) && DiscordOn == false)
        {
            Discord.SetActive(true);
            DiscordOn = true;
        }

        else if (Input.GetKeyDown(KeyCode.D) && DiscordOn == true)
        {
            Discord.SetActive(false);
            DiscordOn = false;
        }
         
        
    }    
    #endregion
}
