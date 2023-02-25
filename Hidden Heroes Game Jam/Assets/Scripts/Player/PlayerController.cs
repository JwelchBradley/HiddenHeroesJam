using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Damageable
{
    #region Fields
    private Weapon weapon;
    #endregion

    #region Functions
    // Start is called before the first frame update
    private void Awake()
    {
        weapon = GetComponentInChildren<Weapon>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        var healthBar = GameObject.Find("PlayerHealthBar");

        if(healthBar != null)
        HealthChangeEvent.AddListener(healthBar.GetComponent<HealthBarHandler>().UpdateHealthBar);
    }

    // Update is called once per frame
    private void Update()
    {
        WeaponInput();
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
    #endregion
}
