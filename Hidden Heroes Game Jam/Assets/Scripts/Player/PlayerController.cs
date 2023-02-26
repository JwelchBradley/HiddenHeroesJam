using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Damageable
{
    #region Fields
    private Weapon weapon;
    [SerializeField] private bool startWithAxe = true;

    private GameObject Discord;
    bool DiscordOn = false;
    #endregion

    #region Functions
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        if (startWithAxe)
        {
            GetAxe();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        var healthBar = GameObject.Find("PlayerHealthBar");

        if(healthBar != null)
        HealthChangeEvent.AddListener(healthBar.GetComponent<HealthBarHandler>().UpdateHealthBar);
    }

    public void GetAxe()
    {
        weapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    private void Update()
    {
        WeaponInput();
        OpenDiscord();
    }

    private void WeaponInput()
    {
        if (weapon == null) return;

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
        if (Discord == null) return;

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
