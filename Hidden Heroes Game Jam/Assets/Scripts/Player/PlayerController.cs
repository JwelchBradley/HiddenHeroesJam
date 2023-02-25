using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Damageable
{
    #region Fields

    #endregion

    #region Functions
    // Start is called before the first frame update
    private void Awake()
    {
        
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

        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {

        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {

        }
    }
    #endregion
}
